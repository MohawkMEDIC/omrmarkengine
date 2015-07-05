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

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// Validation Result
    /// </summary>
    public class ValidationResult
    {

        /// <summary>
        /// Validation result ctor
        /// </summary>
        public ValidationResult()
        {
            this.Issues = new List<string>();
        }

        /// <summary>
        /// True if valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// The issues
        /// </summary>
        public List<String> Issues { get; set; }
    }
}
