using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokémon_Æterna;

public class MainMenuState : IGameState
{
    private Game1 _game;
    private GameStateManager _manager;

    public MainMenuState(Game1 game, GameStateManager manager)
    {
        _game = game;
        _manager = manager;
    }

    public void LoadContent()
    {

    }

    public void UnloadContent()
    {

    }

    public void Update(GameTime gameTime)
    {

    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_game.Content.Load<SpriteFont>("Font"), "Presiona Enter Para Comenzar", new Vector2(200, 300), Color.White);
    }
}