using MasterMindWPF.Model;
using Prism.Commands;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWPF.ViewModels {
    public class BoardViewModel : BindableBase {

        public BoardViewModel() {
            Attempts = new ObservableCollection<Attempt>();
        }

        public ObservableCollection<Attempt> Attempts { get; }


        public DelegateCommand AddCommand { get { return new DelegateCommand(AddAttempt); } }


        public void AddAttempt() {
            var att = new Attempt();
            att.BlackPieces = 2;
            att.WhitePieces = 3;
            att.Piece1 = new PatternPiece();
            att.Piece2 = new PatternPiece();
            att.Piece3 = new PatternPiece();
            att.Piece4 = new PatternPiece();
            att.Piece1.Color = MMConstants.CC_Blue;
            att.Piece2.Color = MMConstants.CC_Red;
            att.Piece3.Color = MMConstants.CC_Black;
            att.Piece4.Color = MMConstants.CC_White;
            Attempts.Insert(0,att);
        }



    }
}
