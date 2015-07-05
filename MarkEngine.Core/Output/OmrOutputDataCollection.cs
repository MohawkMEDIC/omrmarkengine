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
 * Date: 4-16-2015
 */

using OmrMarkEngine.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Output
{

    /// <summary>
    /// OMR Data Collection
    /// </summary>
    [XmlType("OmrOutputDataCollection", Namespace = "urn:scan-omr:analysis")]
    public abstract class OmrOutputDataCollection : OmrOutputData
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OmrOutputDataCollection ()
	    {
                this.Details = new List<OmrOutputData>();
	    }

        /// <summary>
        /// OMR Field Row
        /// </summary>
        [XmlElement("row", typeof(OmrRowData))]
        [XmlElement("barcode", typeof(OmrBarcodeData))]
        [XmlElement("bubble", typeof(OmrBubbleData))]
        [XmlElement("aggregate", typeof(OmrAggregateDataOutput))]
        public List<OmrOutputData> Details { get; set; }

        /// <summary>
        /// Already answered?
        /// </summary>
        public bool AlreadyAnswered(OmrOutputData data)
        {
            if (!(data is OmrBubbleData)) return false;

            var bubble = data as OmrBubbleData;
            var existing = this.Details.OfType<OmrBubbleData>().FirstOrDefault(o => o.Key == bubble.Key);
            if (existing == null)
                return false;
            else if (existing.BlobArea < bubble.BlobArea)
                this.Details.Remove(existing);
            else
                return true;
            return false;
        }


        /// <summary>
        /// Validates the input against the template
        /// </summary>
        public virtual ValidationResult Validate(OmrTemplate template)
        {
            ValidationResult retVal = new ValidationResult();
            retVal.IsValid = true;
            // Row ID
            String rowId = null;
            if (this is OmrRowData)
                rowId = (this as OmrRowData).Id;
            else if (this is OmrAggregateDataOutput)
                rowId = (this as OmrAggregateDataOutput).RowId;

            List<OmrQuestionField> requiredFields = new List<OmrQuestionField>(template.Fields.Where(o => o.IsQuestionRequired && o.AnswerRowGroup == rowId));

            // Validate
            foreach (var itm in this.Details)
            {
                if (itm is OmrOutputDataCollection)
                {
                    var sub = (itm as OmrOutputDataCollection).Validate(template);
                    retVal.IsValid &= sub.IsValid;
                    retVal.Issues.AddRange(sub.Issues);
                }
                else if (itm is OmrBarcodeData)
                {
                    // First find the template data
                    var templateData = template.Fields.Find(o => o.Id == itm.Id && o.AnswerRowGroup == rowId);
                    if (templateData == null)
                    {
                        retVal.IsValid = false;
                        retVal.Issues.Add(String.Format("Missing template data for {0}", itm.Id));
                    }
                    requiredFields.Remove(templateData);
                }
                else if (itm is OmrBubbleData)
                {
                    var bubbleData = itm as OmrBubbleData;
                    var templateData = template.Fields.Find(o => o.Id == itm.Id && o.AnswerRowGroup == rowId) as OmrBubbleField;
                    var otherOptions = template.Fields.FindAll(o => o is OmrBubbleField && (o as OmrBubbleField).Question == bubbleData.Key && o.AnswerRowGroup == rowId);

                    // Mutli not permitted
                    if (templateData.Behavior == BubbleBehaviorType.One &&
                        this.Details.OfType<OmrBubbleData>().Count(o => otherOptions.Exists(p => p.Id == o.Id)) != 1)
                        {
                            retVal.IsValid = false;
                            retVal.Issues.Add(String.Format("Only one response for  {0} can be registered", bubbleData.Key));
                        }
                    
                    // Remove all from required fields
                    requiredFields.RemoveAll(o => o is OmrBubbleField && (o as OmrBubbleField).Question == templateData.Question);

                }
            }

            // How many remaining required fields remain, is it the number of total required fields in the row?
            // if so then the row is valid because nothing was filled out
            if(rowId != null)
            {
                var uniqueQuestionCountInTpl = template.Fields.Where(o => o.AnswerRowGroup == rowId && o.IsQuestionRequired).OfType<OmrBubbleField>().Select(o => o.Question).Distinct();
                var uniqueQuestionCountInReq = requiredFields.OfType<OmrBubbleField>().Select(o => o.Question).Distinct();
                if(uniqueQuestionCountInReq.Count() != 0 && uniqueQuestionCountInReq.Count() != uniqueQuestionCountInTpl.Count())
                {
                    retVal.IsValid = false;
                    String flds = String.Empty;
                    var distinctFields = requiredFields.Select(o=>o is OmrBubbleField ? (o as OmrBubbleField).Question : o.Id).Distinct().ToList();
                    distinctFields.ForEach(o => flds = String.Format("{0} {1},", flds, o));
                    retVal.Issues.Add(String.Format("Missing required fields ({1}) for row {0}", rowId, flds));
                }

            }
            else
                retVal.IsValid &= requiredFields.Count == 0;
            
            return retVal;
        }

    }
}
