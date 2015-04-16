using OmrMarkEngine.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrScannerApplication.Sync
{
    /// <summary>
    /// Synchronizer
    /// </summary>
    public interface ISynchronizer
    {
        /// <summary>
        /// Sync the data on the page
        /// </summary>
        void Sync(OmrPageOutput page);
    }
}
