using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MarkEngine.Template
{
    /// <summary>
    /// OMR True/False field
    /// </summary>
    [XmlType("OmrQuestionField", Namespace = "urn:scan-omr:template")]
    public class OmrTrueFalseField : OmrQuestionField
    {
        // Horizontal
        private Orientation m_orientation = Orientation.Horizontal;
        private int m_padding = 10;

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

        /// <summary>
        /// Gets or sets the spacing between the answers
        /// </summary>
        [XmlAttribute("padding")]
        public int Padding {
            get
            {
                return this.m_padding;
            }
            set
            {
                this.m_padding = value;
                this.OnPropertyChange("Spacing");
            }
        }

    }
}
