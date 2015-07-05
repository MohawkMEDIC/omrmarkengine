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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrMarkEngine.Wia
{
    /// <summary>
    /// Scan has completed event arguments
    /// </summary>
    public class ScanCompletedEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the image from the event
        /// </summary>
        public byte[] Image { get; private set; }

        /// <summary>
        /// Scan completed
        /// </summary>
        public ScanCompletedEventArgs(byte[] imageData)
        {
            this.Image = imageData;
        }
    }
}
