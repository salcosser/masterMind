using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MasterMindWPF.Model {
    public class PatternPiece : BindableBase {
        private int colorType;
        public int ColorType {
            get { return colorType; }
            set
            {
                Set(ref colorType, value);
            }
        }

        private string color;
        public string Color {
            get { return color; }
            set
            {
                Set(ref color, value);
                ColorName = MMConstants.ColorHexToName.TryGetValue(this.Color ?? string.Empty, out var cName) ? cName : string.Empty;
            }
        }

        private int sortOrder;
        public int SortOrder {
            get { return sortOrder; }
            set => Set(ref sortOrder, value);
        }


        private string colorName;
        public string ColorName {
            get => colorName;
            set => Set(ref colorName, value);
        }

    }
}
