using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CdmMethTools
{
    public class SemcoScadaDataLogFile : IDataLogFile
    {
        public const string TimeStampFormat = "dd/MM/yyyy HH:mm:ss";

        /// <summary>
        /// Gets the dictionary of data rows.
        /// </summary>
        public Dictionary<DateTime, Dictionary<string, decimal>> DataRows { get; private set; }

        /// <summary>
        /// Loads the specified data log CSV, given the associated HDR file.
        /// </summary>
        /// <param name="fullPathCsv"></param>
        /// <param name="fullPathHdr"></param>
        public void Load(DataLogFileLoadOptions options) 
        {
            TextFieldParser tfp = new TextFieldParser(options.FullPath, Encoding.ASCII, true);
            string[] values;
            DateTime timeStamp;
            Dictionary<string, decimal> dataRow;

            tfp.Delimiters = new string[] { "," };

            // Loop through the data rows
            while (!tfp.EndOfData)
            {
                // Read a line
                values = tfp.ReadFields();

                // First 2 are Date & Time
                timeStamp = DateTime.ParseExact(values[0] + " " + values[1], TimeStampFormat, CultureInfo.InvariantCulture);
                dataRow = new Dictionary<string, decimal>();

                foreach (string value in values)
                {
                    //dataRow.Add(
                }
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
