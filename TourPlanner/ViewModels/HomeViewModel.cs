﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.Services;
using TourPlanner.Services.Database;
using TourPlanner.Services.Greet;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class HomeViewModel : BaseViewModel, ISubject
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();
        private List<IObserver> _observers = new List<IObserver>();

        private string _greetMessage;

        public string GreetMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_greetMessage))
                {
                    LoadGreetMessage();
                }

                return _greetMessage;
            }
            set { _greetMessage = value; OnPropertyChanged(); }
        }

        private Tour _selectedTour;

        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set { _selectedTour = value; OnPropertyChanged();}
        }


        public ICommand SelectedTourChangedCommand => new RelayCommand(OnSelectedTourChangedCommandExecuted);
        public ICommand SelectedTourEditedCommand => new RelayCommand(OnSelectedTourEditedCommandExecuted);
        public ICommand SelectedTourCopiedCommand => new RelayCommand(OnSelectedTourCopiedCommandExecuted);
        public ICommand SelectedTourDeletedCommand => new RelayCommand(OnSelectedTourDeletedCommandExecuted);


        private void OnSelectedTourEditedCommandExecuted(object obj)
        {
            
        }


        private void OnSelectedTourDeletedCommandExecuted(object obj)
        {
            Tour tour = (Tour) obj;
            if (MessageBox.Show($"Delete {tour.Name}?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                MessageBoxResult.Yes)
            {
                try
                {
                    BaseObserverSingleton.GetInstance.TourObservers.ForEach(Attach);
                    _databaseService.DeleteTour(tour.Id);
                    _fileService.DeleteImage(tour.ImagePath);
                    MessageBox.Show("Tour deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Notify();
                    BaseObserverSingleton.GetInstance.TourObservers.ForEach(Detach);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    MessageBox.Show("There was an error deleting the tour, please try again", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private void OnSelectedTourCopiedCommandExecuted(object obj)
        {
            
        }

        private void OnSelectedTourChangedCommandExecuted(object obj)
        {
            SelectedTour = (Tour) obj;
        }

        private void LoadGreetMessage()
        {
            GreetMessage = GetService<IGreetService>().Greet();
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o => o.Update(this));
        }
    }
}
