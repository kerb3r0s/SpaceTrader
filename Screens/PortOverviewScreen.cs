using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SpaceTrader.UI;

namespace SpaceTrader.Screens
{
    public class PortOverviewScreen
    {
        private SpriteFont font;
        private Texture2D backgroundTexture;
        private string portName;
        private string portDescription;
        // private Rectangle continueButtonRect;
        private Texture2D buttonTexture;
        private Button continueButton;

        private MouseState previousMouseState;

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            var currentPort = GameManager.Instance.CurrentPort;
            portName = currentPort.Name;
            portDescription = currentPort.Description;

            backgroundTexture = content.Load<Texture2D>(currentPort.BackgroundImagePath);
            font = content.Load<SpriteFont>("Fonts/Default");
            buttonTexture = content.Load<Texture2D>("UI/button");  // Placeholder button image

            // continueButtonRect = new Button(new Rectangle(600, 500, 200, 60), "Continue", font, buttonTexture); // Adjust as needed
            continueButton = new Button(new Rectangle(600, 400, 150, 50), "Continue", font, buttonTexture);
        }

        public void Update(GameTime gameTime)
        {
            continueButton.Update(gameTime);

            if (continueButton.WasClicked)
            {
                GameManager.Instance.SetGameState(GameState.TradeScreen);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.DrawString(font, portName, new Vector2(50, 30), Color.White);
            spriteBatch.DrawString(font, portDescription, new Vector2(50, 80), Color.LightGray);

            // spriteBatch.Draw(buttonTexture, continueButtonRect, Color.White);
            continueButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
