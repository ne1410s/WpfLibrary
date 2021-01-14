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
        /// Gets the test command.
        /// </summary>
        public RelayCommand OpenMediaCommand { get; } = new RelayCommand(item =>
        {
            var media = item as MediaGalleryItem;
            var mediaWindow = new MediaWindow { };
            mediaWindow.DataContext = new MediaWindowViewModel { Media = media };
            mediaWindow.Show();
        });
    }
}
