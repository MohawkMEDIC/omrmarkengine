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
 * Date: 6-12-2015
 */

using MohawkCollege.Util.Console.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrConsole
{
    /// <summary>
    /// Parameters to be used on the console
    /// </summary>
    public class ConsoleParameters
    {
        /// <summary>
        /// Console parameters
        /// </summary>
        public ConsoleParameters()
        {
            this.OutputFormat = "xml";
        }

        /// <summary>
        /// Show help
        /// </summary>
        [Parameter("help")]
        [Parameter("?")]
        [Description("Show this help")]
        public bool Help { get; set; }

        /// <summary>
        /// The sources of the analysis
        /// </summary>
        [Parameter("s")]
        [Parameter("source")]
        [Description("Source image files (BMP, PNG or JPG)")]
        public StringCollection SourceFiles { get; set; }

        /// <summary>
        /// The output file
        /// </summary>
        [Parameter("o")]
        [Parameter("output")]
        [Description("Sets the output file for the scan analysis")]
        public String OutputFile { get; set; }

        /// <summary>
        /// Validate the output
        /// </summary>
        [Parameter("v")]
        [Parameter("validate")]
        [Description("Enable validation of the scanned image")]
        public bool Validate { get; set; }

        /// <summary>
        /// The output format
        /// </summary>
        [Parameter("format")]
        [Parameter("f")]
        [Description("Sets the output format (xml or csv)")]
        public String OutputFormat { get; set; }

        /// <summary>
        /// Run the script file attached to the template
        /// </summary>
        [Parameter("run")]
        [Parameter("r")]
        [Description("Run the script contained in the script")]
        public bool Run { get; set; }

        /// <summary>
        /// Sets the template to use
        /// </summary>
        [Parameter("t")]
        [Parameter("template")]
        [Description("The mxml template to use")]
        public String Template { get; set; }
    }
}
