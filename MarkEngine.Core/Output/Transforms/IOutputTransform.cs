using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmrMarkEngine.Template;

namespace OmrMarkEngine.Output.Transforms
{
    /// <summary>
    /// Output transform interface
    /// </summary>
    public interface IOutputTransform
    {
        /// <summary>
        /// Name of the output transform
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Extension of the output transform
        /// </summary>
        String Extension { get; }

        /// <summary>
        /// Perform the transform
        /// </summary>
        byte[] Transform(OmrTemplate template, OmrPageOutputCollection pages);
    }
}
