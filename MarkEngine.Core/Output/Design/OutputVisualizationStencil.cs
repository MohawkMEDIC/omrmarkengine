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
 * Date: 4-20-2015
 */

using FyfeSoftware.Sketchy.Core.Primitives;
using FyfeSoftware.Sketchy.Core.Shapes;
using OmrMarkEngine.Output;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrMarkEngine.Output.Design
{
    /// <summary>
    /// A stencil which is responsible for visualizing an output
    /// </summary>
    public class OutputVisualizationStencil : AbstractStencil
    {

        // The data which is being visualized
        private OmrPageOutput m_data;

        /// <summary>
        /// Get the allowed sizing modes
        /// </summary>
        public override FyfeSoftware.Sketchy.Core.SizeModeType AllowedSizing
        {
            get
            {
                return FyfeSoftware.Sketchy.Core.SizeModeType.None;
            }
        }

        /// <summary>
        /// The output visualizer
        /// </summary>
        public OutputVisualizationStencil(OmrPageOutput pageOutput)
        {
            this.m_data = pageOutput;
            // Find the bounds and that is our size
            this.Size = new System.Drawing.SizeF(pageOutput.BottomRight.X, pageOutput.BottomRight.Y);
            this.Position = new System.Drawing.PointF(0, 0);
            this.DrawItems(pageOutput.Details);

            this.Add(new TextShape()
            {
                FillBrush = Brushes.White,
                Font = new Font(FontFamily.GenericSansSerif, 16f, FontStyle.Bold),
                Position = new PointF(0,0),
                Text = String.Format("Scan ID: {0}", pageOutput.Id)
            });
            
            
        }

        /// <summary>
        /// Draw items
        /// </summary>
        private void DrawItems(List<OmrOutputData> details)
        {
            foreach (var dtl in details)
            {
                if (dtl is OmrOutputDataCollection)
                    this.DrawItems((dtl as OmrOutputDataCollection).Details);
                else
                {
                    var blotch = new RectangleShape()
                    {
                        Position = dtl.TopLeft,
                        Size = new System.Drawing.SizeF(dtl.BottomRight.X - dtl.TopLeft.X, dtl.BottomRight.Y - dtl.TopLeft.Y),
                        FillBrush = new SolidBrush(Color.FromArgb(127, Color.Green)),
                        OutlineColor = Color.DarkGreen,
                        OutlineStyle = System.Drawing.Drawing2D.DashStyle.Solid,
                        OutlineWidth = 2
                    };
                    this.Add(blotch);
                }
            }
        }

        
        
    }
}
