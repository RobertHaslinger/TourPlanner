using System;
using System.Windows;

namespace TourPlanner.Helper.Factory
{
    /// <summary>
    /// Implements a loader whose dependency property can be used in XAML to load the requested view model through the given factory
    /// Inspiration from: https://dzone.com/articles/service-locator-mvvm
    /// </summary>
    public class ViewModelLoader
    {
        public static readonly DependencyProperty FactoryTypeProperty =
            DependencyProperty.RegisterAttached("FactoryType", typeof(Type), typeof(ViewModelLoader),
                new FrameworkPropertyMetadata(null,
                    OnFactoryTypeChanged));


        public static Type GetFactoryType(DependencyObject d)
        {
            return (Type)d.GetValue(FactoryTypeProperty);
        }

        public static void SetFactoryType(DependencyObject d, Type value)
        {
            d.SetValue(FactoryTypeProperty, value);
        }

        private static void OnFactoryTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            IViewModelFactory factory = Activator.CreateInstance(GetFactoryType(d)) as IViewModelFactory;
            if (factory == null)
                throw new InvalidOperationException("Your type does not implement the IViewModelFactory.");
            element.DataContext = factory.CreateViewModel(d);
        }
    }
}
