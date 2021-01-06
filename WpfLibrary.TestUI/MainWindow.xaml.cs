// <copyright file="MainWindow.xaml.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace WpfLibrary.TestUI
{
    using System.Windows;
    using WpfLibrary.ControlSet;

    /// <summary>
    /// The main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Keyring_ResultChanged(object sender, KeyringResultEventArgs args)
        {
        }
    }
}
