using MasterMindWPF.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MasterMindWPF.ViewModels
{
    public class BoardViewModel : BindableBase {

        public BoardViewModel() {
            Attempts = new ObservableCollection<Attempt>();
            RecordedGames = new ObservableCollection<PreviousGame>();
            SetupLookups();
            NewGame();
           
        }
        public ObservableCollection<Attempt> Attempts { get; }
        public ObservableCollection<PreviousGame> RecordedGames { get; }

        public List<Brush> ColorOptions { get; set; }

        public DelegateCommand TryAttemptCommand { get { return new DelegateCommand(TryAttempt); } }
        public DelegateCommand NewGameCommand { get { return new DelegateCommand(NewGame); } }
        public DelegateCommand GiveUpCommand { get { return new DelegateCommand(() => GameOver(false)); } }

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
        private bool wonGame;
        public bool WonGame {
            get { return wonGame; }
            set => Set(ref wonGame, value);
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
            if (string.IsNullOrEmpty(SelectedColor1)) {
                MessageBox.Show("Select a color for the first piece.","Missing Color");
                return;
            }

            if (string.IsNullOrEmpty(SelectedColor2)) {
                MessageBox.Show("Select a color for the second piece.", "Missing Color");
                return;
            }

            if (string.IsNullOrEmpty(SelectedColor3)) {
                MessageBox.Show("Select a color for the third piece.", "Missing Color");
                return;
            }

            if (string.IsNullOrEmpty(SelectedColor4)) {
                MessageBox.Show("Select a color for the fourth piece.", "Missing Color");
                return;
            }

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
                GameOver(true);
            }
        }

        public void GameOver(bool wonGame = false) {
            WonGame = wonGame;
            if (WonGame) {
                RecordGame();
            }
            AttemptsEnabled = false;
            TargetPatternVisible = true;
        }

        public void NewGame() {
            WonGame = false;
            TargetPatternVisible = false;
            GeneratePattern();
            Attempts.Clear();
            AttemptCount = 0;
            AttemptsEnabled = true;
        }

        private void RecordGame() {
            var game = new PreviousGame();
            game.Tries = AttemptCount;
            RecordedGames.Add(game);
            var orderedGames = RecordedGames.OrderBy(g => g.Tries).ToList();
            for(int i = 0; i < orderedGames.Count; i++) {
                orderedGames[i].Rank = i + 1;
            }
            RecordedGames.Clear();
            RecordedGames.AddRange(orderedGames);
        }

        public void GeneratePattern() {
           var rand = new Random(Guid.NewGuid().GetHashCode());
           TargetColorDisplay1 = GetRandomColor(rand);
           TargetColorDisplay2 = GetRandomColor(rand);
           TargetColorDisplay3 = GetRandomColor(rand);
           TargetColorDisplay4 = GetRandomColor(rand);
        }

        public void GradeAttempt(ref Attempt attempt) {

            attempt.BlackPieces = 0;
            attempt.WhitePieces = 0;

            var guess = new[]
            {
                attempt.Piece1.ColorName,
                attempt.Piece2.ColorName,
                attempt.Piece3.ColorName,
                attempt.Piece4.ColorName
            };

            var target = new[]
            {
                TargetColorDisplay1,
                TargetColorDisplay2,
                TargetColorDisplay3,
                TargetColorDisplay4
            };

            var guessUsed = new bool[4];
            var targetUsed = new bool[4];

            for (var i = 0; i < 4; i++) {
                if (!string.IsNullOrEmpty(guess[i]) && guess[i] == target[i]) {
                    attempt.BlackPieces++;
                    guessUsed[i] = true;
                    targetUsed[i] = true;
                }
            }

            for (var i = 0; i < 4; i++) {
                if (guessUsed[i] || string.IsNullOrEmpty(guess[i])) {
                    continue;
                }

                for (var j = 0; j < 4; j++) {
                    if (targetUsed[j] || string.IsNullOrEmpty(target[j])) {
                        continue;
                    }

                    if (guess[i] == target[j]) {
                        attempt.WhitePieces++;
                        targetUsed[j] = true;
                        break;
                    }
                }
            }

        }

        private string GetRandomColor(Random random) {
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

            ColorOptions = new List<Brush>{
                new SolidColorBrush(Colors.Red),
                new SolidColorBrush(Colors.Blue),
                new SolidColorBrush(Colors.Green),
                new SolidColorBrush(Colors.Yellow),
                new SolidColorBrush(Colors.Black),
                new SolidColorBrush(Colors.White)
            };
        }
       
    }
}
