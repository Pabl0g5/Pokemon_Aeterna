using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager
{
    private IGameState _currentState;
    private IGameState _nextState;
    private bool _isTransitioning = false;

    public void ChangeState(IGameState newState)
    {
        _nextState = newState;
        _isTransitioning = true;
    }

    public void Update(GameTime gameTime)
    {
        if (_isTransitioning)
        {
            _currentState?.UnloadContent();
            _currentState = _nextState;
            _currentState.LoadContent();
            _isTransitioning = false;
        }

        _currentState?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentState?.Draw(spriteBatch);
    }
}