// <copyright file="FluidImage.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;
    using System.Windows.Controls;
    using SecureMedia.Core.Models;
    using WpfLibrary.ControlSet.Models;

    /// <summary>
    /// The fluid image control.
    /// </summary>
    public class FluidImage : Control
    {
        /// <summary>
        /// Dependency backing for <see cref="Media"/>.
        /// </summary>
        public static readonly DependencyProperty MediaProperty =
            DependencyProperty.Register(
                "Media",
                typeof(MediaGalleryItem),
                typeof(FluidImage));

        static FluidImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FluidImage),
                new FrameworkPropertyMetadata(typeof(FluidImage)));
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

            if (this.Media.Type != MediaType.Image)
            {
                this.IsEnabled = false;
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
