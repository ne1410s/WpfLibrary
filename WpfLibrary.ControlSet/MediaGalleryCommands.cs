// <copyright file="MediaGalleryCommands.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows.Input;

    /// <summary>
    /// Commands for the media gallery. Note that there is no implementation in
    /// this class; it merely serves as a declaration so the commands can be
    /// referenced in the control template. The implemention is provided in the
    /// control class.
    /// </summary>
    public static class MediaGalleryCommands
    {
        /// <summary>
        /// Gets the open media command.
        /// </summary>
        public static RoutedCommand OpenMediaCommand { get; } = new RoutedCommand();

        /// <summary>
        /// Gets the browse files command.
        /// </summary>
        public static RoutedCommand BrowseFilesCommand { get; } = new RoutedCommand();
    }
}
