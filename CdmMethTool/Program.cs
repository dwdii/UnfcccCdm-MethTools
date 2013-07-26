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
    }
}
