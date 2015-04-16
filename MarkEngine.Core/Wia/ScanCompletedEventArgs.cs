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
