using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IGameState
{
    void LoadContent();
    void UnloadContent();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}