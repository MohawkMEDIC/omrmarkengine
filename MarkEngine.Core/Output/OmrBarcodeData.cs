using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// Barcode data
    /// </summary>
    [XmlType("OmrBarcodeData", Namespace = "urn:scan-omr:analysis")]
    public class OmrBarcodeData : OmrOutputData
    {

        /// <summary>
        /// The format of the barcode
        /// </summary>
        [XmlAttribute("format")]
        public ZXing.BarcodeFormat Format { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [XmlAttribute("data")]
        public String BarcodeData { get; set; }


    }
}
