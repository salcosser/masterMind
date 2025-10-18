using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterMindWPF.Model {
    public class Attempt : BindableBase {

        public Attempt() {
        }

        private int blackPieces;
        public int BlackPieces {
            get => blackPieces;
            set {
                if (Set(ref blackPieces, value)) {
                    RaisePropertyChanged(nameof(FeedbackPegs));
                }
            }
        }

        private int whitePieces;
        public int WhitePieces {
            get => whitePieces;
            set {
                if (Set(ref whitePieces, value)) {
                    RaisePropertyChanged(nameof(FeedbackPegs));
                }
            }
        }

        private int attemptOrderPosition;
        public int AttemptOrderPosition {
            get => attemptOrderPosition;
            set => Set(ref attemptOrderPosition, value);
        }

        private PatternPiece piece1;
        public PatternPiece Piece1 {
            get => piece1;
            set => Set(ref piece1, value);
        }

        private PatternPiece piece2;
        public PatternPiece Piece2 {
            get => piece2;
            set => Set(ref piece2, value);
        }

        private PatternPiece piece3;
        public PatternPiece Piece3 {
            get => piece3;
            set => Set(ref piece3, value);
        }

        private PatternPiece piece4;
        public PatternPiece Piece4 {
            get => piece4;
            set => Set(ref piece4, value);
        }


        public IReadOnlyList<FeedbackPegState> FeedbackPegs {
            get {
                var pegs = new List<FeedbackPegState>();
                pegs.AddRange(Enumerable.Repeat(FeedbackPegState.Black, BlackPieces));
                pegs.AddRange(Enumerable.Repeat(FeedbackPegState.White, WhitePieces));

                while (pegs.Count > 4) {
                    pegs.RemoveAt(pegs.Count - 1);
                }

                while (pegs.Count < 4) {
                    pegs.Add(FeedbackPegState.None);
                }

                return pegs;
            }
        }

        public void SetToDefaultPieces() {
            Piece1 = new PatternPiece();
            Piece2 = new PatternPiece();
            Piece3 = new PatternPiece();
            Piece4 = new PatternPiece();
            RaisePropertyChanged(nameof(FeedbackPegs));
        }
    }
}
