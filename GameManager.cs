using System;
using System.Collections.Generic;

namespace SpaceTrader
{
    public class GameManager
    {
        public static GameManager Instance { get; private set; } = new GameManager();

        public PlayerData Player { get; private set; }
        public GameState CurrentState { get; private set; }
        public Port CurrentPort => Player.CurrentPort;

        private GameManager() { }

        public void StartNewGame()
        {
            var startingPort = PortsDatabase.GetRandomInnerPort();
            Player = new PlayerData(startingCredits: 500, cargoLimit: 30);
            Player.CurrentPort = startingPort;
            LoadGoodsForCurrentPort();

            CurrentState = GameState.PortOverview;
        }

        public void TravelToPort(Port destination, int cost)
        {
            if (Player.Credits >= cost)
            {
                Player.Credits -= cost;
                Player.CurrentPort = destination;
                LoadGoodsForCurrentPort();
                CurrentState = GameState.PortOverview;
            }
            else
            {
                // Handle insufficient funds
                // (e.g., show warning or disable option in UI)
            }
        }

        public void LoadGoodsForCurrentPort()
        {
            var available = GoodsDatabase.GetCommonAndMidTier(6);
            Player.CurrentPort.AvailableGoods = available;
        }

        public void SetGameState(GameState newState)
        {
            CurrentState = newState;
        }
    }
}
