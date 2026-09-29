using System;
using System.Windows;
using Kalendar.Classes;

namespace WpfCalendar.Views
{
    public partial class AddWindow : Window
    {
        public AddWindow(DateTime date)
        {
            InitializeComponent();
            DatePicker.SelectedDate = date;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate == null || string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            FileService.SaveEvent(DatePicker.SelectedDate.Value, DescriptionBox.Text);
            MessageBox.Show($"Событие добавлено!\nФайл: {FileService.PathFile}");
            Close();
        }
    }
}