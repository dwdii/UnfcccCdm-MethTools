using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools.Interfaces
{
    public interface IDataLogFile
    {
        /// <summary>
        /// Gets the dictionary of data rows.
        /// </summary>
        Dictionary<DateTime, Dictionary<string, decimal>> DataRows { get; }

        /// <summary>
        /// Load the specified data file according to the specified options
        /// </summary>
        /// <param name="options"></param>
        void Load(DataLogFileLoadOptions options);
    }

    public class DataLogFileLoadOptions
    {
        public string FullPath { get; set; }
        public Dictionary<string, object> ExtraData { get; private set; }

        public DataLogFileLoadOptions()
        {
            ExtraData = new Dictionary<string, object>();
        }
    }
}
