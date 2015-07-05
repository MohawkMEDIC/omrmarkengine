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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OmrMarkEngine.Template
{
    /// <summary>
    /// OMR Question field
    /// </summary>
    [XmlType("OmrQuestionField", Namespace = "urn:scan-omr:template")]
    public abstract class OmrQuestionField : OmrBoundObject
    {
        // Answer row group
        private string m_answerRowGroup;
        // Question is required
        private bool m_isQuestionRequired;

        /// <summary>
        /// Identifies the row group that this applies to
        /// </summary>
        [XmlAttribute("row")]
        [Description("Identifies the row to which the answer belongs")]
        public String AnswerRowGroup
        {
            get
            {
                return this.m_answerRowGroup;
            }
            set
            {
                this.m_answerRowGroup = value;
                this.OnPropertyChange("AnswerRowGroup");
            }
        }

        /// <summary>
        /// Is question required
        /// </summary>
        [XmlAttribute("isRequiredForRow")]
        [Description("Indicates whether the question concept is required for the row to be considered a successful scan")]
        public bool IsQuestionRequired 
        {
            get
            {
                return this.m_isQuestionRequired;
            }
            set
            {
                this.m_isQuestionRequired = value;
                this.OnPropertyChange("IsQuestionRequired");
            }
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
