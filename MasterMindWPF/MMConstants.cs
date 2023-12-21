using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MasterMindWPF {
    public static class MMConstants {

        public const string CC_Blue = "Blue";
        public const string CC_Red = "Red";
        public const string CC_Green = "Green";
        public const string CC_Yellow = "Yellow";
        public const string CC_Black = "Black";
        public const string CC_White = "White";



        //ColorOptions = new List<Brush>{
        //        new SolidColorBrush(Colors.Red),
        //        new SolidColorBrush(Colors.Blue),
        //        new SolidColorBrush(Colors.Green),
        //        new SolidColorBrush(Colors.Yellow),
        //        new SolidColorBrush(Colors.Black),
        //        new SolidColorBrush(Colors.White)
        //    };


        public static Dictionary<string, string> ColorNameToHex = new Dictionary<string, string>
        {
            {CC_Red, Colors.Red.ToString()},
            {CC_Blue, Colors.Blue.ToString()},
            {CC_Green, Colors.Green.ToString()},
            {CC_Yellow, Colors.Yellow.ToString()},
            {CC_Black, Colors.Black.ToString()},
            {CC_White, Colors.White.ToString()}
        };

        public static Dictionary<string, string> ColorHexToName = ColorNameToHex.ToDictionary(kv => kv.Value, kv => kv.Key);
    public enum Grade : int {
            Nothing = 0,
            White = 1,
            Black = 2
        }

    }
}
