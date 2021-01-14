// <copyright file="Converters.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

#pragma warning disable SA1649 //SA1649FileNameMustMatchTypeName
namespace WpfLibrary.TestUI
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Visibility converter based on string matching.
    /// </summary>
    public class StringMatchVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value}" == $"{parameter}"
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
#pragma warning restore SA1649 //SA1649FileNameMustMatchTypeName
