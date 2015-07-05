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

using OmrMarkEngine.Core.Processor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OmrMarkEngine.Template
{
    /// <summary>
    /// Bound object
    /// </summary>
    [XmlType("OmrBoundObject", Namespace = "urn:scan-omr:template")]
    public abstract class OmrBoundObject : OmrIdentifiedObject
    {
        // Backing fields
        private PointF m_topLeft;
        private PointF m_topRight;
        private PointF m_bottomRight;
        private PointF m_bottomLeft;

        /// <summary>
        /// Gets or sets the top left corner
        /// </summary>
        [XmlIgnore]
        public PointF TopLeft {
            get { return this.m_topLeft; }
            set
            {
                this.m_topLeft = value;
                base.OnPropertyChange("TopLeft");
            }
        }

        /// <summary>
        /// Gets or sets the top right
        /// </summary>
        [XmlIgnore]
        public PointF TopRight {
            get { return this.m_topRight; }
            set
            {
                this.m_topRight = value;
                base.OnPropertyChange("TopRight");
            }
        }

        /// <summary>
        /// Gets or sets the bottom right
        /// </summary>
        [XmlIgnore]
        public PointF BottomRight {
            get { return this.m_bottomRight; }
            set
            {
                this.m_bottomRight = value;
                base.OnPropertyChange("BottomRight");
            }
        }

        /// <summary>
        /// GEts or sets the bottom left
        /// </summary>
        [XmlIgnore]
        public PointF BottomLeft {
            get { return this.m_bottomLeft; }
            set
            {
                this.m_bottomLeft = value;
                base.OnPropertyChange("BottomLeft");
            }

        }

        /// <summary>
        /// Gets the topleft in the image
        /// </summary>
        [XmlAttribute("topLeft")]
        [Browsable(false)]
        public String XmlTopLeft
        {
            get
            {
                return string.Format("{0},{1}", this.TopLeft.X, this.TopLeft.Y);
            }
            set
            {
                var comps = value.Split(',');
                this.TopLeft = new PointF(float.Parse(comps[0]), float.Parse(comps[1]));
            }
        }

        /// <summary>
        /// Gets the bottomleft in the image
        /// </summary>
        [XmlAttribute("bottomLeft")]
        [Browsable(false)]
        public String XmlBottomLeft
        {
            get
            {
                return string.Format("{0},{1}", this.BottomLeft.X, this.BottomLeft.Y);
            }
            set
            {
                var comps = value.Split(',');
                this.BottomLeft = new PointF(float.Parse(comps[0]), float.Parse(comps[1]));
            }
        }

        /// <summary>
        /// Gets the topright
        /// </summary>
        [XmlAttribute("topRight")]
        [Browsable(false)]
        public String XmlTopRight
        {
            get
            {
                return string.Format("{0},{1}", this.TopRight.X, this.TopRight.Y);
            }
            set
            {
                var comps = value.Split(',');
                this.TopRight = new PointF(float.Parse(comps[0]), float.Parse(comps[1]));
            }
        }

        /// <summary>
        /// Gets the bottom right
        /// </summary>
        [XmlAttribute("bottomRight")]
        [Browsable(false)]
        public String XmlBottomRight
        {
            get
            {
                return string.Format("{0},{1}", this.BottomRight.X, this.BottomRight.Y);
            }
            set
            {
                var comps = value.Split(',');
                this.BottomRight = new PointF(float.Parse(comps[0]), float.Parse(comps[1]));
            }
        }

        /// <summary>
        /// Process image data
        /// </summary>
        /// <param name="image"></param>
        protected override void ProcessImageData(ScannedImage image)
        {
            base.ProcessImageData(image);
            if (image.IsScannable)
            {
                this.m_topLeft = image.FormArea[0];
                this.m_topRight = image.FormArea[1];
                this.m_bottomRight = image.FormArea[2];
                this.m_bottomLeft = image.FormArea[3];
            }
        }
    }
}
