using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// OMR Page Output Collection
    /// </summary>
    [XmlType("OmrPageOutputCollection", Namespace = "urn:scan-omr:analysis")]
    [XmlRoot("pages", Namespace = "urn:scan-omr:analysis")]
    public class OmrPageOutputCollection 
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public OmrPageOutputCollection()
        {
            this.Pages = new List<OmrPageOutput>();
        }

        /// <summary>
        /// Gets or sets the pages
        /// </summary>
        [XmlElement("page")]
        public List<OmrPageOutput> Pages { get; set; }

        /// <summary>
        /// Save the scanned pages
        /// </summary>
        public void Save(string fileName)
        {
            using (FileStream fs = File.Create(fileName))
                new XmlSerializer(typeof(OmrPageOutputCollection)).Serialize(fs, this);

        }
    }
}
