using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CdmMethTools;

namespace CdmMethTestTool
{
    class Program
    {
        static void Main(string[] args)
        {
            // Quick Test
            GaseousStreamMassFlowTool_EB61Annex11v2 t = new GaseousStreamMassFlowTool_EB61Annex11v2();

            // Call the calc method
            decimal F_i_t = t.Calc_F_i_t(100m, 0.45m, 0.1786m);

            // Show result
            Console.Write("F_i_t: {0}", F_i_t);
        }
    }
}
