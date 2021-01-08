// <copyright file="MediaGalleryItem.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet.Models
{
    using System;

    /// <summary>
    /// Media type.
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// Audio type.
        /// </summary>
        Audio,

        /// <summary>
        /// Image type.
        /// </summary>
        Image,

        /// <summary>
        /// Video type.
        /// </summary>
        Video,
    }

    /// <summary>
    /// A media gallery item.
    /// </summary>
    public class MediaGalleryItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        public MediaType Type { get; set; }

        /// <summary>
        /// Gets or sets the file size in bytes.
        /// </summary>
        public long FileSize { get; set; }
    }
}
