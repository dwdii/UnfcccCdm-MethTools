using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools
{
    /// <summary>
    /// Tool to determine the mass flow of a greenhouse gas in a gaseous stream Version 2.0.0
    /// </summary>
    /// <seealso cref="http://cdm.unfccc.int/methodologies/PAmethodologies/tools/am-tool-08-v2.0.0.pdf/history_view"/>
    public class GaseousStreamMassFlowTool_EB61Annex11v2
    {
        /// <summary>
        /// Universal ideal gases constant (Pa.m3/kmol.K) = 8.314
        /// </summary>
        public const decimal R_u = 8.314m;

        /// <summary>
        /// Saturation pressure of H2O at 100C in time interval t (Pascals)
        /// </summary>  
        public const decimal p_H2O_t_Sat = 101.325m;

        /// <summary>
        /// Equation 4 - Calculates the saturation absolute humidity (mH2O,t,db,sat)
        /// </summary>
        /// <param name="p_H2O_t_Sat">Saturation pressure of H2O at temperature Tt in time interval t (Pa)</param>
        /// <param name="P_t">Absolute pressure of the gaseous stream in time interval t (Pa)</param>
        /// <param name="MM_t_db">Molecular mass of the gaseous stream in a time interval t on a dry basis (kg dry gas/kmol dry gas)</param>
        /// <returns>Saturation absolute humidity in time interval t on a dry basis (kg H2O/kg dry gas)</returns>
        public decimal Calc_m_H2O_t_db_Sat(decimal p_H2O_t_Sat, decimal P_t, decimal MM_t_db)
        {
            // Local Vars
            decimal m_H2O_t_db_Sat = 0;

            // Equation 4
            m_H2O_t_db_Sat = (p_H2O_t_Sat * MolecularMass.H2O) / ((P_t - p_H2O_t_Sat) * MM_t_db);

            // Return
            return m_H2O_t_db_Sat;
        }

        /// <summary>
        /// Equation 5 - Calculates the mass flow of greenhouse gas i (F_i,t)
        /// </summary>
        /// <param name="V_t_db">Volumetric flow of the gaseous stream in time interval t on a dry basis (m³ dry gas/h)</param>
        /// <param name="v_i_t_db">Volumetric fraction of greenhouse gas i in the gaseous stream in a time interval t on a dry basis (m³ gas i/m³ dry gas)</param>
        /// <param name="rho_i_t">Density of greenhouse gas i in the gaseous stream in time interval t (kg gas i/m³ gas i)</param>
        /// <returns>Mass flow of greenhouse gas i in the gaseous stream in time interval t (kg gas/h)</returns>
        public decimal Calc_F_i_t(decimal V_t_db, decimal v_i_t_db, decimal rho_i_t)
        {
            // Local Variables
            decimal F_i_t = 0;

            // Equation 5
            F_i_t = V_t_db * v_i_t_db * rho_i_t;

            // Return
            return F_i_t;
        }

        /// <summary>
        /// Equation 6 - Calculates the density of greenhouse gas i in the gaseous stream in time interval t (kg gas i/m³ gas i)
        /// </summary>
        /// <param name="P_t">Absolute pressure of the gaseous stream in time interval t (Pa)</param>
        /// <param name="MM_i">Molecular mass of greenhouse gas i (kg/kmol)</param>
        /// <param name="T_t">Temperature of the gaseous stream in time interval t (K)</param>
        /// <returns>Density of greenhouse gas i in the gaseous stream in time interval t (kg gas i/m³ gas i)</returns>
        public decimal Calc_Rho_i_t(decimal P_t, decimal MM_i, decimal T_t)
        {
            // Local Vars
            decimal Rho_i_t = 0;

            Rho_i_t = (P_t * MM_i) / (R_u * T_t);

            // Return
            return Rho_i_t;
        }

        /// <summary>
        /// Equation 7  - Calculates the volumetric flow of the gaseous stream in time interval t on a dry basis (Vt,db)
        /// </summary>
        /// <param name="V_t_wb">Volumetric flow of the gaseous stream in time interval t on a wet basis (m³ wet gas/h)</param>
        /// <param name="v_H2O_t_db">Volumetric fraction of H2O in the gaseous stream in time interval t on a dry basis (m³ H2O/m³ dry gas)</param>
        /// <returns>Volumetric flow of the gaseous stream in time interval t on a dry basis (m³ dry gas/h)</returns>
        public decimal Calc_V_t_db(decimal V_t_wb, decimal v_H2O_t_db)
        {
            // Local Vars
            decimal V_t_db = 0;

            // Equation 7
            V_t_db = V_t_wb / (1 + v_H2O_t_db);

            // Return
            return V_t_db;
        }

        /// <summary>
        /// Equation 8 - The volumetric fraction of H2O in time interval t on a dry basis (vH2O,t,db)
        /// </summary>
        /// <param name="m_H2O_t_db">Absolute humidity in the gaseous stream in time interval t on a dry basis (kg H2O/kg dry gas)</param>
        /// <param name="MM_t_db">Molecular mass of the gaseous stream in time interval t on a dry basis (kg dry gas/kmol dry gas)</param>
        /// <returns>The volumetric fraction of H2O in time interval t on a dry basis (vH2O,t,db)</returns>
        public decimal Calc_v_H2O_t_db(decimal m_H2O_t_db, decimal MM_t_db)
        {
            // Local Vars
            decimal v_H2O_t_db = 0;

            v_H2O_t_db = (m_H2O_t_db * MM_t_db) / MolecularMass.H2O;

            // Return
            return v_H2O_t_db;
        }

        /// <summary>
        /// Calculates the molecular mass of the gaseous stream (MM_t_db).
        /// </summary>
        /// <param name="v_ch4_t_db">Volumetric fraction of methane (CH4) in the gaseious stream in time interval t on a dry basis (m3 gas h/m3 dry gas).</param>
        /// <param name="v_co2_t_db">Volumetric fraction of carbon dioxide (CO2) in the gaseious stream in time interval t on a dry basis (m3 gas h/m3 dry gas).</param>
        /// <param name="v_co_t_db">Volumetric fraction of carbon monoxide (CO) in the gaseious stream in time interval t on a dry basis (m3 gas h/m3 dry gas).</param>
        /// <param name="v_o2_t_db">Volumetric fraction of oxygen (O) in the gaseious stream in time interval t on a dry basis (m3 gas h/m3 dry gas).</param>
        /// <param name="v_h2_t_db">Volumetric fraction of hydrogen (H2) in the gaseious stream in time interval t on a dry basis (m3 gas h/m3 dry gas).</param>
        /// <param name="v_n2_t_db">Volumetric fraction of nitrogen (N2) in the gaseious stream in time interval t on a dry basis (m3 gas h/m3 dry gas).</param>
        /// <returns></returns>
        public decimal Calc_MM_t_db(decimal? v_ch4_t_db, decimal? v_co2_t_db, decimal? v_co_t_db, decimal? v_o2_t_db, decimal? v_h2_t_db, decimal? v_n2_t_db)
        {
            // Local Vars
            decimal MM_t_db = 0;

            // CH4
            if (v_ch4_t_db.HasValue)
            {
                MM_t_db += v_ch4_t_db.Value * MolecularMass.CH4;
            }

            // CO2
            if (v_co2_t_db.HasValue)
            {
                MM_t_db += v_co2_t_db.Value * MolecularMass.CO2;
            }

            // CO
            if (v_co_t_db.HasValue)
            {
                MM_t_db += v_co_t_db.Value * MolecularMass.CO;
            }

            // O2
            if (v_o2_t_db.HasValue)
            {
                MM_t_db += v_o2_t_db.Value * MolecularMass.O2;
            }

            // H2
            if (v_h2_t_db.HasValue)
            {
                MM_t_db += v_h2_t_db.Value * MolecularMass.H2;
            }

            // N2
            if (v_n2_t_db.HasValue)
            {
                MM_t_db += v_n2_t_db.Value * MolecularMass.N2;
            }

            // Return
            return MM_t_db;

        }
    }
}
