using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWPF.Model {
    public class PreviousGame {
        public Guid Id => _guid;

        private readonly Guid _guid = Guid.NewGuid();
        public int Rank { get; set; }
        public int Tries {  get; set; }

        public string AsText {
            get {
                return $"#{Rank} | {Tries} Tries";
            }
        }

        public override bool Equals(object obj) {
            return obj is PreviousGame game &&
                   _guid.Equals(game._guid);
        }

        public override int GetHashCode() {
            return -2045414129 + _guid.GetHashCode();
        }
    }
}
