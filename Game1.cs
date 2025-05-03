using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceTrader.Screens;
using SpaceTrader.UI;

namespace SpaceTrader;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    PortOverviewScreen portOverviewScreen;
    TradeScreen tradeScreen;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GameManager.Instance.StartNewGame();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        GameManager.Instance.StartNewGame();
        portOverviewScreen = new PortOverviewScreen();
        portOverviewScreen.LoadContent(GraphicsDevice, Content);
        tradeScreen = new TradeScreen();
        tradeScreen.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //     Exit();

        switch (GameManager.Instance.CurrentState)
        {
            case GameState.PortOverview:
                // Show port overview screen
                portOverviewScreen.Update(gameTime);
                break;
            case GameState.TradeScreen:
                // Show list of items for trade
                tradeScreen.Update(gameTime);
                break;
            case GameState.TravelScreen:
                // Show travel destinations and costs
                break;
            case GameState.GameOver:
                // Show game over screen
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        switch (GameManager.Instance.CurrentState)
        {
            case GameState.PortOverview:
                portOverviewScreen.Draw(_spriteBatch);
                break;
            case GameState.TradeScreen:
                // Show list of items for trade
                tradeScreen.Draw(_spriteBatch);
                break;
            case GameState.TravelScreen:
                // Show travel destinations and costs
                break;
            case GameState.GameOver:
                // Show game over screen
                break;
        }
        base.Draw(gameTime);
    }
}
