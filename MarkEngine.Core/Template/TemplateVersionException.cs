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
 * Date: 5-1-2015
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmrMarkEngine.Core.Template
{
    /// <summary>
    /// Template version exception
    /// </summary>
    public class TemplateVersionException : Exception
    {
        /// <summary>
        /// Create a new template version exception
        /// </summary>
        public TemplateVersionException(string templateVersion, string maxSupportedVersion) : base(String.Format("The supplied template was created a version not compatible with this version of OMR Mark Engine. The maximum version this engine supports is {0}, the template version is {1}", maxSupportedVersion, templateVersion))
        {
        }
    }
}
