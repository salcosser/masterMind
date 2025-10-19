using System;
using System.Collections.ObjectModel;

namespace MasterMindWPF.Model {
    public class Attempt : BindableBase {

        private readonly ObservableCollection<FeedbackPegState> feedbackPegs;
        private readonly ReadOnlyObservableCollection<FeedbackPegState> readOnlyFeedbackPegs;

        public Attempt() {
            feedbackPegs = new ObservableCollection<FeedbackPegState>();
            readOnlyFeedbackPegs = new ReadOnlyObservableCollection<FeedbackPegState>(feedbackPegs);
            UpdateFeedbackPegs();
        }

        private int blackPieces;
        public int BlackPieces {
            get => blackPieces;
            set {
                if (Set(ref blackPieces, value)) {
                    UpdateFeedbackPegs();
                }
            }
        }

        private int whitePieces;
        public int WhitePieces {
            get => whitePieces;
            set {
                if (Set(ref whitePieces, value)) {
                    UpdateFeedbackPegs();
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

        public ReadOnlyObservableCollection<FeedbackPegState> FeedbackPegs => readOnlyFeedbackPegs;

        private void UpdateFeedbackPegs() {
            feedbackPegs.Clear();

            var blacks = Math.Max(0, Math.Min(BlackPieces, 4));
            var whites = Math.Max(0, Math.Min(WhitePieces, 4));

            for (var i = 0; i < blacks && feedbackPegs.Count < 4; i++) {
                feedbackPegs.Add(FeedbackPegState.Black);
            }

            for (var i = 0; i < whites && feedbackPegs.Count < 4; i++) {
                feedbackPegs.Add(FeedbackPegState.White);
            }

            while (feedbackPegs.Count < 4) {
                feedbackPegs.Add(FeedbackPegState.None);
            }

            while (feedbackPegs.Count > 4) {
                feedbackPegs.RemoveAt(feedbackPegs.Count - 1);
            }
        }

        public void SetToDefaultPieces() {
            Piece1 = new PatternPiece();
            Piece2 = new PatternPiece();
            Piece3 = new PatternPiece();
            Piece4 = new PatternPiece();
            BlackPieces = 0;
            WhitePieces = 0;
            UpdateFeedbackPegs();
        }
    }
}
