using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools
{
    public class UnitConvert
    {
        public const decimal PascalsPerInchH20 = 249.08890833319208m;

        public const decimal KelvinFromCelsiusAdditive = 273.15m;

        /// <summary>
        /// Converts a pressure value from Inches of Water to Pascals.
        /// </summary>
        /// <param name="inchesH20"></param>
        /// <returns></returns>
        public static decimal InchesH2OToPascals(decimal inchesH20)
        {
            return inchesH20 * PascalsPerInchH20;
        }

        /// <summary>
        /// Converts a tempurature value from Celsius to Kelvin
        /// </summary>
        /// <param name="celsius"></param>
        /// <returns></returns>
        public static decimal CelsiusToKelvin(decimal celsius)
        {
            return celsius + KelvinFromCelsiusAdditive;
        }
    }
}
