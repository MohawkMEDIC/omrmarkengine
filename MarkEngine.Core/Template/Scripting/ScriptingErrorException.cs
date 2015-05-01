using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrMarkEngine.Template.Scripting
{
    /// <summary>
    /// Scripting error exception
    /// </summary>
    public class ScriptingErrorException : Exception
    {

        /// <summary>
        /// Scripting error exception
        /// </summary>
        public ScriptingErrorException(string message) : base(message)
        {

        }
    }
}
