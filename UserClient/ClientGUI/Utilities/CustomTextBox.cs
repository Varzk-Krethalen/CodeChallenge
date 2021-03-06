﻿using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using AdonisUI;

namespace ClientGUI
{
    public class CustomTextBox : TextBox //Allows for a custom tab size
    {
        public CustomTextBox()
        {
            TextWrapping = TextWrapping.Wrap;
            AcceptsTab = true;
            AcceptsReturn = true;
            ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);
        }

        public int TabSize { get; set; } = 4;

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                string tab = new string(' ', TabSize);
                int caretPosition = CaretIndex;
                Text = Text.Insert(caretPosition, tab);
                CaretIndex = caretPosition + TabSize;
                e.Handled = true;
            }
        }
    }
}
