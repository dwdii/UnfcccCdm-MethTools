﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools
{
    /// <summary>
    /// Methodological tool “Project emissions from flaring” Version 2.0.0 - EB68 Annex 15
    /// </summary>
    /// <seealso cref="http://cdm.unfccc.int/methodologies/PAmethodologies/tools/am-tool-06-v2.0.pdf/history_view"/>
    public class ProjectEmissionsFromFlaringTool_EB68Annex15v2 : Interfaces.IMethodologyTool
    {
        /// <summary>
        /// Helper class for encapsulating a flare efficiency measurement
        /// </summary>
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
        /// Reference conditions are defined as 0oC (273.15 K, 32ºF) and 1 atm (101.325 kN/m2, 101.325 kPa, 14.69 psia, 29.92 in Hg, 760 torr).
        /// </summary>
        public struct ReferenceConditions
        {
            /// <summary>
            /// 0C (273.15 K, 32ºF)
            /// </summary>
            public const decimal TemperatureC = 0;

            /// <summary>
            /// 1 atm (101.325 kN/m2, 101.325 kPa, 14.69 psia, 29.92 in Hg, 760 torr).
            /// </summary>
            public const decimal PressureAtm = 1;
        }

        #region IMethodologyTool interface

        public string Name
        {
            get { return "Methodological tool \"Project emissions from flaring\" Version 2.0.0"; }
        }

        public Uri MoreInfoLink
        {
            get { return new Uri("http://cdm.unfccc.int/methodologies/PAmethodologies/tools/am-tool-06-v2.0.pdf/history_view"); }
        }

        #endregion

        /// <summary>
        /// Equation 1: Option B.1: Calculates flare efficiency made in year y (ηflare,calc,y) from biannual measurement of the flare efficiency
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

        /// <summary>
        /// Equation 2: Option B.2: Measurement of flare efficiency in each minute
        /// </summary>
        /// <param name="flareEffMeasure">The mass flow of methane in the residual gas and exhaust gas of the flare on a dry basis at reference conditions in the minute m (kg)</param>
        /// <returns>Flare efficiency in the minute m</returns>
        public decimal Calc_Eta_flare_calc_m(FlareEffMassFlow flareEffMeasure)
        {
            // Local Vars
            decimal Eta_flare_calc_m = 0;

            // Calc
            Eta_flare_calc_m = 1 - (flareEffMeasure.F_CH4_EG_t / flareEffMeasure.F_CH4_RG_t);

            // Return
            return Eta_flare_calc_m;
        }
    }
}
