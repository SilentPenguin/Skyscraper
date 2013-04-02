using System.Windows;
using System.Windows.Media;

namespace Skyscraper
{
    public static class UIElementExtensionMethods
    {
        public static T FindAncestor<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            while (dependencyObject != null)
            {
                T typedDependencyObject = dependencyObject as T;

                if (typedDependencyObject != null)
                    return typedDependencyObject;

                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            return null;
        }

        public static T FindAncestor<T>(this UIElement obj) where T : UIElement
        {
            return FindAncestor<T>((DependencyObject)obj);
        }
    }
}