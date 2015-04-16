using MarkEngine.Core.Processor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace MarkEngine.Template
{
    [XmlType("OmrTemplate", Namespace = "urn:scan-omr:template")]
    [XmlRoot("template", Namespace = "urn:scan-omr:template")]
    public class OmrTemplate : OmrBoundObject
    {

        // Backing fields
        private String m_sourcePath;

        /// <summary>
        /// Creates a new Omr template
        /// </summary>
        public OmrTemplate()
        {
            this.Id = "MyForm";
            this.Fields = new List<OmrQuestionField>();
        }

        /// <summary>
        /// Filename
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public String FileName { get; set; }

        /// <summary>
        /// Source image path
        /// </summary>
        [XmlElement("sourcePath")]
        [Description("The image that this template is based off")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public String SourcePath {
            get { return this.m_sourcePath; }
            set
            {
                this.m_sourcePath = value;
                this.OnPropertyChange("SourcePath");
            }
        }

        /// <summary>
        /// Gets the image source from which the template was created if applicable
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public ScannedImage ImageSource { get; private set; }

        /// <summary>
        /// Fields
        /// </summary>
        [XmlElement("barcodeQuestion", Type = typeof(OmrBarcodeField))]
        [XmlElement("questionBubble", Type = typeof(OmrBubbleField))]
        public List<OmrQuestionField> Fields { get; private set; }

        /// <summary>
        /// Save the file
        /// </summary>
        public void Save()
        {
            // Load from stream
            using (FileStream fs = File.Create(this.FileName))
            {
                XmlSerializer xsz = new XmlSerializer(typeof(OmrTemplate));
                xsz.Serialize(fs, this);
            }

        }

        /// <summary>
        /// Load from the file
        /// </summary>
        public static OmrTemplate Load(String fileName)
        {
            
            // Load from stream
            using(FileStream fs = File.OpenRead(fileName))
            {
                XmlSerializer xsz = new XmlSerializer(typeof(OmrTemplate));
                var retVal = xsz.Deserialize(fs) as OmrTemplate;
                retVal.FileName = fileName;
                return retVal;
            }
        }

        /// <summary>
        /// Create an OmrTemplate instance from the scanned image
        /// </summary>
        public static OmrTemplate FromFile(String fileName)
        {
            OmrTemplate retVal = new OmrTemplate();
            retVal.SourcePath = fileName;

            // Input image
            ScannedImage inputImage = new ScannedImage(fileName);

            // Analyze the image
            inputImage.Analyze();
            retVal.ImageSource = inputImage; 
            retVal.ProcessImageData(inputImage);

            return retVal;
        }
    }
}
