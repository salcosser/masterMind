using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWPF.Model {
    public class PatternPiece : BindableBase {
        private int colorType;
        public int ColorType {
            get { return colorType; }
            set => Set(ref colorType, value);
        }

        private string color;
        public string Color {
            get { return color; }
            set => Set(ref color, value);
        }

        private int sortOrder;
        public int SortOrder {
            get { return sortOrder; }
            set => Set(ref sortOrder, value);
        }
    }
}
