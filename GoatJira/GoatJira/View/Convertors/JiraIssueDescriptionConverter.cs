namespace GoatJira.View.Convertors
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    //Space is due to error in WPF -- if null or empty string is assigned, MinLines in TextBox is not applied
    class JiraIssueDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = (string)value;
            return String.IsNullOrEmpty(result) ? " " : result; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value; 
    }
}
