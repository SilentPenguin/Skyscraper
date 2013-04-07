using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Skyscraper
{
    public static class CursorLocationAttachedProperty
    {
        public static bool SetCursorLocation(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(CursorLocationProperty);
        }

        public static void SetCursorLocation(FrameworkElement frameworkElement, int value)
        {
            frameworkElement.SetValue(CursorLocationProperty, value);
        }

        public static readonly DependencyProperty CursorLocationProperty = DependencyProperty.RegisterAttached(
            "CursorLocation",
            typeof(int),
            typeof(CursorLocationAttachedProperty),
            new FrameworkPropertyMetadata(0, OnCursorLocationPropertyChanged)
        );

        private static void OnCursorLocationPropertyChanged(DependencyObject dependancyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            TextBox textbox = dependancyObject as TextBox;
            if (textbox != null)
            {
                if (eventArgs.OldValue == null && eventArgs.NewValue != null)
                {
                    textbox.SelectionChanged += textbox_SelectionChanged;
                }
                if (eventArgs.NewValue == null && eventArgs.OldValue != null){
                    textbox.SelectionChanged += textbox_SelectionChanged;
                }
            }
        }

        private static void textbox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                SetCursorLocation(textbox, textbox.CaretIndex);
            }
        }
    }
}
