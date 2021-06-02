using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TourPlanner.CustomControls
{
    public delegate void SearchTextChangedEventHandler(object sender, SearchTextChangedEventArgs e);

    public class SearchViewBase : UserControl
    {
        public static readonly RoutedEvent SearchTextChangedEvent = EventManager.RegisterRoutedEvent(
            "SearchTextChanged", // Event name
            RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
            typeof(SearchTextChangedEventHandler), // The event type
            typeof(SearchViewBase)); // Belongs to ChildControlBase

        // Allows add and remove of event handlers to handle the custom event
        public event SearchTextChangedEventHandler SearchTextChanged
        {
            add { AddHandler(SearchTextChangedEvent, value); }
            remove { RemoveHandler(SearchTextChangedEvent, value); }
        }

        public static readonly DependencyProperty SearchTextChangedCommandProperty =
            DependencyProperty.Register("SearchTextChangedCommand", typeof(ICommand), typeof(SearchViewBase), null);

        public ICommand SearchTextChangedCommand
        {
            get { return (ICommand)GetValue((SearchTextChangedCommandProperty)); }
            set { SetValue(SearchTextChangedCommandProperty, value); }
        }
    }

    public class SearchTextChangedEventArgs : RoutedEventArgs
    {
        public string Text { get; set; }
        public SearchTextChangedEventArgs() : base() { }
        public SearchTextChangedEventArgs(RoutedEvent routedEvent) : base(routedEvent) { }
        public SearchTextChangedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
    }
}
