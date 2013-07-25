using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools
{
    public interface IDataLogFile
    {
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
