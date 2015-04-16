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
