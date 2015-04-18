using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmrMarkEngine.Template;

namespace OmrMarkEngine.Output.Transforms
{
    /// <summary>
    /// CSV Output Transform
    /// </summary>
    public class CsvOutputTransform : IOutputTransform
    {
        /// <summary>
        /// The name of the transform
        /// </summary>
        public string Name
        {
            get { return "Comma separated values"; }
        }

        /// <summary>
        /// Extension
        /// </summary>
        public string Extension
        {
            get { return "csv"; }
        }

        /// <summary>
        /// Transform the output
        /// </summary>
        public byte[] Transform(Template.OmrTemplate template, OmrPageOutputCollection pages)
        {
            // CSV output is formatted as follows:
            //                 BARCODE0    BARCODE1    BARCODEN   BUBBLEQ1   BUBBLEQ2    BUBBLEQ3
            // PAGE1     ROW1
            // PAGE1     ROW2
            // PAGE1     ROWN
            // PAGE2     ROW1
            // PAGE2     ROW2
            // PAGE2     ROWN
            // PAGEN

            using(MemoryStream ms = new MemoryStream())
            {
                using(StreamWriter sw = new StreamWriter(ms))
                {
                    // Get the headings
                    var barcodeGroup= template.FlatFields.OfType<OmrBarcodeField>().GroupBy(o => o.AnswerRowGroup).FirstOrDefault();
                    int nBarcodesPerRow = barcodeGroup.Count();
                    var questionsPerRow = template.FlatFields.OfType<OmrBubbleField>().Select(o => o.Question).Distinct().ToArray();
                    // Output header row
                    sw.Write("PAGE,ROW");
                    for (int i = 0; i < nBarcodesPerRow; i++)
                        sw.Write(",BARCODE{0}", i);
                    foreach (var q in questionsPerRow)
                        sw.Write(",{0}", q);
                    sw.WriteLine();

                    foreach(var pg in pages.Pages)
                    {
                        foreach(var rw in pg.Details.OfType<OmrRowData>())
                        {
                            sw.Write("{0},{1}", pg.Id, rw.Id);
                            OmrBarcodeData[] barcodes = rw.Details.OfType<OmrBarcodeData>().ToArray();

                            // Barcode
                            for(int i = 0; i < nBarcodesPerRow; i++)
                            {
                                if (barcodes.Length > i)
                                    sw.Write(",\"{0}\"", barcodes[i].BarcodeData);
                                else
                                    sw.Write(",");
                            }

                            // Questions
                            foreach(var q in questionsPerRow)
                            {
                                OmrBubbleData[] answerBubble = rw.Details.OfType<OmrBubbleData>().Where(o=>o.Key == q).ToArray();
                                OmrAggregateDataOutput[] aggregate = rw.Details.OfType<OmrAggregateDataOutput>().Where(o=>o.Id == q).ToArray();
                                OmrBubbleData[] nonGroupedAnswer = pg.Details.OfType<OmrBubbleData>().Where(o => o.Key == q).ToArray();

                                if (aggregate.Length > 0)
                                    sw.Write(",\"{0}\"", this.MakeString(aggregate));
                                else if (answerBubble.Length > 0)
                                    sw.Write(",\"{0}\"", this.MakeString(answerBubble));
                                else if (nonGroupedAnswer.Length > 0)
                                    sw.Write(",\"{0}\"", this.MakeString(nonGroupedAnswer));
                                else
                                    sw.Write(",");
                            }

                            sw.WriteLine();
                        }
                    }
                }

                return ms.ToArray();
            }
            // Get the header
        }

        /// <summary>
        /// Make a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="answer"></param>
        /// <returns></returns>
        public String MakeString<T>(T[] answer)
        {
            StringBuilder retVal = new StringBuilder();
            foreach (var t in answer)
                retVal.AppendFormat("{0}|", t);
            retVal.Remove(retVal.Length - 1, 1);
            return retVal.ToString();
        }
    }
}
