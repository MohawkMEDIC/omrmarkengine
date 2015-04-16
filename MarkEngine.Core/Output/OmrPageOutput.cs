using OmrMarkEngine.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// OMR Analysis
    /// </summary>
    [XmlType("OmrPageOutput", Namespace = "urn:scan-omr:analysis")]
    [XmlRoot("page", Namespace = "urn:scan-omr:analysis")]
    public class OmrPageOutput : OmrOutputDataCollection
    {
        
        /// <summary>
        /// Reference identifier for pages in the scanner
        /// </summary>
        [XmlElement("refImage")]
        public List<String> RefImages { get; set; }

        /// <summary>
        /// Start time
        /// </summary>
        [XmlAttribute("start")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Stop time
        /// </summary>
        [XmlAttribute("stopTime")]
        public DateTime StopTime { get; set; }

        /// <summary>
        /// The template processed
        /// </summary>
        [XmlAttribute("templateId")]
        public String TemplateId { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        [XmlElement("parameter")]
        public String[] Parameters { get; set; }

        /// <summary>
        /// Outcome
        /// </summary>
        [XmlAttribute("outcome")]
        public OmrScanOutcome Outcome { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [XmlElement("errorText")]
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Analyze image
        /// </summary>
        [XmlIgnore]
        public String AnalyzedImage { get; set; }
    }
}
