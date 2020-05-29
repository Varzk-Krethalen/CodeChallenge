using ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUI
{
    class ChallengeEditorViewModel : ViewModelBase
    {
        public Challenge Challenge { get; set; }
        public bool IsNewChallenge { get; set; } = true;

        public ChallengeEditorViewModel()
        {
            Challenge = new Challenge();
        }

        public ChallengeEditorViewModel(Challenge challengeToEdit)
        {
            Challenge = challengeToEdit;
            IsNewChallenge = false;
        }
    }
}
