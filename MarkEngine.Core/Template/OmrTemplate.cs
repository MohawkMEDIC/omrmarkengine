using OmrMarkEngine.Core.Processor;
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

namespace OmrMarkEngine.Template
{
    [XmlType("OmrTemplate", Namespace = "urn:scan-omr:template")]
    [XmlRoot("template", Namespace = "urn:scan-omr:template")]
    public class OmrTemplate : OmrBoundObject
    {

        // Backing fields
        private String m_sourcePath;
        private Image m_imageSource;
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
        public Image ImageSource { 
            get {
                return this.m_imageSource;
            }
            set
            {
                this.m_imageSource = value;
                this.OnPropertyChange("ImageSource");
            }
        }

        /// <summary>
        /// Fields
        /// </summary>
        [XmlElement("barcodeQuestion", Type = typeof(OmrBarcodeField))]
        [XmlElement("questionBubble", Type = typeof(OmrBubbleField))]
        public List<OmrQuestionField> Fields { get; private set; }

        /// <summary>
        /// Gets a list of fields collapsed from all containers
        /// </summary>
        [XmlIgnore]
        public List<OmrQuestionField> FlatFields
        {
            get
            {
                List<OmrQuestionField> retVal = new List<OmrQuestionField>();
                foreach (var itm in this.Fields)
                    if (itm is OmrBubbleContainer)
                        retVal.AddRange((itm as OmrBubbleContainer).Children);
                    else
                        retVal.Add(itm);
                return retVal;
            }
        }

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
            retVal.ImageSource = inputImage.Image; 
            retVal.ProcessImageData(inputImage);

            return retVal;
        }
    }
}
