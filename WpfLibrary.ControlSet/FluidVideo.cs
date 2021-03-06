﻿// <copyright file="FluidVideo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;
    using SecureMedia.Core.Models;

    /// <summary>
    /// The fluid video control.
    /// </summary>
    public class FluidVideo : MediaControl
    {
        static FluidVideo()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FluidVideo),
                new FrameworkPropertyMetadata(typeof(FluidVideo)));
        }

        /// <inheritdoc/>
        public override MediaType MediaType => MediaType.Video;
    }
}
