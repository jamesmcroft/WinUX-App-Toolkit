namespace WinUX.Xaml.Behaviors.Composition
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Hosting;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior to provide a parallax effect to a ScrollViewer such as a ListView or GridView.
    /// </summary>
    public sealed class ParallaxEffectBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="ParallaxElement"/>.
        /// </summary>
        public static readonly DependencyProperty ParallaxElementProperty =
            DependencyProperty.Register(
                nameof(ParallaxElement),
                typeof(UIElement),
                typeof(ParallaxEffectBehavior),
                new PropertyMetadata(
                    null,
                    (d, e) => ((ParallaxEffectBehavior)d).AttachParallaxEffect((UIElement)e.NewValue)));

        /// <summary>
        /// Defines the dependency property for <see cref="ParallaxMultiplier"/>.
        /// </summary>
        public static readonly DependencyProperty ParallaxMultiplierProperty =
            DependencyProperty.Register(
                nameof(ParallaxMultiplier),
                typeof(double),
                typeof(ParallaxEffectBehavior),
                new PropertyMetadata(0.3));

        /// <summary>
        /// Gets or sets the parallax multiplier.
        /// </summary>
        public double ParallaxMultiplier
        {
            get
            {
                return (double)this.GetValue(ParallaxMultiplierProperty);
            }
            set
            {
                this.SetValue(ParallaxMultiplierProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the parallax element.
        /// </summary>
        public UIElement ParallaxElement
        {
            get
            {
                return (UIElement)this.GetValue(ParallaxElementProperty);
            }
            set
            {
                this.SetValue(ParallaxElementProperty, value);
            }
        }

        /// <summary>
        /// Called after the behavior is attached to the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnAttached()
        {
            this.AttachParallaxEffect(this.ParallaxElement);
        }

        private void AttachParallaxEffect(UIElement element)
        {
            if (element != null && this.AssociatedObject != null)
            {
                var scrollViewer = this.AssociatedObject as ScrollViewer;
                if (scrollViewer == null)
                {
                    // Attempt to see if this is attached to a scroll-based control like a ListView or GridView.
                    scrollViewer = this.AssociatedObject.FindDescendant<ScrollViewer>();

                    if (scrollViewer == null)
                    {
                        throw new InvalidOperationException(
                                  "The associated object or one of it's child elements must be of type ScrollViewer.");
                    }
                }

                var compositionPropertySet =
                    ElementCompositionPreview.GetScrollViewerManipulationPropertySet(scrollViewer);

                var compositor = compositionPropertySet.Compositor;

                var animationExpression = compositor.CreateExpressionAnimation(
                    "ScrollViewer.Translation.Y * Multiplier");

                animationExpression.SetScalarParameter("Multiplier", (float)this.ParallaxMultiplier);
                animationExpression.SetReferenceParameter("ScrollViewer", compositionPropertySet);

                var visual = ElementCompositionPreview.GetElementVisual(element);
                visual.StartAnimation("Offset.Y", animationExpression);
            }
        }
    }
}