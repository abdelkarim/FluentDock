using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FluentDock.Converters
{
    /// <summary>
    /// The purpose of this <see cref="DocumentsScrollingVisibilityConverter"/> is to change the visibility output
    /// of the <see cref="MenuScrollingVisibilityConverter"/> from collapsed to hidden.
    /// </summary>
    public class DocumentsScrollingVisibilityConverter : IMultiValueConverter
    {
        #region "Fields"

        private MenuScrollingVisibilityConverter _scrollingVisibilityConverter;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes instance members of the <see cref="DocumentsScrollingVisibilityConverter"/> class.
        /// </summary>
        public DocumentsScrollingVisibilityConverter()
        {
            _scrollingVisibilityConverter = new MenuScrollingVisibilityConverter();
        }

        #endregion

        #region "Methods"

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var convert = _scrollingVisibilityConverter.Convert(values, targetType, parameter, culture);
            if (convert == null)
                return Visibility.Hidden;

            var visibility = (Visibility)convert;
            return visibility == Visibility.Collapsed ? Visibility.Hidden : visibility;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return _scrollingVisibilityConverter.ConvertBack(value, targetTypes, parameter, culture);
        }

        #endregion
    }
}
