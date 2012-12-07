﻿using System.Windows;
using Skyscraper.ViewModels;

namespace Skyscraper
{
    public partial class MainWindow : Window
    {
        protected MainWindowViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new MainWindowViewModel();
        }
    }
}