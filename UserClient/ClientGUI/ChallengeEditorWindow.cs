using ClientModels.Interfaces;
using System.Windows;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for ChallengeEditor.xaml
    /// </summary>
    public partial class ChallengeEditorDialog : Window
    {
        private IModel Model { get; }
        public IChallenge Challenge { get; }
        public bool IsNewChallenge { get; } = true;

        public ChallengeEditorDialog()
        {
            InitializeComponent();
            Challenge = Model.NewChallengeInstance();
            DataContext = this;
        }

        public ChallengeEditorDialog(IModel model, IChallenge challenge)
        {
            InitializeComponent();
            Model = model;
            Challenge = challenge;
            IsNewChallenge = false;
            DataContext = this;
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
