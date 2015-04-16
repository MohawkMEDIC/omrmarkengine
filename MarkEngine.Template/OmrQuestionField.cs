using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MarkEngine.Template
{
    /// <summary>
    /// OMR Question field
    /// </summary>
    [XmlType("OmrQuestionField", Namespace = "urn:scan-omr:template")]
    public abstract class OmrQuestionField : OmrBoundObject
    {

        /// <summary>
        /// Identifies the row group that this applies to
        /// </summary>
        [XmlAttribute("row")]
        [Description("Identifies the row to which the answer belongs")]
        public String AnswerRowGroup { get; set; }

    }
}
