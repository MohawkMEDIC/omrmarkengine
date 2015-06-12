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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmrMarkEngine.Template.Design
{
    /// <summary>
    /// Barcode form field stencil
    /// </summary>
    [Serializable]
    public class BarcodeFormFieldStencil : AbstractInteractiveStencil
    {

        // The field to which this belongs
        [NonSerialized]
        private OmrBarcodeField m_field;
        [NonSerialized]
        private TextShape m_textShape;
        [NonSerialized]
        private ImageShape m_iconShape;
        [NonSerialized]
        private RectangleShape m_containerShape;

        /// <summary>
        /// Gets the editor 
        /// </summary>
        public override FyfeSoftware.Sketchy.Core.IShapeEditor Editor
        {
            get { return null; }
        }

        /// <summary>
        /// Allowed sizing
        /// </summary>
        public override FyfeSoftware.Sketchy.Core.SizeModeType AllowedSizing
        {
            get
            {
                return FyfeSoftware.Sketchy.Core.SizeModeType.Horizontal | FyfeSoftware.Sketchy.Core.SizeModeType.Vertical;
            }
        }

        /// <summary>
        /// Handle edit
        /// </summary>
        /// <param name="e"></param>
        public override void HandleEndEdit(FyfeSoftware.Sketchy.Core.ShapeEditEventArgs e)
        {
            return;
        }

        /// <summary>
        /// Barcode form field stencil
        /// </summary>
        public BarcodeFormFieldStencil(OmrBarcodeField field)
        {

            this.Tag = this.m_field = field;
            this.Size = new SizeF(200, 96);
            
            // Container shape
            this.m_containerShape = new RectangleShape()
            {
                FillBrush = new SolidBrush(Color.FromArgb(127, Color.Gainsboro)),
                Position = new PointF(0,0),
                ShadowBrush = new SolidBrush(Color.FromArgb(127,0 ,0,0)),
                Size = new SizeF(this.Size.Width , this.Size.Height )
            };
            this.m_textShape = new TextShape()
            {
                Alignment = StringAlignment.Center,
                FillBrush = Brushes.Black,
                Text = field.Id,
                Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 16)
            };
            this.m_textShape.Position = new PointF((this.Size.Width - this.m_textShape.Size.Width) / 2, (this.Size.Height - 32) / 2 + 16);

            this.m_iconShape = new ImageShape()
            {
                Position = new PointF((this.Size.Width - 32) / 2, 5),
                Size = new SizeF(32, 32),
                AutoScale = false,
                Image = Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("OmrMarkEngine.Core.Template.Resources.view-barcode.png"))
            };

            this.Add(this.m_containerShape, "container");
            this.Add(this.m_iconShape, "image");
            this.Add(this.m_textShape);

            this.m_field.PropertyChanged += m_field_PropertyChanged;
            this.SizeChanged += BarcodeFormFieldStencil_SizeChanged;
            this.PositionChanged += BarcodeFormFieldStencil_PositionChanged;
        }

        /// <summary>
        /// Position has changed
        /// </summary>
        void BarcodeFormFieldStencil_PositionChanged(object sender, EventArgs e)
        {
            this.UpdateTemplate();
        }

        /// <summary>
        /// Size has changed
        /// </summary>
        void BarcodeFormFieldStencil_SizeChanged(object sender, EventArgs e)
        {
            this.m_textShape.Position = new PointF((this.Size.Width - this.m_textShape.Size.Width) / 2, (this.Size.Height - 32) / 2 + 16);
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
            this.m_field.BottomLeft = new PointF(this.Position.X , this.Position.Y + this.Size.Height );

        }

        /// <summary>
        /// Property has change
        /// </summary>
        void m_field_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.m_textShape.Text = this.m_field.Id;
            if(this.Site != null)
                this.Site.Name = this.m_field.Id;
        }


        /// <summary>
        /// Set the size
        /// </summary>
        public override SizeF Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (value.Width < 200 || value.Height < 96)
                    return;
                base.Size = value;
            }
        }

        
    }
}
