// <copyright file="KeyringCommands.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows.Input;

    /// <summary>
    /// Commands for the keyring control. Note that there is no implementation
    /// in this class; it merely serves as a declaration so the commands can be
    /// referenced in the control template. The implemention is provided in the
    /// control class.
    /// </summary>
    public static class KeyringCommands
    {
        /// <summary>
        /// Gets the pick files command.
        /// </summary>
        public static RoutedCommand PickFilesCommand { get; } = new RoutedCommand();

        /// <summary>
        /// Gets the remove file command.
        /// </summary>
        public static RoutedCommand RemoveFileCommand { get; } = new RoutedCommand();

        /// <summary>
        /// Gets the clear files command.
        /// </summary>
        public static RoutedCommand ClearFilesCommand { get; } = new RoutedCommand();
    }
}
