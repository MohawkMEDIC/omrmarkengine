using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OmrMarkEngine.Template
{
    /// <summary>
    /// Represents an input field which is nothing more than a collection of individual bubbles
    /// </summary>
    [XmlType("OmrBubbleContainer", Namespace = "urn:scan-omr:template")]
    public abstract class OmrBubbleContainer : OmrQuestionField
    {

        /// <summary>
        /// Construct the bubble container
        /// </summary>
        public OmrBubbleContainer()
        {
            this.Children = new List<OmrBubbleField>();
        }

        /// <summary>
        /// The children of this bubble field
        /// </summary>
        [XmlElement("child", typeof(OmrBubbleField))]
        public List<OmrBubbleField> Children { get; set; }


    }
}
