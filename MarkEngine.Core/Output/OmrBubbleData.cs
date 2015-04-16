using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// OMR Question data
    /// </summary>
    [XmlType("OmrBubbleData", Namespace = "urn:scan-omr:analysis")]
    public class OmrBubbleData : OmrOutputData
    {

        /// <summary>
        /// The key 
        /// </summary>
        [XmlAttribute("key")]
        public String Key { get; set; }

        /// <summary>
        /// The value
        /// </summary>
        [XmlAttribute("value")]
        public String Value { get; set; }

        /// <summary>
        /// The area represented by the blob
        /// </summary>
        [XmlAttribute("area")]
        public Int32 BlobArea { get; set; }

        /// <summary>
        /// Value as int
        /// </summary>
        public float ValueAsFloat
        {
            get
            {
                return float.Parse(this.Value);
            }
        }
    }
}
