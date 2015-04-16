using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// Row data
    /// </summary>
    [XmlType("OmrRowData", Namespace = "urn:scan-omr:analysis")]
    public class OmrRowData : OmrOutputDataCollection
    {

    }
}
