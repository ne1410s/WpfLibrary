// <copyright file="FluidImage.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The fluid image control.
    /// </summary>
    public class FluidImage : Control
    {
        static FluidImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FluidImage),
                new FrameworkPropertyMetadata(typeof(FluidImage)));
        }
    }
}
