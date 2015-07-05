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

namespace OmrMarkEngine.Wia
{
    /// <summary>
    /// Scanner info
    /// </summary>
    public class ScannerInfo
    {

        public String Name { get; set; }
        public String Id { get; set; }

        public ScannerInfo(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Get the device
        /// </summary>
        public WIA.Device GetDevice()
        {
            WIA.DeviceManager manager = new WIA.DeviceManager();
            return manager.DeviceInfos.OfType<WIA.DeviceInfo>().FirstOrDefault(o => o.DeviceID == this.Id).Connect();
        }
    }
}
