namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    /// <summary>
    /// Defines a control which can expand and collapse content with a header.
    /// </summary>
    [TemplateVisualState(GroupName = "ExpanderStates", Name = "Expanded")]
    [TemplateVisualState(GroupName = "ExpanderStates", Name = "Collapsed")]
    [TemplateVisualState(GroupName = "ExpanderStates", Name = "AnimatedExpanded")]
    [TemplateVisualState(GroupName = "ExpanderStates", Name = "AnimatedCollapsed")]
    [TemplatePart(Name = "HeaderPresenter", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "ContentPresenter", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "HeaderContainer", Type = typeof(UIElement))]
    public partial class Expander : ContentControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Expander"/> class.
        /// </summary>
        public Expander()
        {
            this.DefaultStyleKey = typeof(Expander);
        }

        /// <summary>
        /// Called when applying the control's template.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (this.HeaderContainer != null)
            {
                this.HeaderContainer.Tapped -= this.OnHeaderContainerTapped;
            }

            this.HeaderContainer = this.GetTemplateChild("HeaderContainer") as UIElement;

            if (this.HeaderContainer != null)
            {
                this.HeaderContainer.Tapped += this.OnHeaderContainerTapped;
            }

            this.SetState(this.IsExpanded, false);
            base.OnApplyTemplate();
        }

        private void OnHeaderContainerTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
            this.IsExpanded = !this.IsExpanded;
        }

        private void SetState(bool isExpanded, bool useTransitions)
        {
            if (this.UseAnimations)
            {
                VisualStateManager.GoToState(this, isExpanded ? "AnimatedExpanded" : "AnimatedCollapsed", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, isExpanded ? "Expanded" : "Collapsed", useTransitions);
            }

            VisualStateManager.GoToState(this, isExpanded ? "GlyphExpanded" : "GlyphCollapsed", useTransitions);
        }
    }
}