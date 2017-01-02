namespace WinUX.UWP.Mvvm.Services
{
    using System;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;

    /// <summary>
    /// Defines the interface for a Windows navigation service.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Gets the current page type.
        /// </summary>
        Type CurrentPageType { get; }

        /// <summary>
        /// Gets the current page's navigation parameter.
        /// </summary>
        object CurrentPageNavigationParameter { get; }

        /// <summary>
        /// Gets the navigation frame.
        /// </summary>
        Frame Frame { get; }

        /// <summary>
        /// Gets a value indicating whether the frame can go back.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Navigates the current frame back.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Navigates the current frame to the specified page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to navigate to.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        bool Navigate(Type sourcePageType, NavigationTransitionInfo transition = null);

        /// <summary>
        /// Navigates the current frame to the specified page type with the specified parameter.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to navigate to.
        /// </param>
        /// <param name="parameter">
        /// The parameter to pass to the page.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        bool Navigate(Type sourcePageType, object parameter, NavigationTransitionInfo transition = null);

        /// <summary>
        /// Navigates the current frame to the specified page via the type's full name.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to navigate to.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        bool Navigate(string sourcePageName, NavigationTransitionInfo transition = null);


        /// <summary>
        /// Navigates the current frame to the specified page via the type's full name with the specified parameter.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to navigate to.
        /// </param>
        /// <param name="parameter">
        /// The parameter to pass to the page.
        /// </param>
        /// <param name="transition">
        /// The navigation transition.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> indicating whether the navigation was successful.
        /// </returns>
        bool Navigate(string sourcePageName, object parameter, NavigationTransitionInfo transition = null);

        /// <summary>
        /// Removes the last navigation entry in the backstack.
        /// </summary>
        void RemoveLastNavigation();

        /// <summary>
        /// Removes the last navigation entries in the backstack up to the specified page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to remove to.
        /// </param>
        void RemoveToView(Type sourcePageType);

        /// <summary>
        /// Removes the last navigation entries in the backstack up to the specified page via the type's full name.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to remove to.
        /// </param>
        /// <example>
        /// this.NavigationService.RemoveToView("WinUX.UWP.MainPage, WinUX.UWP");
        /// </example>
        void RemoveToView(string sourcePageName);

        /// <summary>
        /// Checks whether the navigation backstack entries contains the specified page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The page type to find.
        /// </param>
        /// <returns>
        /// Returns true if the page exists; else false.
        /// </returns>
        bool HasPreviouslyNavigatedTo(Type sourcePageType);

        /// <summary>
        /// Checks whether the navigation backstack entries contains the specified page via the type's full name.
        /// </summary>
        /// <param name="sourcePageName">
        /// The page type's full name to find.
        /// </param>
        /// <returns>
        /// Returns true if the page exists; else false.
        /// </returns>
        bool HasPreviouslyNavigatedTo(string sourcePageName);

        /// <summary>
        /// Clears all navigation backstack entries up to the current page.
        /// </summary>
        void ClearNavigationHistory();
    }
}