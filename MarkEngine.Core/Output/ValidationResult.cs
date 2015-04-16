using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmrMarkEngine.Output
{
    /// <summary>
    /// Validation Result
    /// </summary>
    public class ValidationResult
    {

        /// <summary>
        /// Validation result ctor
        /// </summary>
        public ValidationResult()
        {
            this.Issues = new List<string>();
        }

        /// <summary>
        /// True if valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// The issues
        /// </summary>
        public List<String> Issues { get; set; }
    }
}
