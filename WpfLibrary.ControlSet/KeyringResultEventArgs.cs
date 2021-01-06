// <copyright file="KeyringResultEventArgs.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;

    /// <summary>
    /// Handler declaration for KeyringResult events.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The event args.</param>
    public delegate void KeyringResultEventHandler(object sender, KeyringResultEventArgs args);

    /// <summary>
    /// Arguments for KeyringResult events.
    /// </summary>
    public class KeyringResultEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="KeyringResultEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="result">The resulting bytes.</param>
        public KeyringResultEventArgs(RoutedEvent routedEvent, byte[] result)
            : base(routedEvent)
        {
            this.Result = result;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public byte[] Result { get; }
    }
}
