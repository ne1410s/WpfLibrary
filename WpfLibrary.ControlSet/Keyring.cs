// <copyright file="Keyring.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.ControlSet
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
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
    public class Keyring : Control
    {
        /// <summary>
        /// The Files dependency property.
        /// </summary>
        public static readonly DependencyProperty FilesProperty =
            DependencyProperty.Register(
                "Files",
                typeof(ObservableCollection<FileInfo>),
                typeof(Keyring));

        /// <summary>
        /// The ResultChanged routed event identifier.
        /// </summary>
        public static readonly RoutedEvent ResultChangedEvent =
            EventManager.RegisterRoutedEvent(
                "ResultChanged",
                RoutingStrategy.Direct,
                typeof(KeyringResultEventHandler),
                typeof(Keyring));

        private static readonly DependencyPropertyKey ResultPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Result",
                typeof(string),
                typeof(Keyring),
                new PropertyMetadata());

        /// <summary>
        /// The Result dependency property.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202", Justification = "WPF Framework")]
        public static readonly DependencyProperty ResultProperty = ResultPropertyKey.DependencyProperty;

        private static readonly string PasswordPartName = "PART_Password";

        private PasswordBox passwordBox;

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
            });
        }

        /// <summary>
        /// The ResultChanged event.
        /// </summary>
        public event KeyringResultEventHandler ResultChanged
        {
            add { this.AddHandler(ResultChangedEvent, value); }
            remove { this.RemoveHandler(ResultChangedEvent, value); }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public string Result
        {
            get => (string)this.GetValue(ResultProperty);
            private set => this.SetValue(ResultPropertyKey, value);
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        public ObservableCollection<FileInfo> Files
        {
            get => (ObservableCollection<FileInfo>)this.GetValue(FilesProperty);
        }

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
                    this.passwordBox.PasswordChanged -= this.InputChanged;
                }

                this.passwordBox = value;

                if (this.passwordBox != null)
                {
                    this.passwordBox.PasswordChanged += this.InputChanged;
                }
            }
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.Files == null)
            {
                this.SetValue(FilesProperty, new ObservableCollection<FileInfo>());
                this.Files.CollectionChanged += this.InputChanged;
            }

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
                    this.Files.Add(new FileInfo(fileName));
                }
            }
        }

        private void RemoveFileCommandExec(object sender, ExecutedRoutedEventArgs args)
        {
            if (args.Parameter is FileInfo fi)
            {
                this.Files.Remove(fi);
            }
        }

        private void InputChanged(object sender, EventArgs e)
        {
            var bytes = this.UpdateResult();
            this.RaiseEvent(new KeyringResultEventArgs(ResultChangedEvent, bytes));
        }

        private byte[] UpdateResult()
        {
            var seed = this.PasswordBox.Password;
            var hexHashes = this.Files
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

            var resultBytes = seed.AsBytes(CharCodec.Utf8).Hash(HashAlgo.Md5);
            this.Result = resultBytes.AsString(ByteCodec.Base64);

            return resultBytes;
        }
    }
}
