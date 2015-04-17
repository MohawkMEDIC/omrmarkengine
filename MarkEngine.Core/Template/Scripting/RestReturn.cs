using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OmrMarkEngine.Template.Scripting.Util
{
    /// <summary>
    /// Return class
    /// </summary>
    [DataContract]
    public class RestReturn
    {
        // Identifier
        [DataMember(Name = "id")]
        public Int32 Id { get; set; }
    }
}
