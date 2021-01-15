// <copyright file="ImageExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet.Extensions
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Security.Cryptography;
    using System.Windows.Media.Imaging;
    using FullStack.Extensions.Crypto;
    using WpfLibrary.ControlSet.Models;

    /// <summary>
    /// Image extensions.
    /// </summary>
    internal static class ImageExtensions
    {
        /// <summary>
        /// Generates a bindable source from a custom bitmap.
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

        /// <summary>
        /// Generates a bindable source at full scale from original media item.
        /// </summary>
        /// <param name="media">The media item.</param>
        /// <returns>A bindable image.</returns>
        internal static BitmapImage ToSource(this MediaGalleryItem media)
        {
            var image = (BitmapImage)null;
            if (media?.Secure == false)
            {
                image = new BitmapImage(new System.Uri(media.Path));
            }
            else if (media?.Secure == true)
            {
                using var fs = File.OpenRead(media.Path);
                using var memStream = new MemoryStream();
                try
                {
                    fs.Churn(memStream, CryptoMode.Decrypt, media.Salt, media.Secret, 1000);
                }
                catch (CryptographicException cryEx)
                {
                    return cryEx.Message.StartsWith("Padding is invalid") ? null : throw cryEx;
                }

                image = new Bitmap(memStream)?.ToSource();
            }

            return image;
        }
    }
}
