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
    /// Represents a single bubble
    /// </summary>
    [XmlType("OmrBubbleField", Namespace = "urn:scan-omr:template")]
    public class OmrBubbleField : OmrQuestionField
    {

        private string m_question;
        private string m_answer;

        /// <summary>
        /// Gets or sets the question key (that is the key which this belongs to)
        /// </summary>
        [XmlAttribute("key")]
        [Description("The question which the value answers")]
        public String Question {
            get { return this.m_question; }
            set { 
                this.m_question = value;
                this.OnPropertyChange("Question");
            }
        }

        /// <summary>
        /// Gets or sets the question key (that is the key which this belongs to)
        /// </summary>
        [XmlAttribute("value")]
        [Description("The value this bubble represents when filled")]
        public String Value
        {
            get
            {
                return this.m_answer;
            }
            set
            {
                this.m_answer = value;
                this.OnPropertyChange("Value");
            }
        }

    }
}
