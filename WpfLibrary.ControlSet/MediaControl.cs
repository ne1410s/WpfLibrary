// <copyright file="MediaControl.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;
    using System.Windows.Controls;
    using SecureMedia.Core.Models;
    using WpfLibrary.ControlSet.Models;

    /// <summary>
    /// Base implementation for a media control.
    /// </summary>
    public abstract class MediaControl : Control
    {
        /// <summary>
        /// Dependency backing for <see cref="Media"/>.
        /// </summary>
        public static readonly DependencyProperty MediaProperty =
            DependencyProperty.Register(
                "Media",
                typeof(MediaGalleryItem),
                typeof(MediaControl));

        /// <summary>
        /// Gets the media type.
        /// </summary>
        public abstract MediaType MediaType { get; }

        /// <summary>
        /// Gets or sets the media item.
        /// </summary>
        public MediaGalleryItem Media
        {
            get => (MediaGalleryItem)this.GetValue(MediaProperty);
            set => this.SetValue(MediaProperty, value);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.Media?.Type != this.MediaType)
            {
                this.IsEnabled = false;
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
