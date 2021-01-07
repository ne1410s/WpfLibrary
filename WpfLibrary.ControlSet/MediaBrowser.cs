// <copyright file="MediaBrowser.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// A media browser.
    /// </summary>
    public class MediaBrowser : Selector
    {
        static MediaBrowser()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MediaBrowser),
                new FrameworkPropertyMetadata(typeof(MediaBrowser)));
        }
    }
}
