using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Kalendar.Classes;

namespace WpfCalendar.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Calendar.SelectedDatesChanged += Calendar_SelectedDateChanged;

            LoadEvents(DateTime.Today);
        }

        private void LoadEvents(DateTime date)
        {
            EventsList.Items.Clear();
            List<string> events = FileService.GetEvents(date);

            if (events.Count == 0)
                EventsList.Items.Add("Нет событий");
            else
                foreach (string e in events)
                    EventsList.Items.Add(e);
        }

        private void Calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Calendar.SelectedDate.HasValue)
                LoadEvents(Calendar.SelectedDate.Value);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = Calendar.SelectedDate ?? DateTime.Today;
            AddWindow addWindow = new AddWindow(date);
            addWindow.ShowDialog();
            LoadEvents(date);
        }
    }
}