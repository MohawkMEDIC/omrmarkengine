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

using OmrMarkEngine.Core.Processor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Template
{
    /// <summary>
    /// Basic OMR template object
    /// </summary>
    [XmlType("OmrIdentifiedObject", Namespace = "urn:scan-omr:template")]
    public abstract class OmrIdentifiedObject : INotifyPropertyChanged
    {

        // Backing fields
        private String m_id;

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
