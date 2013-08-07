using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools
{
    /// <summary>
    /// Methodological tool “Project emissions from flaring” Version 2.0.0 - EB68 Annex 15
    /// </summary>
    /// <seealso cref="http://cdm.unfccc.int/methodologies/PAmethodologies/tools/am-tool-06-v2.0.pdf/history_view"/>
    public class ProjectEmissionsFromFlaringTool_EB68Annex15v2
    {
        public class FlareEffMassFlow
        {
            /// <summary>
            /// Mass flow of methane in the exhaust gas of the flare on a dry basis at reference conditions in the time period t (kg)
            /// </summary>
            public decimal F_CH4_EG_t = 0;

            /// <summary>
            /// Mass flow of methane in the residual gas on a dry basis at reference conditions in the time period t (kg)
            /// </summary>
            public decimal F_CH4_RG_t = 0;
        }

        /// <summary>
        /// Equation 1: Calculates flare efficiency made in year y (ηflare,calc,y) from biannual measurement of the flare efficiency
        /// </summary>
        /// <param name="flareEffMeasure1">The first flare efficiency measurement</param>
        /// <param name="flareEffMeasure2">The second flare efficiency measurement</param>
        /// <returns>Flare efficiency in the year y</returns>
        public decimal Calc_Eta_flare_calc_y(FlareEffMassFlow flareEffMeasure1, FlareEffMassFlow flareEffMeasure2)
        {
            // Param check
            if (null == flareEffMeasure1)
            {
                throw new ArgumentNullException("flareEffMeasure1");
            }
            else if (null == flareEffMeasure2)
            {
                throw new ArgumentNullException("flareEffMeasure2");
            }

            // Local Vars
            decimal Eta_flare_calc_y = 0;
            decimal sumOfRatios = 0;
            List<FlareEffMassFlow> list = new List<FlareEffMassFlow>();

            // Summation
            list.Add(flareEffMeasure1);
            list.Add(flareEffMeasure2);
            foreach (FlareEffMassFlow flareEffMeasure in list)
            {
                // First calc the sum of ratios
                sumOfRatios += flareEffMeasure.F_CH4_EG_t / flareEffMeasure.F_CH4_RG_t;
            }

            // Calc
            Eta_flare_calc_y = 1 - ((1 / 2) * sumOfRatios);

            // Return
            return Eta_flare_calc_y;
        }
    }
}
