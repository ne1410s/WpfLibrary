// <copyright file="FluidVideo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;
    using System.Windows.Controls;
    using SecureMedia.Core.Models;
    using WpfLibrary.ControlSet.Models;

    /// <summary>
    /// The fluid video control.
    /// </summary>
    public class FluidVideo : Control
    {
        /// <summary>
        /// Dependency backing for <see cref="Media"/>.
        /// </summary>
        public static readonly DependencyProperty MediaProperty =
            DependencyProperty.Register(
                "Media",
                typeof(MediaGalleryItem),
                typeof(FluidVideo));

        static FluidVideo()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FluidVideo),
                new FrameworkPropertyMetadata(typeof(FluidVideo)));
        }

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

            if (this.Media.Type != MediaType.Video)
            {
                this.IsEnabled = false;
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
