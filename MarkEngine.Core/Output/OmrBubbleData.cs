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
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// OMR Question data
    /// </summary>
    [XmlType("OmrBubbleData", Namespace = "urn:scan-omr:analysis")]
    public class OmrBubbleData : OmrOutputData
    {

        /// <summary>
        /// The key 
        /// </summary>
        [XmlAttribute("key")]
        public String Key { get; set; }

        /// <summary>
        /// The value
        /// </summary>
        [XmlAttribute("value")]
        public String Value { get; set; }

        /// <summary>
        /// The area represented by the blob
        /// </summary>
        [XmlAttribute("area")]
        public Int32 BlobArea { get; set; }

        /// <summary>
        /// Value as int
        /// </summary>
        public float ValueAsFloat
        {
            get
            {
                return float.Parse(this.Value);
            }
        }

        /// <summary>
        /// Represent as a string
        /// </summary>
        public override string ToString()
        {
            return this.Value;
        }
    }
}
