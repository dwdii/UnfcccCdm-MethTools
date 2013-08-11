using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CdmMethTools;
using CdmMethTools.Interfaces;

namespace CdmMethTool
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
                decimal o2Percent;
                decimal P_t_fl2;
                decimal T_t_fl2;
                decimal rho_i_t;
                decimal F_i_t_fl2;
                decimal p_H2O_t_Sat; 

                foreach(KeyValuePair<DateTime, Dictionary<string, decimal>> dataRowEntry in dl.DataRows)
                {
                    timeStamp = dataRowEntry.Key;
                    dataRow = dataRowEntry.Value;

                    // First calculate the molecular mass of the gaseous stream (using simplified approach).
                    ch4Percent = dataRow[CH4] / 100;
                    o2Percent = dataRow[O2] / 100;
                    MM_t_db = t.Calc_MM_t_db(ch4Percent, null, null, o2Percent, null, 1 - ch4Percent);

                    // This data is in Inches of H2O, so convert to Pascals and get gas temp also.
                    P_t_fl2 = UnitConvert.InchesH2OToPascals(dataRow[FL2_Pressure]);
                    T_t_fl2 = UnitConvert.CelsiusToKelvin(dataRow[FL2_GasTemp]);

                    // Calc saturation pressure at actual gas temp;
                    p_H2O_t_Sat = MiscEquations.SaturationPressurePaOfH2O(dataRow[FL2_GasTemp]);

                    // Calc saturation absolution humidity
                    //
                    m_H2O_t_db = t.Calc_m_H2O_t_db_Sat(p_H2O_t_Sat, P_t_fl2, MM_t_db);

                    // Calc volumetric fraction of H2O
                    v_H20_t_db = t.Calc_v_H2O_t_db(m_H2O_t_db, MM_t_db);

                    // Calc volumetric flow 
                    V_t_db = t.Calc_V_t_db(dataRow[FL2_Flow], v_H20_t_db);

                    // Calc density of CH4
                    rho_i_t = t.Calc_Rho_i_t(P_t_fl2, MolecularMass.CH4, T_t_fl2);
                    
                    // Mass flow
                    F_i_t_fl2 = t.Calc_F_i_t(V_t_db, ch4Percent, rho_i_t);
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
        private const string FL2_Pressure = "PT27[1]";
        private const string FL2_GasTemp = "TT27[1]";
        private const string FL2_FlareTemp = "TT26[1]";

        private const string CH4 = "GTY7_CH4[1]";
        private const string O2 = "GTY7_O2_2[1]";
        #endregion
    }
}
