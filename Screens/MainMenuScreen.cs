using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
// using SpaceTrader.Screens;
using SpaceTrader.UI;
using System.Collections.Generic;

namespace SpaceTrader.Screens
{
    public class MainMenuScreen : IScreen
    {
        private SpriteFont font;
        private Texture2D buttonTexture;
        private Texture2D backgroundTexture;
        private List<Button> buttons;
        
        private string[] labels = new[] { "New Game", "Load Game", "Save Game", "Quit" };

        public void Refresh(ContentManager content)
        {
            // This method can be used to refresh the screen if needed
            // For now, it does nothing
        }
        public void LoadContent(GraphicsDevice graphics, ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/Default");
            buttonTexture = content.Load<Texture2D>("UI/button");
            backgroundTexture = content.Load<Texture2D>("UI/MainMenu");
            buttons = new List<Button>();
            int startY = 250;
            int spacing = 70;

            for (int i = 0; i < labels.Length; i++)
            {
                var rect = new Rectangle(500, startY + i * spacing, 300, 50);
                buttons.Add(new Button(rect, labels[i], font, buttonTexture));
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(gameTime);

                if (buttons[i].WasClicked)
                {
                    HandleClick(i);
                }
            }
        }

        private void HandleClick(int index)
        {
            switch (labels[index])
            {
                case "New Game":
                    GameManager.Instance.StartNewGame();
                    break;
                case "Load Game":
                    GameManager.Instance.LoadGame();
                    break;
                case "Save Game":
                    SaveLoadManager.SaveGame(GameManager.Instance.Player);
                    break;
                case "Quit":
                    Game1.ExitGame();
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1600, 900), Color.White);
            spriteBatch.DrawString(font, "SPACE SMUGGLER", new Vector2(480, 150), Color.White);

            foreach (var btn in buttons)
                btn.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
