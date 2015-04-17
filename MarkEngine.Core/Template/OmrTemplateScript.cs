using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Core.Template
{
    /// <summary>
    /// Represents a script which can be run after processing a page
    /// </summary>
    [XmlRoot("script", Namespace = "urn:scan-omr:template")]
    public class OmrTemplateScript
    {

        /// <summary>
        /// Assemblies
        /// </summary>
        [XmlElement("reference")]
        public List<String> Assemblies { get; set; }

        /// <summary>
        /// Script to be run when the scan completes
        /// </summary>
        [XmlText]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public String Script { get; set; }

    }
}
