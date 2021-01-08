// <copyright file="MediaGallery.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using SecureMedia.Core.Extensions;
    using Forms = System.Windows.Forms;

    /// <summary>
    /// A media gallery.
    /// </summary>
    [TemplatePart(Name = FileListPartName, Type = typeof(MediaGallery))]
    public class MediaGallery : Control
    {
        /// <summary>
        /// Dependency backing for <see cref="Secret"/>.
        /// </summary>
        public static readonly DependencyProperty SecretProperty =
            DependencyProperty.Register(
                "Secret",
                typeof(byte[]),
                typeof(MediaGallery),
                new PropertyMetadata(Array.Empty<byte>()));

        private const string FileListPartName = "PART_FileList";

        private ListView fileListControl;

        static MediaGallery()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MediaGallery),
                new FrameworkPropertyMetadata(typeof(MediaGallery)));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MediaGallery"/> class.
        /// </summary>
        public MediaGallery()
        {
            this.CommandBindings.AddRange(new[]
            {
                new CommandBinding(MediaGalleryCommands.OpenMediaCommand, this.OpenMediaCommandExec),
                new CommandBinding(MediaGalleryCommands.BrowseFilesCommand, this.BrowseFilesCommandExec),
            });
        }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        public byte[] Secret
        {
            get => (byte[])this.GetValue(SecretProperty);
            set => this.SetValue(SecretProperty, value);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.fileListControl = this.GetTemplateChild(FileListPartName) as ListView;
        }

        private void OpenMediaCommandExec(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO
        }

        private void BrowseFilesCommandExec(object sender, ExecutedRoutedEventArgs e)
        {
            var folderBrowserDialog = new Forms.FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyDocuments,
            };

            if (folderBrowserDialog.ShowDialog() == Forms.DialogResult.OK)
            {
                this.fileListControl.Items.Clear();
                new DirectoryInfo(folderBrowserDialog.SelectedPath).Walk(
                    media => this.fileListControl.Items.Add(media), null, 100);
            }
        }
    }
}
