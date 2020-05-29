using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for ChallengeEditor.xaml
    /// </summary>
    public partial class ChallengeEditorWindow : Window
    {
        public ChallengeEditorWindow()
        {
            InitializeComponent();
        }

        private void Apply_Button(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
