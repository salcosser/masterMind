using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWPF {
    public static class MMConstants {

        public const string CC_Blue = "Blue";
        public const string CC_Red = "Red";
        public const string CC_Green = "Green";
        public const string CC_Yellow = "Yellow";
        public const string CC_Black = "Black";
        public const string CC_White = "White";

        public enum Grade : int {
            Nothing = 0,
            White = 1,
            Black = 2
        }

    }
}
