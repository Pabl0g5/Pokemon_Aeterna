using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player
{
    public Vector2 Position;
    public float Speed = 100f; // px/s
    private Texture2D _texture;
    private Vector2 _lastPosition;
    public double DistanceMovedSinceLastFrame => Vector2.Distance(Position, _lastPosition);

    public Player(Vector2 startPosition)
    {
        Position = startPosition;
    }

    public void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Sprites/Player");
    }

    public void Update(GameTime gameTime, TileMap map)
    {
        _lastPosition = Position;

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 move = Vector2.Zero;

        var k = Keyboard.GetState();
        if (k.IsKeyDown(Keys.Up)) move.Y -= 1;
        if (k.IsKeyDown(Keys.Down)) move.Y += 1;
        if (k.IsKeyDown(Keys.Left)) move.X -= 1;
        if (k.IsKeyDown(Keys.Right)) move.X += 1;

        if (move != Vector2.Zero)
        {
            move.Normalize();
            Vector2 newPos = Position + move * Speed * delta;

            // Verificar colisión simple con tiles sólidas
            if (!map.IsSolidAt(newPos))
                Position = newPos;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_texture != null)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}