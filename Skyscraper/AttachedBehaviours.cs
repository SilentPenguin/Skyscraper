using System.Windows;
using System.Windows.Controls;

namespace Skyscraper
{
    public class AttachedBehaviours
    {
        #region CursorLocationBehaviourProperty
        public static readonly DependencyProperty CursorLocationBehaviourProperty = DependencyProperty.RegisterAttached(
            "CursorLocationBehaviour",
            typeof(bool),
            typeof(AttachedBehaviours),
            new FrameworkPropertyMetadata(false, OnSetCursorLocationBehaviourChanged)
        );

        public static bool GetCursorLocationBehaviour(TextBox textBox)
        {
            return (bool)textBox.GetValue(CursorLocationBehaviourProperty);
        }

        public static void SetCursorLocationBehaviour(TextBox textBox, bool value)
        {
            textBox.SetValue(CursorLocationBehaviourProperty, value);
        }

        private static void OnSetCursorLocationBehaviourChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            TextBox textBox = dependencyObject as TextBox;

            if (textBox == null)
                return;

            if (!(eventArgs.NewValue is bool))
                return;

            if ((bool)eventArgs.NewValue)
            {
                textBox.SelectionChanged += (sender, selectionChangedEventArgs) =>
                {
                    TextBox textbox = sender as TextBox;

                    if (textbox != null)
                    {
                        SetCursorLocation(textbox, textbox.CaretIndex);
                    }
                };
            }
        }
        #endregion

        #region CursorLocationProperty
        public static readonly DependencyProperty CursorLocationProperty = DependencyProperty.RegisterAttached(
            "CursorLocation",
            typeof(int),
            typeof(AttachedBehaviours),
            new FrameworkPropertyMetadata(0)
        );

        public static int GetCursorLocation(TextBox textBox)
        {
            return (int)textBox.GetValue(CursorLocationProperty);
        }

        public static void SetCursorLocation(TextBox textBox, int value)
        {
            textBox.SetValue(CursorLocationProperty, value);
        }
        #endregion

        #region IsUserVisibleBehaviourProperty
        public static readonly DependencyProperty IsUserVisibleBehaviourProperty = DependencyProperty.RegisterAttached(
            "IsUserVisibleBehaviour",
            typeof(bool),
            typeof(AttachedBehaviours),
            new FrameworkPropertyMetadata(false, OnSetIsUserVisibleBehaviourChanged)
        );

        public static bool GetIsUserVisibleBehaviour(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(IsUserVisibleBehaviourProperty);
        }

        public static void SetIsUserVisibleBehaviour(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(IsUserVisibleBehaviourProperty, value);
        }

        private static void OnSetIsUserVisibleBehaviourChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            FrameworkElement frameworkElement = dependencyObject as FrameworkElement;

            if (frameworkElement == null)
                return;

            if (!(eventArgs.NewValue is bool))
                return;

            if ((bool)eventArgs.NewValue)
            {
                frameworkElement.LayoutUpdated += (sender, layoutUpdateEventArgs) =>
                {
                    if (frameworkElement != null)
                    {
                        //HACK: AJ: This uses a hard coded container type.
                        FrameworkElement containerFrameworkElement = frameworkElement.FindAncestor<System.Windows.Controls.ListView>();

                        bool isUserVisible = false;

                        if (frameworkElement.IsVisible)
                        {
                            Rect containerBounds = frameworkElement.TransformToAncestor(containerFrameworkElement).TransformBounds(new Rect(0.0, 0.0, frameworkElement.ActualWidth, frameworkElement.ActualHeight));
                            Rect frameworkElementBounds = new Rect(0.0, 0.0, containerFrameworkElement.ActualWidth, containerFrameworkElement.ActualHeight);

                            isUserVisible = frameworkElementBounds.IntersectsWith(containerBounds);
                        }

                        frameworkElement.SetValue(IsUserVisibleBehaviourProperty, isUserVisible);
                    }
                };
            }
        }
        #endregion

        #region IsUserVisibleProperty
        public static readonly DependencyProperty IsUserVisibleProperty = DependencyProperty.RegisterAttached(
            "IsUserVisible",
            typeof(bool),
            typeof(AttachedBehaviours),
            new FrameworkPropertyMetadata(false)
        );

        public static bool GetIsUserVisible(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(IsUserVisibleProperty);
        }

        public static void SetIsUserVisible(FrameworkElement frameworkElement, bool value) 
        {
            frameworkElement.SetValue(IsUserVisibleProperty, value);
        }
        #endregion
    }
}