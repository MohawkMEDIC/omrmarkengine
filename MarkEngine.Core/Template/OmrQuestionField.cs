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
    /// OMR Question field
    /// </summary>
    [XmlType("OmrQuestionField", Namespace = "urn:scan-omr:template")]
    public abstract class OmrQuestionField : OmrBoundObject
    {
        // Answer row group
        private string m_answerRowGroup;
        // Question is required
        private bool m_isQuestionRequired;

        /// <summary>
        /// Identifies the row group that this applies to
        /// </summary>
        [XmlAttribute("row")]
        [Description("Identifies the row to which the answer belongs")]
        public String AnswerRowGroup
        {
            get
            {
                return this.m_answerRowGroup;
            }
            set
            {
                this.m_answerRowGroup = value;
                this.OnPropertyChange("AnswerRowGroup");
            }
        }

        /// <summary>
        /// Is question required
        /// </summary>
        [XmlAttribute("isRequiredForRow")]
        [Description("Indicates whether the question concept is required for the row to be considered a successful scan")]
        public bool IsQuestionRequired 
        {
            get
            {
                return this.m_isQuestionRequired;
            }
            set
            {
                this.m_isQuestionRequired = value;
                this.OnPropertyChange("IsQuestionRequired");
            }
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
