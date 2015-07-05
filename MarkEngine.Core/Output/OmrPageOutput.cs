/* 
 * Optical Mark Recognition 
 * Copyright 2015, Justin Fyfe
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * Author: Justin
 * Date: 4-16-2015
 */

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
        /// The path the template
        /// </summary>
        [XmlIgnore]
        public OmrTemplate Template { get; set; }

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
