using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace CdmMethTools
{
    public class SemcoScadaDataLogFile : Interfaces.IDataLogFile
    {
        /// <summary>
        /// DateTime Format of data log timestamp after Date + " " + Time concatentation
        /// </summary>
        public const string TimeStampFormat = "dd/MM/yyyy HH:mm:ss";

        /// <summary>
        /// Constants associated with DataLogFileLoadOptions.ExtraData
        /// </summary>
        public class ExtraData
        {
            /// <summary>
            /// Key associated with ExtraData entry for the full path to the HDR header file.
            /// </summary>
            public const string HdrFile = "HDR";
        }

        /// <summary>
        /// Gets the dictionary of data rows.
        /// </summary>
        public Dictionary<DateTime, Dictionary<string, decimal>> DataRows { get; private set; }

        /// <summary>
        /// Loads the specified data log CSV, given the associated HDR file.
        /// </summary>
        /// <param name="fullPathCsv"></param>
        /// <param name="fullPathHdr"></param>
        public void Load(Interfaces.DataLogFileLoadOptions options) 
        {
            // Local Vars
            TextFieldParser tfp = new TextFieldParser(options.FullPath, Encoding.ASCII, true);
            Dictionary<string, decimal> dataRow;
            DateTime timeStamp;
            List<string> headers;
            string[] values;

            // (Re)Initialize the data rows dictionary
            if (null == DataRows)
            {
                DataRows = new Dictionary<DateTime, Dictionary<string, decimal>>();
            }
            else
            {
                DataRows.Clear();
            }

            // Load the headers list
            headers = LoadHdr(options.ExtraData[ExtraData.HdrFile].ToString());

            // Set our delimiters
            tfp.Delimiters = new string[] { "," };

            // Loop through the data rows
            while (!tfp.EndOfData)
            {
                // Read a line
                values = tfp.ReadFields();

                // First 2 are Date & Time
                timeStamp = DateTime.ParseExact(values[0] + " " + values[1], TimeStampFormat, CultureInfo.InvariantCulture);
                dataRow = new Dictionary<string, decimal>();

                for(int i = 2; i < values.Length; i++)
                {
                    try
                    {
                        dataRow.Add(headers[i - 2], Convert.ToDecimal(values[i]));
                    }
                    catch (Exception exDR)
                    {
                        throw new InvalidDataException(string.Format("An error occured interpreting {0} data. See inner exception for details.", headers[i]), exDR);
                    }
                }

                // Add the data row to our dictionary...
                DataRows.Add(timeStamp, dataRow);
            }
        }

        /// <summary>
        /// Load the HDR file and return as a string List.
        /// </summary>
        /// <param name="fullPathHdr"></param>
        /// <returns></returns>
        protected List<string> LoadHdr(string fullPathHdr)
        {
            // Local Vars
            string[] headers = File.ReadAllLines(fullPathHdr);
            List<string> hdrList = new List<string>();

            // Skip the # at the top, and add the rest to our list.
            hdrList.AddRange(headers.Skip(1));

            // Return
            return hdrList;
        }
    }
}
