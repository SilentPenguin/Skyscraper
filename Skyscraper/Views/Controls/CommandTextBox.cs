using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Skyscraper.Views.Controls
{
    class CommandTextBox : TextBox
    {
        public CommandTextBox ()
        {
            base.SelectionChanged += CommandTextBox_SelectionChanged;
        }

        private void CommandTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox != null)
            {
                CursorLocation = textbox.CaretIndex;
            }
        }

        #region CursorLocationProperty

        public int CursorLocation
        {
            get
            {
                return (int)base.GetValue(CursorLocationProperty);
            }
            set
            {
                base.SetValue(CursorLocationProperty, value);
            }
        }

        public static readonly DependencyProperty CursorLocationProperty = DependencyProperty.RegisterAttached(
            "CursorLocation",
            typeof(int),
            typeof(CommandTextBox),
            new FrameworkPropertyMetadata(0, OnCursorLocationChanged)
        );

        private static void OnCursorLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textbox = d as TextBox;

            if (textbox != null && e.NewValue != e.OldValue && e.NewValue is int && (int)e.NewValue != textbox.CaretIndex)
            {
                textbox.CaretIndex = (int)e.NewValue;
            }
        }
        #endregion

        
    }
}
