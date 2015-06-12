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

using FyfeSoftware.Sketchy.Core;
using FyfeSoftware.Sketchy.Core.Primitives;
using FyfeSoftware.Sketchy.Core.Primitives.Stencils;
using FyfeSoftware.Sketchy.Core.Shapes;
using OmrMarkEngine.Template;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrMarkEngine.Template.Design
{
    /// <summary>
    /// Scannable area stencil
    /// </summary>
    public class CornerAnchorShape : AbstractInteractiveStencil
    {

        /// <summary>
        /// Return null
        /// </summary>
        public override IShapeEditor Editor
        {
            get { return null; }
        }

        /// <summary>
        /// End of edit
        /// </summary>
        public override void HandleEndEdit(ShapeEditEventArgs e)
        {
            return;
        }

        /// <summary>
        /// Allowed sizing
        /// </summary>
        public override SizeModeType AllowedSizing
        {
            get
            {
                return SizeModeType.None;
            }
        }

        // The template
        private PointF m_point;
        private RectangleShape m_markerBox;
        private TextShape m_labelShape;

        /// <summary>
        /// Scan area stencil
        /// </summary>
        public CornerAnchorShape(PointF point, String label)
        {
            this.Tag = this.m_point = point;
            this.Position = new PointF(point.X - 15, point.Y - 15);
            this.Size = new SizeF(30,30);
            this.m_markerBox = new RectangleShape()
            {
                Size = new SizeF(24,24),
                Position = new PointF(3,3),
                FillBrush = new SolidBrush(Color.FromArgb(127,255,255,0)),
                OutlineColor = Color.Orange,
                OutlineWidth = 5,
                OutlineStyle = System.Drawing.Drawing2D.DashStyle.Solid
            };
            this.m_labelShape = new TextShape()
            {
                FillBrush = Brushes.Black,
                Font = new Font(SystemFonts.CaptionFont, FontStyle.Bold),
                Position = new PointF(5,5),
                Text = label
            };

            this.Add(this.m_markerBox);
            this.Add(this.m_labelShape);

        }


    }
}
