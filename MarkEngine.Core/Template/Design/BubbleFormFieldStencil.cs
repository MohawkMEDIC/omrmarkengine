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

using FyfeSoftware.Sketchy.Core.Primitives.Stencils;
using FyfeSoftware.Sketchy.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrMarkEngine.Template.Design
{
    /// <summary>
    /// bubble field stencil
    /// </summary>
    [Serializable]
    public class BubbleFormFieldStencil : AbstractInteractiveStencil
    {

        /// <summary>
        /// Field for bubble 
        /// </summary>
        private OmrBubbleField m_field;

        /// <summary>
        /// Move only
        /// </summary>
        public override FyfeSoftware.Sketchy.Core.SizeModeType AllowedSizing
        {
            get
            {
                return FyfeSoftware.Sketchy.Core.SizeModeType.Horizontal | FyfeSoftware.Sketchy.Core.SizeModeType.Vertical;
            }
        }

        // True/false shape
        private RectangleShape m_bubbleShape = new RectangleShape()
        {
            FillBrush = new SolidBrush(Color.FromArgb(127, 0, 0, 255)),
            OutlineColor = Color.Blue,
            OutlineWidth = 4, 
            OutlineStyle = System.Drawing.Drawing2D.DashStyle.Solid,
            Position = new PointF(2,2),
            Size = new SizeF(40, 40)
        };
        private TextShape m_answerShape = new TextShape()
        {
            FillBrush = Brushes.Black,
            Position = new PointF(4, 4),
            Font = new Font(FontFamily.GenericSansSerif, 16.0f)
        };

        /// <summary>
        /// True/false form field stencil
        /// </summary>
        public BubbleFormFieldStencil(OmrBubbleField field)
        {
            this.Tag = this.m_field = field;
            this.Size = new SizeF(48, 48);
            this.m_answerShape.Text = field.Value;

            this.Add(m_bubbleShape);
            this.Add(m_answerShape);

            this.PositionChanged += BubbleFormFieldStencil_PositionChanged;
            this.SizeChanged += BubbleFormFieldStencil_SizeChanged;
            this.m_field.PropertyChanged += m_field_PropertyChanged;

        }

        /// <summary>
        /// Property changed
        /// </summary>
        void m_field_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.m_answerShape.Text = this.m_field.Value;
            if(this.Site != null)
                this.Site.Name = this.m_field.Id;
        }

        /// <summary>
        /// Size changed
        /// </summary>
        void BubbleFormFieldStencil_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateTemplate();
        }

        /// <summary>
        /// Position changed
        /// </summary>
        void BubbleFormFieldStencil_PositionChanged(object sender, EventArgs e)
        {
            this.UpdateTemplate();
        }

        /// <summary>
        /// Update the position
        /// </summary>
        private void UpdateTemplate()
        {
            this.m_field.TopLeft = this.Position;
            this.m_field.TopRight = new PointF(this.Position.X + this.Size.Width, this.Position.Y);
            this.m_field.BottomRight = new PointF(this.Position.X + this.Size.Width, this.Position.Y + this.Size.Height);
            this.m_field.BottomLeft = new PointF(this.Position.X, this.Position.Y + this.Size.Height);

        }

        /// <summary>
        /// Editor
        /// </summary>
        public override FyfeSoftware.Sketchy.Core.IShapeEditor Editor
        {
            get { return null; ; }
        }

        /// <summary>
        /// End of edit
        /// </summary>
        public override void HandleEndEdit(FyfeSoftware.Sketchy.Core.ShapeEditEventArgs e)
        {
        }
    }
}
