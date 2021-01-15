// <copyright file="MediaGallery.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using SecureMedia.Core.Extensions;
    using WpfLibrary.ControlSet.Extensions;
    using WpfLibrary.ControlSet.Models;
    using Forms = System.Windows.Forms;

    /// <summary>
    /// A media gallery.
    /// </summary>
    [TemplatePart(Name = FileListPartName, Type = typeof(MediaGallery))]
    public class MediaGallery : Control, ICommandSource
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

        /// <summary>
        /// Dependency backing for <see cref="Command"/>.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(MediaGallery),
                new PropertyMetadata(null));

        private const int ThumbnailHeight = 100;
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
        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        /// <inheritdoc/>
        public object CommandParameter
        {
            get
            {
                var media = (MediaGalleryItem)this.fileListControl.SelectedItem;
                media.Secret = this.Secret;
                return media;
            }
        }

        /// <inheritdoc/>
        public IInputElement CommandTarget => throw new NotImplementedException();

        /// <inheritdoc/>
        public override async void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.fileListControl = this.GetTemplateChild(FileListPartName) as ListView;

            // DEMO!
            await this.AddMedia(@"c:\temp\keys");
        }

        private void OpenMediaCommandExec(object sender, ExecutedRoutedEventArgs e)
        {
            this.Command?.Execute(this.CommandParameter);
        }

        private async void BrowseFilesCommandExec(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new Forms.FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyDocuments,
            };

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                this.fileListControl.Items.Clear();
                await this.AddMedia(dialog.SelectedPath);
            }
        }

        private async Task AddMedia(string directory)
        {
            var secret = this.Secret;
            foreach (var media in directory.ListMedia())
            {
                var item = new MediaGalleryItem(media, secret);
                using var bitmap = item.GetPreview(ThumbnailHeight, item.Secret);
                item.ThumbSource = bitmap.ToSource();
                this.fileListControl.Items.Add(item);
                await Task.Delay(1);
            }
        }
    }
}
