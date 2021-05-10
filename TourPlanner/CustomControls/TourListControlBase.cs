using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Models;

namespace TourPlanner.CustomControls
{
    public delegate void TourSelectedEventHandler(object sender, TourSelectedEventArgs e);

    public class TourListControlBase : UserControl
    {
        public static readonly RoutedEvent TourSelectedEvent = EventManager.RegisterRoutedEvent(
            "TourSelected", // Event name
            RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
            typeof(TourSelectedEventHandler), // The event type
            typeof(TourListControlBase)); // Belongs to ChildControlBase

        // Allows add and remove of event handlers to handle the custom event
        public event TourSelectedEventHandler TourSelected
        {
            add { AddHandler(TourSelectedEvent, value); }
            remove { RemoveHandler(TourSelectedEvent, value); }
        }

        public static readonly DependencyProperty SelectedTourChangedCommandProperty=
            DependencyProperty.Register("SelectedTourChangedCommand", typeof(ICommand), typeof(TourListControlBase), null);

        public ICommand SelectedTourChangedCommand
        {
            get { return (ICommand) GetValue((SelectedTourChangedCommandProperty)); }
            set {SetValue(SelectedTourChangedCommandProperty, value);}
        }
    }

    public class TourSelectedEventArgs : RoutedEventArgs
    {
        public Tour SelectedTour { get; set; }

        public TourSelectedEventArgs() : base() { }
        public TourSelectedEventArgs(RoutedEvent routedEvent) : base(routedEvent) { }
        public TourSelectedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
    }
}
