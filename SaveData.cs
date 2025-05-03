using System.Collections.Generic;

namespace SpaceTrader
{
    public class SaveData
    {
        public string CurrentPortName { get; set; }
        public int Credits { get; set; }
        public int CargoLimit { get; set; }
        public Dictionary<string, int> CargoHold { get; set; } = new();
    }
}
