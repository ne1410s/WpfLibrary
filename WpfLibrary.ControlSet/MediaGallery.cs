// <copyright file="MediaGallery.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using WpfLibrary.ControlSet.Models;

    /// <summary>
    /// A media browser.
    /// </summary>
    [TemplatePart(Name = FileListPartName, Type = typeof(ListView))]
    public class MediaGallery : Control
    {
        private const string FileListPartName = "PART_FileList";

        private ListView fileListControl;

        static MediaGallery()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MediaGallery),
                new FrameworkPropertyMetadata(typeof(MediaGallery)));
        }

        /// <summary>
        /// Gets or sets the file list control.
        /// </summary>
        protected ListView FileListControl
        {
            get => this.fileListControl;
            set
            {
                if (this.fileListControl != null)
                {
                    this.fileListControl.SelectionChanged -= this.SelectionChangedInternal;
                }

                this.fileListControl = value;

                if (this.fileListControl != null)
                {
                    this.fileListControl.SelectionChanged += this.SelectionChangedInternal;
                }
            }
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.FileListControl = this.GetTemplateChild(FileListPartName) as ListView;

            this.PopulateFiles();
        }

        private void SelectionChangedInternal(object sender, SelectionChangedEventArgs e)
        {
        }

        private void PopulateFiles()
        {
            this.FileListControl.Items.Clear();
            foreach (var fi in new DirectoryInfo(@"c:\temp\keys").GetFiles())
            {
                var item = new MediaGalleryItem
                {
                    Id = Guid.NewGuid(),
                    Name = fi.Name,
                    FileSize = fi.Length,
                    Type = MediaType.Image,
                };

                this.FileListControl.Items.Add(item);
            }
        }
    }
}
