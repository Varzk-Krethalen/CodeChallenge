﻿using System;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Examples_Click(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).RetrieveExamples();
        }
    }
}
