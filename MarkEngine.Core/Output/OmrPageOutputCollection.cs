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

        /// <summary>
        /// Load the file
        /// </summary>
        public static OmrPageOutputCollection Load(string filename)
        {
            using(FileStream fs = File.OpenRead(filename))
            {
                XmlSerializer xsz = new XmlSerializer(typeof(OmrPageOutputCollection));
                return xsz.Deserialize(fs) as OmrPageOutputCollection;
            }
        }
    }
}
