// <copyright file="MediaGalleryItem.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet.Models
{
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media.Imaging;
    using SecureMedia.Core.Models;

    /// <summary>
    /// Item for media.
    /// </summary>
    internal class MediaGalleryItem : MediaInfo
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MediaGalleryItem"/>
        /// class.
        /// </summary>
        /// <param name="media">A media item.</param>
        public MediaGalleryItem(MediaInfo media)
        {
            this.Id = media.Id;
            this.Name = media.Name;
            this.Type = media.Type;
            this.Path = media.Path;
            this.FileSize = media.FileSize;
            this.Secure = media.Secure;
            this.Thumb = media.Thumb;

            this.ReadThumbSource();
        }

        /// <summary>
        /// Gets a bindable source for the thumbnail.
        /// </summary>
        public BitmapImage ThumbSource { get; private set; }

        private void ReadThumbSource()
        {
            if (this.Thumb != null && this.ThumbSource == null)
            {
                using var memStream = new MemoryStream();
                this.Thumb.Save(memStream, ImageFormat.Png);
                memStream.Position = 0;

                this.ThumbSource = new BitmapImage();
                this.ThumbSource.BeginInit();
                this.ThumbSource.StreamSource = memStream;
                this.ThumbSource.CacheOption = BitmapCacheOption.OnLoad;
                this.ThumbSource.EndInit();
                this.ThumbSource.Freeze();
            }
        }
    }
}
