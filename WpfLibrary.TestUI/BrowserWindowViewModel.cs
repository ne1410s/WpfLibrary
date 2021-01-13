// <copyright file="BrowserWindowViewModel.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.TestUI
{
    using WpfLibrary.ControlSet.Models;

    /// <summary>
    /// View model for <see cref="BrowserWindow"/>.
    /// </summary>
    public class BrowserWindowViewModel
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        public byte[] Secret { get; set; }

        /// <summary>
        /// Gets the test command.
        /// </summary>
        public RelayCommand TestCommand { get; } = new RelayCommand(item =>
        {
            new MediaWindow((MediaGalleryItem)item).Show();
        });
    }
}
