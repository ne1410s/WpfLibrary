// <copyright file="Keyring.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using FullStack.Extensions.Crypto.Hash;
    using FullStack.Extensions.Text.Codec;
    using Microsoft.Win32;

    /// <summary>
    /// The keyring control; generates a key from a password and keyfiles.
    /// </summary>
    [TemplatePart(Name = PasswordPartName, Type = typeof(PasswordBox))]
    [TemplatePart(Name = FileListPartName, Type = typeof(ItemsControl))]
    [TemplatePart(Name = ResultTextPartName, Type = typeof(TextBlock))]
    public class Keyring : Control
    {
        /// <summary>
        /// Identifier of the Input routed event.
        /// </summary>
        public static readonly RoutedEvent InputEvent =
            EventManager.RegisterRoutedEvent(
                "Input",
                RoutingStrategy.Direct,
                typeof(RoutedEventHandler),
                typeof(Keyring));

        /// <summary>
        /// Dependency backing for <see cref="MaxScrollHeight"/>.
        /// </summary>
        public static readonly DependencyProperty MaxScrollHeightProperty =
            DependencyProperty.Register(
                "MaxScrollHeight",
                typeof(double),
                typeof(Keyring),
                new PropertyMetadata(42d));

        /// <summary>
        /// Dependency backing for <see cref="ResultBytes"/>.
        /// </summary>
        public static readonly DependencyProperty ResultBytesProperty =
            DependencyProperty.Register(
                "ResultBytes",
                typeof(byte[]),
                typeof(Keyring),
                new PropertyMetadata(Array.Empty<byte>()));

        private const string PasswordPartName = "PART_Password";
        private const string FileListPartName = "PART_FileList";
        private const string ResultTextPartName = "PART_ResultText";

        private PasswordBox passwordBox;
        private TextBlock resultTextBlock;
        private ItemsControl fileListControl;

        static Keyring()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Keyring),
                new FrameworkPropertyMetadata(typeof(Keyring)));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Keyring"/> class.
        /// </summary>
        public Keyring()
        {
            this.CommandBindings.AddRange(new[]
            {
                new CommandBinding(KeyringCommands.PickFilesCommand, this.PickFilesCommandExec),
                new CommandBinding(KeyringCommands.RemoveFileCommand, this.RemoveFileCommandExec),
                new CommandBinding(KeyringCommands.ClearFilesCommand, this.ClearFilesCommandExec),
            });
        }

        /// <summary>
        /// Fired when input is changed.
        /// </summary>
        public event RoutedEventHandler Input
        {
            add { this.AddHandler(InputEvent, value); }
            remove { this.RemoveHandler(InputEvent, value); }
        }

        /// <summary>
        /// Gets or sets the maximum scroll height (for the key file list).
        /// </summary>
        public double MaxScrollHeight
        {
            get => (double)this.GetValue(MaxScrollHeightProperty);
            set => this.SetValue(MaxScrollHeightProperty, value);
        }

        /// <summary>
        /// Gets the result bytes.
        /// </summary>
        public byte[] ResultBytes
        {
            get => (byte[])this.GetValue(ResultBytesProperty);
            private set => this.SetValue(ResultBytesProperty, value);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public string Result => this.ResultBytes?.AsString(ByteCodec.Base64);

        /// <summary>
        /// Gets or sets the password box.
        /// </summary>
        protected PasswordBox PasswordBox
        {
            get => this.passwordBox;
            set
            {
                if (this.passwordBox != null)
                {
                    this.passwordBox.PasswordChanged -= this.InputChangedInternal;
                }

                this.passwordBox = value;

                if (this.passwordBox != null)
                {
                    this.passwordBox.PasswordChanged += this.InputChangedInternal;
                }
            }
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.resultTextBlock = this.GetTemplateChild(ResultTextPartName) as TextBlock;
            this.fileListControl = this.GetTemplateChild(FileListPartName) as ItemsControl;
            this.PasswordBox = this.GetTemplateChild(PasswordPartName) as PasswordBox;

            this.UpdateResult();
        }

        private void PickFilesCommandExec(object sender, ExecutedRoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = true,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    this.fileListControl.Items.Add(new FileInfo(fileName));
                }

                this.InputChangedInternal(this.fileListControl, EventArgs.Empty);
            }
        }

        private void RemoveFileCommandExec(object sender, ExecutedRoutedEventArgs args)
        {
            if (args.Parameter is FileInfo fi)
            {
                this.fileListControl.Items.Remove(fi);
                this.InputChangedInternal(this.fileListControl, EventArgs.Empty);
            }
        }

        private void ClearFilesCommandExec(object sender, ExecutedRoutedEventArgs args)
        {
            this.fileListControl.Items.Clear();
            this.InputChangedInternal(this.fileListControl, EventArgs.Empty);
        }

        private void InputChangedInternal(object sender, EventArgs e)
        {
            this.UpdateResult();
            this.RaiseEvent(new RoutedEventArgs(InputEvent));
        }

        private void UpdateResult()
        {
            var seed = this.PasswordBox.Password;
            var hexHashes = this.fileListControl.Items
                .OfType<FileInfo>()
                .Where(fi => fi.Length != 0)
                .Select(fi => fi.LightHash(HashAlgo.Sha1).AsString(ByteCodec.Hex))
                .OrderBy(s => s);

            foreach (var hexHash in hexHashes)
            {
                seed = $"{hexHash}{seed}"
                    .AsBytes(CharCodec.Utf8)
                    .Hash(HashAlgo.Sha1)
                    .AsString(ByteCodec.Base64);
            }

            this.ResultBytes = seed.AsBytes(CharCodec.Utf8).Hash(HashAlgo.Md5);
            this.resultTextBlock.Text = $"Checksum: {this.Result}";
        }
    }
}
