// <copyright file="MediaWindow.xaml.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.TestUI
{
    using System.Windows;
    using WpfLibrary.ControlSet.Models;

    /// <summary>
    /// The media window.
    /// </summary>
    public partial class MediaWindow : Window
    {
        private readonly MediaGalleryItem media;

        /// <summary>
        /// Initialises a new instance of the <see cref="MediaWindow"/> class.
        /// </summary>
        /// <param name="media">The media item.</param>
        public MediaWindow(MediaGalleryItem media)
        {
            this.media = media;
            this.InitializeComponent();
        }
    }
}
