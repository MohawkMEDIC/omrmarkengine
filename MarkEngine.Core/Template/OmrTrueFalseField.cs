using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OmrMarkEngine.Template
{
    /// <summary>
    /// OMR True/False field
    /// </summary>
    [XmlType("OmrQuestionField", Namespace = "urn:scan-omr:template")]
    public class OmrTrueFalseField : OmrBubbleContainer
    {
        // Horizontal
        private Orientation m_orientation = Orientation.Horizontal;

        /// <summary>
        /// Gets or sets the orientation
        /// </summary>
        [XmlAttribute("orientation")]
        public Orientation Orientation
        {
            get { return this.m_orientation; }
            set { 
                this.m_orientation = value;
                this.OnPropertyChange("Orientation");
            }
        }

    }
}
