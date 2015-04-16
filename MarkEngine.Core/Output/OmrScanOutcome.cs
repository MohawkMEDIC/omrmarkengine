using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// Scan outcome
    /// </summary>
    [XmlType("OmrScanOutcome", Namespace = "urn:scan-omr:analysis")]
    public enum OmrScanOutcome
    {
        Success,
        Failure
    }
}
