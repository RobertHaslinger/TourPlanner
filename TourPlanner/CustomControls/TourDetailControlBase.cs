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
    public class TourDetailControlBase : UserControl
    {
        public static readonly DependencyProperty TourProperty = DependencyProperty.Register(
            "Tour", typeof(Tour), typeof(TourDetailControlBase), new PropertyMetadata(default(Tour)));

        public Tour Tour
        {
            get { return (Tour) GetValue(TourProperty); }
            set { SetValue(TourProperty, value); }
        }

        public static readonly RoutedEvent TourEditedEvent = EventManager.RegisterRoutedEvent(
            "TourEdited", // Event name
            RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
            typeof(TourSelectedEventHandler), // The event type
            typeof(TourDetailControlBase)); // Belongs to ChildControlBase

        // Allows add and remove of event handlers to handle the custom event
        public event TourSelectedEventHandler TourEdited
        {
            add { AddHandler(TourEditedEvent, value); }
            remove { RemoveHandler(TourEditedEvent, value); }
        }

        public static readonly DependencyProperty SelectedTourEditedCommandProperty =
            DependencyProperty.Register("SelectedTourEditedCommand", typeof(ICommand), typeof(TourDetailControlBase), null);

        public ICommand SelectedTourEditedCommand
        {
            get { return (ICommand)GetValue((SelectedTourEditedCommandProperty)); }
            set { SetValue(SelectedTourEditedCommandProperty, value); }
        }

        public static readonly RoutedEvent TourDeletedEvent = EventManager.RegisterRoutedEvent(
            "TourDeleted", // Event name
            RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
            typeof(TourSelectedEventHandler), // The event type
            typeof(TourDetailControlBase)); // Belongs to ChildControlBase

        // Allows add and remove of event handlers to handle the custom event
        public event TourSelectedEventHandler TourDeleted
        {
            add { AddHandler(TourDeletedEvent, value); }
            remove { RemoveHandler(TourDeletedEvent, value); }
        }

        public static readonly DependencyProperty SelectedTourDeletedCommandProperty =
            DependencyProperty.Register("SelectedTourDeletedCommand", typeof(ICommand), typeof(TourDetailControlBase), null);

        public ICommand SelectedTourDeletedCommand
        {
            get { return (ICommand)GetValue((SelectedTourDeletedCommandProperty)); }
            set { SetValue(SelectedTourDeletedCommandProperty, value); }
        }

        public static readonly RoutedEvent TourCopiedEvent = EventManager.RegisterRoutedEvent(
            "TourCopied", // Event name
            RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
            typeof(TourSelectedEventHandler), // The event type
            typeof(TourDetailControlBase)); // Belongs to ChildControlBase

        // Allows add and remove of event handlers to handle the custom event
        public event TourSelectedEventHandler TourCopied
        {
            add { AddHandler(TourCopiedEvent, value); }
            remove { RemoveHandler(TourCopiedEvent, value); }
        }

        public static readonly DependencyProperty SelectedTourCopiedCommandProperty =
            DependencyProperty.Register("SelectedTourCopiedCommand", typeof(ICommand), typeof(TourDetailControlBase), null);

        public ICommand SelectedTourCopiedCommand
        {
            get { return (ICommand)GetValue((SelectedTourCopiedCommandProperty)); }
            set { SetValue(SelectedTourCopiedCommandProperty, value); }
        }
    }
}
