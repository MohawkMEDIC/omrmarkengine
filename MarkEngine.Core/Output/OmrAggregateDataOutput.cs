/* 
 * Optical Mark Recognition 
 * Copyright 2015, Justin Fyfe
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * Author: Justin
 * Date: 4-18-2015
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{

    /// <summary>
    /// Aggregation function
    /// </summary>
    public enum AggregationFunction
    {
        Sum,
        Min,
        Max,
        Count
    }

    /// <summary>
    /// OMR Aggregate data output
    /// </summary>
    [XmlType("OmrAggregateDataOutput", Namespace = "urn:scan-omr:analysis")]
    public class OmrAggregateDataOutput : OmrOutputDataCollection
    {

        /// <summary>
        /// The row to which the OMR output data belongs
        /// </summary>
        [XmlAttribute("rowId")]
        public String RowId { get; set; }

        /// <summary>
        /// The aggregate value
        /// </summary>
        [XmlAttribute("value")]
        public int AggregateValue
        {
            get
            {
                switch(this.Function)
                {
                    case AggregationFunction.Count:
                        return this.Details.Count;
                    case AggregationFunction.Sum:
                        return (int)this.Details.OfType<OmrBubbleData>().Sum(o => o.ValueAsFloat);
                    case AggregationFunction.Min:
                        return (int)this.Details.OfType<OmrBubbleData>().Min(o => o.ValueAsFloat);
                    case AggregationFunction.Max:
                        return (int)this.Details.OfType<OmrBubbleData>().Max(o => o.ValueAsFloat);
                    default:
                        throw new InvalidOperationException("Cannot perform aggregation function");
                }
            }
            set { }
        }

        /// <summary>
        /// Gets or sets the aggregation function
        /// </summary>
        [XmlAttribute("function")]
        public AggregationFunction Function { get; set; }

        /// <summary>
        /// Aggregate value tostring
        /// </summary>
        public override string ToString()
        {
            return this.AggregateValue.ToString();
        }
    }
}
