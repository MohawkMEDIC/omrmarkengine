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
 * Date: 4-17-2015
 */
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
