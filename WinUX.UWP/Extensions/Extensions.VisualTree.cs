namespace WinUX.UWP.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines a collection of extensions for extracting data from the VisualTree.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Finds a descendant of the specified <see cref="FrameworkElement"/> in the VisualTree by the element name.
        /// </summary>
        /// <param name="element">
        /// The parent element.
        /// </param>
        /// <param name="name">
        /// The name of the child element to find.
        /// </param>
        /// <returns>
        /// Returns the child element if exists; else null.
        /// </returns>
        public static FrameworkElement FindDescendantByName(this FrameworkElement element, string name)
        {
            if (element == null || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            if (name.Equals(element.Name, StringComparison.OrdinalIgnoreCase))
            {
                return element;
            }

            var childCount = VisualTreeHelper.GetChildrenCount(element);
            for (var i = 0; i < childCount; i++)
            {
                var result = (VisualTreeHelper.GetChild(element, i) as FrameworkElement).FindDescendantByName(name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the first descendant of the specified <see cref="FrameworkElement"/> in the VisualTree by the type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to find.
        /// </typeparam>
        /// <param name="element">
        /// The parent element.
        /// </param>
        /// <returns>
        /// Returns the child element if exists; else null.
        /// </returns>
        public static T FindDescendant<T>(this DependencyObject element) where T : DependencyObject
        {
            T result = null;
            var childrenCount = VisualTreeHelper.GetChildrenCount(element);

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                var type = child as T;
                if (type != null)
                {
                    result = type;
                    break;
                }

                result = FindDescendant<T>(child);

                if (result != null)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a list of decendants of the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The parent dependency object.
        /// </param>
        /// <returns>
        /// Returns a collection of child elements.
        /// </returns>
        public static IEnumerable<DependencyObject> GetDescendants(this DependencyObject obj)
        {
            if (obj == null)
            {
                yield break;
            }

            var queue = new Queue<DependencyObject>();

            var popup = obj as Popup;

            if (popup != null)
            {
                if (popup.Child != null)
                {
                    queue.Enqueue(popup.Child);
                    yield return popup.Child;
                }
            }
            else
            {
                var count = VisualTreeHelper.GetChildrenCount(obj);

                for (var i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    queue.Enqueue(child);
                    yield return child;
                }
            }

            while (queue.Count > 0)
            {
                var parent = queue.Dequeue();

                popup = parent as Popup;

                if (popup != null)
                {
                    if (popup.Child == null)
                    {
                        continue;
                    }

                    queue.Enqueue(popup.Child);
                    yield return popup.Child;
                }
                else
                {
                    var count = VisualTreeHelper.GetChildrenCount(parent);

                    for (var i = 0; i < count; i++)
                    {
                        var child = VisualTreeHelper.GetChild(parent, i);
                        yield return child;
                        queue.Enqueue(child);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of decendants of the specified <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The parent dependency object.
        /// </param>
        /// <typeparam name="T">
        /// The type of element to find.
        /// </typeparam>
        /// <returns>
        /// Returns a collection of child elements of the specified type.
        /// </returns>
        public static IEnumerable<T> GetDescendantsOfType<T>(this DependencyObject obj) where T : DependencyObject
        {
            return obj.GetDescendants().OfType<T>();
        }

        /// <summary>
        /// Finds the first ascendant of the specified <see cref="FrameworkElement"/> in the VisualTree by the type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of element to find.
        /// </typeparam>
        /// <param name="element">
        /// The parent element.
        /// </param>
        /// <returns>
        /// Returns the parent element if exists; else null.
        /// </returns>
        public static T FindAscendant<T>(this FrameworkElement element) where T : FrameworkElement
        {
            if (element.Parent == null)
            {
                return null;
            }

            var parent = element.Parent as T;
            return parent ?? (element.Parent as FrameworkElement).FindAscendant<T>();
        }

        /// <summary>
        /// Finds the next sibling of the specified element by the specified tyoe.
        /// </summary>
        /// <typeparam name="T">
        /// The type of sibling to find.
        /// </typeparam>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// Returns the element if exists; else null.
        /// </returns>
        public static T FindNextSiblingOfType<T>(this FrameworkElement element) where T : DependencyObject
        {
            var parent = element.FindAscendant<FrameworkElement>();
            if (parent == null) return null;

            var parentDescendants = parent.GetDescendants().ToList();
            var parentDescendantsOfType = parentDescendants.OfType<T>();

            var itemIdx = parentDescendants.IndexOf(element);

            return (from descendantType in parentDescendantsOfType
                    let descendantTypeIdx = parentDescendants.IndexOf(descendantType)
                    where descendantTypeIdx > itemIdx
                    select descendantType).FirstOrDefault();
        }
    }
}