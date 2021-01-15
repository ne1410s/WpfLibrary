// <copyright file="FluidImage.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System.Windows;
    using System.Windows.Controls;
    using SecureMedia.Core.Models;
    using WpfLibrary.ControlSet.Extensions;

    /// <summary>
    /// The fluid image control.
    /// </summary>
    [TemplatePart(Name = ImagePartName, Type = typeof(Image))]
    public class FluidImage : MediaControl
    {
        private const string ImagePartName = "PART_Image";

        private Image imageControl;

        static FluidImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FluidImage),
                new FrameworkPropertyMetadata(typeof(FluidImage)));
        }

        /// <inheritdoc/>
        public override MediaType MediaType => MediaType.Image;

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.imageControl = this.GetTemplateChild(ImagePartName) as Image;
            this.imageControl.Source ??= this.Media.ToSource();
        }
    }
}
