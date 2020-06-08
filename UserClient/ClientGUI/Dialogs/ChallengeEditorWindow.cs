using ClientModels.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace ClientGUI
{
    /// <summary>
    /// Interaction logic for ChallengeEditor.xaml
    /// </summary>
    public partial class ChallengeEditorDialog : Window
    { //TODO: consider using a viewmodel for proper mvvm. Can have proper listview binding then.
        private IModel Model { get; }
        public IChallenge Challenge { get; }
        public bool IsNewChallenge { get; } = true;

        private static string defaultJavaCode = 
@"public class Challenge {
    public static void main(String[] args) {
          
    }
}";

        public ChallengeEditorDialog(IModel model)
        {
            InitializeComponent();
            Model = model;
            Challenge = Model.NewChallengeInstance();
            Challenge.InitialCode = defaultJavaCode;
            DataContext = this;
            TestListView_SizeChanged(TestList, null);
            TestList.Items.Refresh();
        }

        public ChallengeEditorDialog(IModel model, IChallenge challenge)
        {
            InitializeComponent();
            Model = model;
            Challenge = challenge.GetCopy();
            IsNewChallenge = false;
            DataContext = this;
            TestListView_SizeChanged(TestList, null);
            TestList.Items.Refresh();
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

        private void Add_Test(object sender, RoutedEventArgs e)
        {
            Challenge.Tests.Add(Model.NewTestInstance());
            TestList.Items.Refresh();
            TestList.SelectedIndex = TestList.Items.Count - 1;
        }

        private void Delete_Test(object sender, RoutedEventArgs e)
        {
            if (TestList.SelectedValue != null)
            {
                Challenge.Tests.Remove(TestList.SelectedValue as ITest);
                TestList.Items.Refresh();
                TestList.SelectedIndex = TestList.Items.Count - 1;
            }
        }

        private void TestListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (listView.ActualWidth > SystemParameters.ScrollWidth) {
                GridView gView = listView.View as GridView;

                var workingWidth = listView.ActualWidth - SystemParameters.ScrollWidth * 1.5; // take into account vertical scrollbar

                gView.Columns[0].Width = workingWidth * 0.50; //50%
                gView.Columns[1].Width = workingWidth * 0.50;
            }
        }
    }
}
