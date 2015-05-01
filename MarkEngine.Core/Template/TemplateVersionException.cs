using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmrMarkEngine.Core.Template
{
    /// <summary>
    /// Template version exception
    /// </summary>
    public class TemplateVersionException : Exception
    {
        /// <summary>
        /// Create a new template version exception
        /// </summary>
        public TemplateVersionException(string templateVersion, string maxSupportedVersion) : base(String.Format("The supplied template was created a version not compatible with this version of OMR Mark Engine. The maximum version this engine supports is {0}, the template version is {1}", maxSupportedVersion, templateVersion))
        {
        }
    }
}
