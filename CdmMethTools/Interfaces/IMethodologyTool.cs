using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdmMethTools.Interfaces
{
    public interface IMethodologyTool
    {
        string Name { get; }
        Uri MoreInfoLink { get; }
    }
}
