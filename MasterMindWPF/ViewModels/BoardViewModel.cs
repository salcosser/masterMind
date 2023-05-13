using MasterMindWPF.Model;
using Prism.Commands;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWPF.ViewModels {
    public class BoardViewModel : BindableBase {

        public BoardViewModel() {
            Attempts = new ObservableCollection<Attempt>();
            ColorOptions = new List<string>();
            SetupLookups();
            NewGame();
        }

        public ObservableCollection<Attempt> Attempts { get; }
        public List<string> ColorOptions { get; }

        public DelegateCommand TryAttemptCommand { get { return new DelegateCommand(TryAttempt); } }
        public DelegateCommand NewGameCommand { get { return new DelegateCommand(NewGame); } }

        private int attemptCount;
        public int AttemptCount {
            get { return attemptCount; }
            set => Set(ref attemptCount, value);
        }


        private bool targetPatternVisible;
        public bool TargetPatternVisible {
            get { return targetPatternVisible; }
            set => Set(ref targetPatternVisible, value);
        }

        private bool attemptsEnabled;
        public bool AttemptsEnabled {
            get { return attemptsEnabled; }
            set => Set(ref attemptsEnabled, value);
        }


        private string selectedColor1;
        public string SelectedColor1 {
            get { return selectedColor1; }
            set => Set(ref selectedColor1, value);
        }

        private string selectedColor2;
        public string SelectedColor2 {
            get { return selectedColor2; }
            set => Set(ref selectedColor2, value);
        }

        private string selectedColor3;
        public string SelectedColor3 {
            get { return selectedColor3; }
            set => Set(ref selectedColor3, value);
        }

        private string selectedColor4;
        public string SelectedColor4 {
            get { return selectedColor4; }
            set => Set(ref selectedColor4, value);
        }


        private string targetColorDisplay1;
        public string TargetColorDisplay1 {
            get { return targetColorDisplay1; }
            set => Set(ref targetColorDisplay1, value);
        }

        private string targetColorDisplay2;
        public string TargetColorDisplay2 {
            get { return targetColorDisplay2; }
            set => Set(ref targetColorDisplay2, value);
        }

        private string targetColorDisplay3;
        public string TargetColorDisplay3 {
            get { return targetColorDisplay3; }
            set => Set(ref targetColorDisplay3, value);
        }

        private string targetColorDisplay4;
        public string TargetColorDisplay4 {
            get { return targetColorDisplay4; }
            set => Set(ref targetColorDisplay4, value);
        }

        public void TryAttempt() {
            var att = new Attempt();
          
            var p1 = new PatternPiece();
            p1.Color = SelectedColor1;
            att.Piece1 = p1;

            var p2 = new PatternPiece();
            p2.Color = SelectedColor2;
            att.Piece2 = p2;

            var p3 = new PatternPiece();
            p3.Color = SelectedColor3;
            att.Piece3 = p3;
            
            var p4 = new PatternPiece();
            p4.Color = SelectedColor4;
            att.Piece4 = p4;

            GradeAttempt(ref att);

            Attempts.Insert(0, att);
            AttemptCount = Attempts.Count();


            if (att.BlackPieces == 4) {
                GameOver();
            }
        }


        public void GameOver() {
            AttemptsEnabled = false;
            TargetPatternVisible = true;
        }

        public void NewGame() {
            TargetPatternVisible = false;
            GeneratePattern();
            Attempts.Clear();
            AttemptCount = 0;
            AttemptsEnabled = true;
        }


        public void GeneratePattern() {
           TargetColorDisplay1 = GetRandomColor();
           TargetColorDisplay2 = GetRandomColor();
           TargetColorDisplay3 = GetRandomColor();
           TargetColorDisplay4 = GetRandomColor();
        }

        public void GradeAttempt(ref Attempt attempt) {

            var usableTargets = new int[] { 1, 1, 1, 1 };
            var targetDict = new Dictionary<int, string>();
            targetDict.Add(0, TargetColorDisplay1);
            targetDict.Add(1, TargetColorDisplay2);
            targetDict.Add(2, TargetColorDisplay3);
            targetDict.Add(3, TargetColorDisplay4);

            var attemptDict = new Dictionary<int, string>();
            attemptDict.Add(0, attempt.Piece1.Color);
            attemptDict.Add(1, attempt.Piece2.Color);
            attemptDict.Add(2, attempt.Piece3.Color);
            attemptDict.Add(3, attempt.Piece4.Color);



            for(int i = 0; i < 4; i++) {
                if (attemptDict[i].Equals(targetDict[i])) {
                    attempt.BlackPieces++;
                    targetDict.Remove(i);
                    attemptDict.Remove(i);
                }
            }

            var remainingSet = targetDict.Select(t => t.Value).ToList();

            foreach(var remainingKey in attemptDict.Keys) {
                var idx = remainingSet.IndexOf(attemptDict[remainingKey]);

                if (idx > -1) {
                    attempt.WhitePieces++;
                    remainingSet.RemoveAt(idx);
                }
            }

        }

        private string GetRandomColor() {
            var random = new Random();
            var randInt = random.Next(1, 7);

            switch(randInt) {
                case 1:
                    return MMConstants.CC_Blue;
                case 2:
                    return MMConstants.CC_Red;
                case 3:
                    return MMConstants.CC_Green;
                case 4:
                    return MMConstants.CC_Yellow;
                case 5:
                    return MMConstants.CC_White;
                case 6:
                    return MMConstants.CC_Black;
                default:
                    return null;
            }
        }




        public void SetupLookups() {
            ColorOptions.Add(MMConstants.CC_Blue);
            ColorOptions.Add(MMConstants.CC_Red);
            ColorOptions.Add(MMConstants.CC_Green);
            ColorOptions.Add(MMConstants.CC_Yellow);
            ColorOptions.Add(MMConstants.CC_Black);
            ColorOptions.Add(MMConstants.CC_White);
        }
    }
}
