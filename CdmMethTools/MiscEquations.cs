using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools
{
    /// <summary>
    /// Miscellaneous equations. These equations may be refactored to more appropriate object oriented locations in the future.
    /// </summary>
    public class MiscEquations
    {
        /// <summary>
        /// Calculates the saturation pressure of water in Pascals, given the specified gas tempurature in celcius.
        /// </summary>
        /// <param name="temperatureC"></param>
        /// <returns></returns>
        /// <seealso cref="http://www.engineeringtoolbox.com/water-vapor-saturation-pressure-air-d_689.html"/>
        /// <exception cref="System.OverflowException"></exception>
        public static decimal SaturationPressurePaOfH2O(decimal temperatureC)
        {
            // Local Vars
            double P_t_h2o = 0;
            double T_kelvin = Convert.ToDouble(UnitConvert.CelsiusToKelvin(temperatureC));

            // pws = e( 77.3450 + 0.0057 (273 (K) + 25 (degC)) - 7235 / (273 (K) + 25 (degC)) ) / (273 (K) + 25 (degC))8.2
            P_t_h2o = Math.Exp(77.3450 + (0.0057 * T_kelvin) - (7235 / T_kelvin)) / Math.Pow(T_kelvin, 8.2);

            // Return
            return Convert.ToDecimal(P_t_h2o);
        }
    }
}
