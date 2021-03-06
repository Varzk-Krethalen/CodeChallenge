﻿using ClientModels.Interfaces;
using System.Windows;

namespace ClientGUI.Dialogs
{
    /// <summary>
    /// Interaction logic for UserEditorControl.xaml
    /// </summary>
    public partial class UserEditorDialog : Window
    {
        public enum UserField { NAME, PASS, TYPE }
        private IModel Model { get; }
        public IUser User { get; }
        public string NewPassword { get; set; }
        public bool IsNewUser { get; } = true;
        public bool IsEditUser { get => !IsNewUser; }
        public UserField EditedField { get; set; }

        public UserEditorDialog(IModel model)
        {
            InitializeComponent();
            Model = model;
            User = Model.NewUserInstance();
            DataContext = this;
        }

        public UserEditorDialog(IModel model, IUser user)
        {
            InitializeComponent();
            Model = model;
            User = user.GetCopy();
            IsNewUser = false;
            DataContext = this;
        }

        private void Save_Username(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            EditedField = UserField.NAME;
            Close();
        }

        private void Save_Password(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            EditedField = UserField.PASS;
            Close();
        }

        private void Save_UserType(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            EditedField = UserField.TYPE;
            Close();
        }

        private void New_User(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
