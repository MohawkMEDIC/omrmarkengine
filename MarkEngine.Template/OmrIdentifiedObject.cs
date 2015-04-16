using MarkEngine.Core.Processor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MarkEngine.Template
{
    /// <summary>
    /// Basic OMR template object
    /// </summary>
    [XmlType("OmrIdentifiedObject", Namespace = "urn:scan-omr:template")]
    public abstract class OmrIdentifiedObject : INotifyPropertyChanged
    {

        // Backing fields
        private String m_name;
        private String m_id;

        /// <summary>
        /// The name of the template
        /// </summary>
        [XmlElement("name")]
        public String Name {
            get { return this.m_name; }
            set
            {
                this.m_name = value;
                this.OnPropertyChange("Name");
            }
        }

        /// <summary>
        /// ID of the template
        /// </summary>
        [XmlAttribute("id")]
        public String Id {
            get { return this.m_id; }
            set
            {
                this.m_id = value;
                this.OnPropertyChange("Id");
            }
        }


        /// <summary>
        /// Property has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed
        /// </summary>
        protected void OnPropertyChange(String propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void ProcessImageData(ScannedImage image)
        {
            if(image.IsScannable)
                this.m_id = image.TemplateName;
        }
    }
}
