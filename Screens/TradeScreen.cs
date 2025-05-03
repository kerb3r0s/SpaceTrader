using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using SpaceTrader.UI;


namespace SpaceTrader.Screens
{
    public class TradeScreen
    {
        private SpriteFont font;
        private Texture2D buttonTexture;

        private List<Good> goods;
        private List<Button> buyButtons;
        private List<Button> sellButtons;

        private List<NumericInput> numericInputs;

        private int spacingY = 80;
        private int baseY = 100;

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Default");
            buttonTexture = content.Load<Texture2D>("UI/button");

            var port = GameManager.Instance.CurrentPort;
            goods = port.AvailableGoods;

            buyButtons = new List<Button>();
            sellButtons = new List<Button>();
            
            numericInputs = new List<NumericInput>();

            for (int i = 0; i < goods.Count; i++)
            {
                int y = baseY + i * spacingY;

                buyButtons.Add(new Button(new Rectangle(600, y, 80, 40), "Buy", font, buttonTexture));
                sellButtons.Add(new Button(new Rectangle(700, y, 80, 40), "Sell", font, buttonTexture));

                var inputRect = new Rectangle(500, y, 80, 40);
                numericInputs.Add(new NumericInput(inputRect, font, buttonTexture));
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < goods.Count; i++)
            {
                buyButtons[i].Update(gameTime);
                sellButtons[i].Update(gameTime);

                var good = goods[i];
                numericInputs[i].Update(gameTime);
                int quantity = numericInputs[i].Value;
                var player = GameManager.Instance.Player;

                int totalBuyCost = good.BasePrice * quantity;
                int ownedQty = player.CargoHold.ContainsKey(good) ? player.CargoHold[good] : 0;

                // Handle Buy
                if (buyButtons[i].WasClicked)
                {
                    if (player.Credits >= totalBuyCost && player.CanAddCargo(quantity))
                    {
                        player.Credits -= totalBuyCost;
                        if (!player.CargoHold.ContainsKey(good))
                            player.CargoHold[good] = 0;
                        player.CargoHold[good] += quantity;
                    }
                }

                // Handle Sell
                if (sellButtons[i].WasClicked)
                {
                    if (ownedQty >= quantity)
                    {
                        player.Credits += good.BasePrice * quantity;
                        player.CargoHold[good] -= quantity;
                        if (player.CargoHold[good] <= 0)
                            player.CargoHold.Remove(good);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, $"Credits: {GameManager.Instance.Player.Credits}", new Vector2(50, 20), Color.White);

            for (int i = 0; i < goods.Count; i++)
            {
                int y = baseY + i * spacingY;
                var good = goods[i];
                int ownedQty = GameManager.Instance.Player.CargoHold.ContainsKey(good) ? GameManager.Instance.Player.CargoHold[good] : 0;
                int qty = numericInputs[i].Value;
                string line = $"{good.Name} - ${good.BasePrice} | Owned: {ownedQty} | Qty: {qty}";
                spriteBatch.DrawString(font, line, new Vector2(50, y), Color.LightGray);
                numericInputs[i].Draw(spriteBatch);
                buyButtons[i].Draw(spriteBatch);
                sellButtons[i].Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
