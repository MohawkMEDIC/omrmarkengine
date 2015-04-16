using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OmrMarkEngine.Template
{

    /// <summary>
    /// Bubble behavior
    /// </summary>
    public enum BubbleBehaviorType
    {
        One,
        Multi,
        Count 

    }

    /// <summary>
    /// Represents a single bubble
    /// </summary>
    [XmlType("OmrBubbleField", Namespace = "urn:scan-omr:template")]
    public class OmrBubbleField : OmrQuestionField
    {

        private string m_question;
        private string m_answer;
        private BubbleBehaviorType m_behavior = BubbleBehaviorType.One;

        /// <summary>
        /// Bubble behavior type,
        /// </summary>
        [XmlAttribute("behavior")]
        [Description("Behavior of the bubble. One = Only one bubble in the Question may be selected, Multi = Multiple bubbles may be selected, Sum = The sum of selected bubbles")]
        public BubbleBehaviorType Behavior 
        {
            get { return this.m_behavior;  }
            set
            {
                this.m_behavior = value;
                this.OnPropertyChange("Behavior");
            }
        }
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
