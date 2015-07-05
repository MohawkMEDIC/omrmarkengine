/* 
 * Optical Mark Recognition 
 * Copyright 2015 Justin Fyfe
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

using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace OmrMarkEngine.Core.Processor
{
    /// <summary>
    /// Tool for processing images
    /// </summary>
    public class ScannedImage : IDisposable
    {

        // Backing field for image
        private Bitmap m_bitmap;
        private bool m_disposed = false;
        private Result[] m_barcodeResults;
        private Result[] m_markerCodes;
        private AForge.Point m_topLeft;
        private AForge.Point m_topRight;
        private AForge.Point m_bottomRight;
        private AForge.Point m_bottomLeft;

        /// <summary>
        /// Get the indicator that the form has necessary information 
        /// </summary>
        public bool IsScannable { get; private set; }

        /// <summary>
        /// Is ready for scanning?
        /// </summary>
        public bool IsReadyForScan { get; private set; }

        /// <summary>
        /// Template name
        /// </summary>
        public String TemplateName { get; private set; }

        /// <summary>
        /// Image
        /// </summary>
        public System.Drawing.Image Image { get { return this.m_bitmap; } }

        /// <summary>
        /// Marker codes
        /// </summary>
        public Result[] MarkerCodes
        {
            get
            {
                return this.m_markerCodes;
            }
        }

        /// <summary>
        /// Template parameters
        /// </summary>
        public String[] Parameters { get; private set; }

        /// <summary>
        /// Constructs the image processor from a file
        /// </summary>
        public ScannedImage(String fileName)
        {
            using (var img = System.Drawing.Image.FromFile(fileName))
            {
                this.m_bitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
                using (var g = Graphics.FromImage(this.m_bitmap))
                    g.DrawImage(img, 0, 0, img.Width, img.Height);
            }
        }

        /// <summary>
        /// Constructs an image process from an existing bitmap
        /// </summary>
        public ScannedImage(System.Drawing.Image img)
        {
            this.m_bitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(this.m_bitmap))
                g.DrawImage(img, 0, 0, img.Width, img.Height);
        }

        /// <summary>
        /// Performs the analysis on the image
        /// </summary>
        public void Analyze(bool thorough = true)
        {
            if (this.m_disposed)
                throw new ObjectDisposedException("ImageProcessor");

            if (this.IsScannable)
                return;

            LuminanceSource source = new BitmapLuminanceSource(this.m_bitmap);
            var binarizer = new HybridBinarizer(source);
            var binBitmap = new BinaryBitmap(binarizer);

            // Try to extract the form data
            var barReader = new ZXing.BarcodeReader();
            barReader.AutoRotate = true;
            barReader.Options.TryHarder = thorough;
            barReader.Options.PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.CODE_128 };

            this.m_barcodeResults = barReader.DecodeMultiple(source);

            // Look for barcode markers if possible
            if (this.m_barcodeResults != null)
            {
                this.m_markerCodes = this.m_barcodeResults.Where(o => o.Text.StartsWith("OMR:")).ToArray();
            }
            this.IsScannable = true;
            // Get the template data
            var markerCode = this.m_markerCodes == null ? null : this.m_markerCodes.FirstOrDefault(o=>o.Text.StartsWith("OMR:TL") || o.Text.StartsWith("OMR:ID"));

            // Get the guiding points by circles
            GrayscaleY grayFilter = new GrayscaleY();
            Threshold thresholdFilter = new Threshold(127);
            Invert invertFilter = new Invert();
            using (Bitmap searchImage = invertFilter.Apply(thresholdFilter.Apply(grayFilter.Apply(this.m_bitmap))))
            {

                // Blobs
                BlobCounter blobCounter = new BlobCounter();
                blobCounter.FilterBlobs = true;
                blobCounter.MinHeight = 30;
                blobCounter.MinWidth = 30;

                // Check for circles
                blobCounter.ProcessImage(searchImage);
                Blob[] blobs = blobCounter.GetObjectsInformation();
                SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
                List<AForge.Point> controlPoints = new List<AForge.Point>();
                int currentCheck = 35;
                while (currentCheck-- > 20 && controlPoints.Count != 4)
                {
                    controlPoints.Clear();
                    // Get the positions
                    foreach (var blob in blobs)
                    {
                        AForge.Point center = new AForge.Point();
                        float radius = 0;

                        if (shapeChecker.IsCircle(blobCounter.GetBlobsEdgePoints(blob), out center, out radius) &&
                            radius > currentCheck)
                        {
                            controlPoints.Add(center);
                        }
                    }
                }

                // Control points
                this.IsScannable &= controlPoints.Count == 4;
                if (!this.IsScannable)
                    return;

                // Now set markers
                this.m_topLeft = controlPoints[0]; //new AForge.Point(this.m_bitmap.Width + 10, this.m_bitmap.Height + 10);
                this.m_topRight = controlPoints[1];
                this.m_bottomLeft = controlPoints[2];
                this.m_bottomRight = controlPoints[3];

                // Find the right most bubble
                float rightMost = controlPoints.Select(o => o.X).Max(),
                    leftMost = controlPoints.Select(o => o.X).Min();
                // Organize those that are left/right
                AForge.Point[] lefties = controlPoints.Where(o => o.X < leftMost + (rightMost - leftMost) / 2).ToArray(),
                    righties = controlPoints.Where(o => o.X > leftMost + (rightMost - leftMost) / 2).ToArray();

                // HACK:
                if (lefties[0].Y < lefties[1].Y)
                {
                    this.m_topLeft = lefties[0];
                    this.m_bottomLeft = lefties[1];
                }
                else
                {
                    this.m_topLeft = lefties[1];
                    this.m_bottomLeft = lefties[0];
                }

                // HACK:
                if (righties[0].Y < righties[1].Y)
                {
                    this.m_topRight = righties[0];
                    this.m_bottomRight = righties[1];
                }
                else
                {
                    this.m_topRight = righties[1];
                    this.m_bottomRight = righties[0];
                }
            }

            if (!this.IsScannable)
                return;

            // Get the template data
            if (this.m_markerCodes != null && markerCode != null)
            {
                String[] templateData = markerCode.Text.Split(':');
                if (templateData.Length > 2)
                {
                    this.TemplateName = templateData[2];
                    if (templateData.Length > 3)
                        this.Parameters = templateData.Skip(3).ToArray();
                }
            }
        }

        /// <summary>
        /// Get binarized image
        /// </summary>
        public System.Drawing.Image GetBinarizedImage()
        {
            if (this.m_disposed)
                throw new ObjectDisposedException("ImageProcessor");
            if (!this.IsScannable)
                throw new InvalidOperationException("Form doesn't have sufficient control information to perform this operation");

            Grayscale grayFilter = new Grayscale(255, 255, 255);
            return grayFilter.Apply(this.m_bitmap);
        }

        /// <summary>
        /// Gets the corrected image. 
        /// </summary>
        /// <remarks>Done by looking for the barcodes with OMR::TL, etc.</remarks>
        public System.Drawing.Image GetCorrectedImage()
        {
            if (this.m_disposed)
                throw new ObjectDisposedException("ImageProcessor");
            if (!this.IsScannable)
                throw new InvalidOperationException("Form doesn't have sufficient control information to perform this operation");

            float dy = this.m_topRight.Y - this.m_topLeft.Y,
                dx = this.m_topRight.X - this.m_topLeft.X;
            double offsetAngle = Math.Tan(dy / dx);
            RotateBicubic rotate = new RotateBicubic(offsetAngle * (180 / Math.PI), false);
            return rotate.Apply(this.m_bitmap);
        }

        /// <summary>
        /// Get the form area from the raw image
        /// </summary>
        /// <returns></returns>
        public List<PointF> FormArea
        {
            get
            {
                if (this.m_disposed)
                    throw new ObjectDisposedException("ImageProcessor");
                return new List<PointF>()
                {
                    new PointF(this.m_topLeft.X, this.m_topLeft.Y),
                    new PointF(this.m_topRight.X, this.m_topRight.Y),
                    new PointF(this.m_bottomRight.X, this.m_bottomRight.Y),
                    new PointF(this.m_bottomLeft.X, this.m_bottomLeft.Y)
                };
            }
        }

        /// <summary>
        /// Dispose the image
        /// </summary>
        public void Dispose()
        {
            this.m_bitmap.Dispose();
            this.m_disposed = true;
        }

        /// <summary>
        /// Get the cropped image
        /// </summary>
        public System.Drawing.Image GetCroppedImage()
        {
            Crop crop = new Crop(new Rectangle((int)this.m_topLeft.X, (int)this.m_topLeft.Y, (int)(this.m_topRight.X - this.m_topLeft.X), (int)(this.m_bottomLeft.Y - this.m_topLeft.Y)));
            var bmp = new Bitmap(this.m_bitmap.Width, this.m_bitmap.Height, PixelFormat.Format24bppRgb);
            using(var g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                g.DrawImage(crop.Apply(this.m_bitmap), this.m_topLeft.X, this.m_topLeft.Y);
            }
            return bmp;

        }

        /// <summary>
        /// Prepare this image for processing
        /// </summary>
        public void PrepareProcessing()
        {
            if (this.m_disposed)
                throw new ObjectDisposedException("ImageProcessor");
            if (!this.IsScannable)
                throw new InvalidOperationException("Form doesn't have sufficient control information to perform this operation");

            // Rotate and re-analyze
            float dy = this.m_topRight.Y - this.m_topLeft.Y,
                dx = this.m_topRight.X - this.m_topLeft.X;

            double offsetAngle = Math.Tan(dy / dx);
            RotateBicubic rotate = new RotateBicubic(offsetAngle * (180 / Math.PI), false);
            var bmp = rotate.Apply(this.m_bitmap);
            this.m_bitmap.Dispose();
            this.m_bitmap = bmp;
            this.IsScannable = false;
            this.Analyze(true);
            
            // crop
            Crop crop = new Crop(new Rectangle((int)this.m_topLeft.X, (int)this.m_topLeft.Y, (int)(this.m_topRight.X - this.m_topLeft.X), (int)(this.m_bottomLeft.Y - this.m_topLeft.Y)));
            bmp = new Bitmap((int)(this.m_topRight.X - this.m_topLeft.X), (int)(this.m_bottomLeft.Y - this.m_topLeft.Y), PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, this.m_bitmap.Width, this.m_bitmap.Height);
                g.DrawImage(crop.Apply(this.m_bitmap), 0, 0);
            }

            this.m_bitmap.Dispose();
            this.m_bitmap = bmp;
            this.IsReadyForScan = true;
        }
        
    }
}
