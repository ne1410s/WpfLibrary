// <copyright file="ImageExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet.Extensions
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Image extensions.
    /// </summary>
    internal static class ImageExtensions
    {
        /// <summary>
        /// Reads the bindable source.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>A bindable image.</returns>
        internal static BitmapImage ToSource(this Bitmap bitmap)
        {
            var image = (BitmapImage)null;
            if (bitmap != null)
            {
                using var memStream = new MemoryStream();
                bitmap.Save(memStream, ImageFormat.Jpeg);
                memStream.Position = 0;

                image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = memStream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
            }

            return image;
        }
    }
}
