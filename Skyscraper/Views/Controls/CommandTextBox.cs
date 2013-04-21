using Skyscraper.ViewModels.Managers;
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
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCursorLocationChanged)
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

        public Range CurrentlySelectedText
        {
            get { return (Range)base.GetValue(CurrentlySelectedTextProperty); }
            set { base.SetValue(CurrentlySelectedTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentlySelectedTextProperty =
            DependencyProperty.RegisterAttached(
                "CurrentlySelectedText",
                typeof(Range),
                typeof(CommandTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTextChanged));

        private static void OnSelectedTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CommandTextBox tb = obj as CommandTextBox;
            if (tb != null)
            {
                if (e.OldValue == null && e.NewValue != null)
                {
                    tb.SelectionChanged += tb_SelectionChanged;
                }
                else if (e.OldValue != null && e.NewValue == null)
                {
                    tb.SelectionChanged -= tb_SelectionChanged;
                }

                Range newValue = e.NewValue as Range;
                Range oldValue = e.OldValue as Range;

                if (newValue != null && (oldValue == null || oldValue.LowerBound != newValue.LowerBound || oldValue.UpperBound != newValue.UpperBound))
                {
                    tb.Select(newValue.LowerBound, newValue.UpperBound - newValue.LowerBound);
                }
            }
        }

        static void tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CommandTextBox tb = sender as CommandTextBox;
            if (tb != null)
            {
                tb.CurrentlySelectedText = new Range { LowerBound = tb.SelectionStart, UpperBound = tb.SelectionStart + tb.SelectionLength };
            }
        }
        
    }
}
