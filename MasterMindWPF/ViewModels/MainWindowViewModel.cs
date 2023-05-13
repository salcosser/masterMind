using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWPF.ViewModels {
    public class MainWindowViewModel : BindableBase{
        public MainWindowViewModel() {
            BoardViewModel = new BoardViewModel();
        }

        public BoardViewModel BoardViewModel { get; set; }
    }
}
