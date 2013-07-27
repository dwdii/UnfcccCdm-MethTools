using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CdmMethTools;

namespace CdmMethTestTool
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Quick Test
                GaseousStreamMassFlowTool_EB61Annex11v2 t = new GaseousStreamMassFlowTool_EB61Annex11v2();

                // Call the calc method
                decimal F_i_t = t.Calc_F_i_t(100m, 0.45m, 0.1786m);

                // Show result
                Console.Write("F_i_t: {0}", F_i_t);

                #region Data Log File Loading
                IDataLogFile dl = new SemcoScadaDataLogFile();
                DataLogFileLoadOptions opts = new DataLogFileLoadOptions();
                FileInfo thisExe = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

                opts.FullPath = Path.Combine(thisExe.DirectoryName, "130725.csv");
                opts.ExtraData.Add(SemcoScadaDataLogFile.ExtraData.HdrFile, Path.Combine(thisExe.DirectoryName, "ScadaConfig.hdr"));

                dl.Load(opts);
                #endregion

                DateTime timeStamp;
                Dictionary<string, decimal> dataRow;
                decimal V_t_db;
                decimal v_H20_t_db;
                decimal m_H2O_t_db;
                decimal MM_t_db;
                decimal ch4Percent;

                foreach(KeyValuePair<DateTime, Dictionary<string, decimal>> dataRowEntry in dl.DataRows)
                {
                    timeStamp = dataRowEntry.Key;
                    dataRow = dataRowEntry.Value;

                    // First calculate the molecular mass of the gaseous stream (using simplified approach).
                    ch4Percent = dataRow[CH4] / 100;
                    MM_t_db = t.Calc_MM_t_db(ch4Percent, null, null, null, null, 1 - ch4Percent);

                    //m_H2O_t_db = t.Calc_m_H2O_t_db_Sat(

                    //v_H20_t_db = t.Calc_v_H2O_t_db(m_H2O_t_db, MM_t_db);

                    //V_t_db = t.Calc_V_t_db(dataRow[FL2_Flow], v_H20_t_db);
                    
                    //t.Calc_F_i_t();
                }
                
            }
            catch (Exception ex)
            {
                HandleMainException(ex);
            }
        }

        private static void HandleMainException(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadLine();
            Console.WriteLine("Press ENTER to end...");
        }

        #region Constants
        private const string FL2_Flow = "FT27[1]";
        private const string CH4 = "GTY7_CH4[1]";
        #endregion
    }
}
