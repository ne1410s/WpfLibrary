// <copyright file="MediaGalleryItem.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet.Models
{
    using System.Windows.Media.Imaging;
    using SecureMedia.Core.Models;

    /// <summary>
    /// Item for media.
    /// </summary>
    public class MediaGalleryItem : MediaInfo
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MediaGalleryItem"/>
        /// class.
        /// </summary>
        /// <param name="media">A media item.</param>
        /// <param name="secret">Optional secret.</param>
        public MediaGalleryItem(MediaInfo media, byte[] secret = null)
        {
            this.Salt = media.Salt;
            this.Name = media.Name;
            this.Type = media.Type;
            this.Path = media.Path;
            this.FileSize = media.FileSize;
            this.Secure = media.Secure;
            this.Secret = secret;
        }

        /// <summary>
        /// Gets or sets a bindable source for the thumbnail.
        /// </summary>
        public BitmapImage ThumbSource { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        public byte[] Secret { get; set; }
    }
}
