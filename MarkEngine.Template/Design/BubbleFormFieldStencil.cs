using FyfeSoftware.Sketchy.Core.Primitives.Stencils;
using FyfeSoftware.Sketchy.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkEngine.Template.Design
{
    /// <summary>
    /// bubble field stencil
    /// </summary>
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
                return FyfeSoftware.Sketchy.Core.SizeModeType.MoveOnly;
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
