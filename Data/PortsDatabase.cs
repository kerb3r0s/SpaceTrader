using System.Collections.Generic;

namespace SpaceTrader
{
    public static class PortsDatabase
    {
        public static List<Port> AllPorts { get; private set; }

        static PortsDatabase()
        {
            AllPorts = new List<Port>
            {
                new Port("Mercury Foundry Complex", "Blistering factories melt metal near the sun.", PortZone.Inner, "Ports/mercury"),
                new Port("Venus Sky Habitats", "Floating cities hover above acid clouds.", PortZone.Inner, "Ports/mercury"),
                new Port("New Lagos, Mars", "Martian frontier full of miners and misfits.", PortZone.Inner, "Ports/mercury"),
                new Port("Ceres Free Port", "Neutral ground for traders and asteroid miners.", PortZone.Inner, "Ports/mercury"),

                new Port("Europa Ice Docks", "Subsurface ocean labs and illicit biotrade.", PortZone.Outer, "Ports/mercury"),
                new Port("Titan Noir Outpost", "Foggy noir smuggler den under Saturn’s eye.", PortZone.Outer, "Ports/mercury"),

                new Port("Pluto Relic Vault", "Forbidden site of ancient tech discoveries.", PortZone.Fringe, "Ports/mercury"),
                new Port("Kuiper Flotilla", "Nomadic pirate fleet beyond Neptune.", PortZone.Fringe, "Ports/mercury")
            };
        }

        public static List<Port> GetPortsByZone(PortZone zone)
        {
            return AllPorts.FindAll(p => p.Zone == zone);
        }

        public static Port GetRandomInnerPort()
        {
            var innerPorts = GetPortsByZone(PortZone.Inner);
            var rng = new System.Random();
            return innerPorts[rng.Next(innerPorts.Count)];
        }
    }
}
