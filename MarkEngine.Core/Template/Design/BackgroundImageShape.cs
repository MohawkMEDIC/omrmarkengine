using FyfeSoftware.Sketchy.Core.Shapes;
using OmrMarkEngine.Template;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkerEngine.Template.Design
{
    /// <summary>
    /// Represents a shape with an image
    /// </summary>
    [Serializable]
    public class BackgroundImageShape : AbstractStyledShape
    {

        // Omr template
        private OmrTemplate m_template;

        /// <summary>
        /// The background image shape ctor
        /// </summary>
        public BackgroundImageShape(OmrTemplate template)
        {
            this.m_template = template;
            if (System.IO.File.Exists(template.SourcePath))
                this.Image = Image.FromFile(template.SourcePath);
            else
                this.Image = (Image)new Bitmap((int)template.BottomRight.X, (int)template.BottomRight.Y, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            this.m_template.PropertyChanged += m_template_PropertyChanged;
            this.Tag = this.m_template;
        }

        /// <summary>
        /// Template property has changed
        /// </summary>
        void m_template_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "SourcePath")
            {
                this.Image.Dispose();
                this.Image = Image.FromFile(this.m_template.SourcePath);
            }
            else if (e.PropertyName == "ImageSource")
            {
                this.Image.Dispose();
                this.Image = this.m_template.ImageSource;
            }
        }

        // Image backing field
        [NonSerialized]
        private Image m_image;

        /// <summary>
        /// The image
        /// </summary>
        public Image Image {
            get
            {
                return this.m_image;
            } 
            set
            {
                this.m_image = value;
                this.Position = new PointF(0, 0);
                this.Size = this.m_image.Size;

                // Redraw on change
                if(this.GetCanvas() != null)
                    this.GetCanvas().Invalidate();
            }
        }

        /// <summary>
        /// Sizing mode
        /// </summary>
        public override FyfeSoftware.Sketchy.Core.SizeModeType AllowedSizing
        {
            get
            {
                return FyfeSoftware.Sketchy.Core.SizeModeType.None;
            }
        }

        /// <summary>
        /// Draw to the output
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public override bool DrawTo(Graphics g)
        {
            if(this.GetCanvas().Size != this.m_size)
                this.GetCanvas().Size = this.m_image.Size;

            g.DrawImage(this.m_image, this.DrawPosition.X, this.DrawPosition.Y, this.DrawSize.Width, this.DrawSize.Height);
            return true;
        }

    }
}
