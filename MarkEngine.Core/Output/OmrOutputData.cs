using OmrMarkEngine.Template;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{

    /// <summary>
    /// OMR Bubble Data
    /// </summary>
    [XmlType("OmrOutputData", Namespace = "urn:scan-omr:analysis")]
    public abstract class OmrOutputData : OmrBoundObject
    {

    }
}
