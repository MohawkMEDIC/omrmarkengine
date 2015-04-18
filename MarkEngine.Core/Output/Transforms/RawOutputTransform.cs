using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OmrMarkEngine.Output.Transforms;

namespace OmrMarkEngine.Output.Transforms
{
    /// <summary>
    /// RAW Output Transform
    /// </summary>
    public class RawOutputTransform : IOutputTransform
    {
        /// <summary>
        /// Raw output transform
        /// </summary>
        public string Name
        {
            get { return "Raw Mark Engine Output"; }
        }

        /// <summary>
        /// Raw output extension
        /// </summary>
        public string Extension
        {
            get { return "omr.xml"; }
        }

        /// <summary>
        /// Transform
        /// </summary>
        public byte[] Transform(Template.OmrTemplate template, OmrPageOutputCollection pages)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                new XmlSerializer(typeof(OmrPageOutputCollection)).Serialize(ms, pages);
                ms.Flush();
                return ms.ToArray();
            }

        }
    }
}
