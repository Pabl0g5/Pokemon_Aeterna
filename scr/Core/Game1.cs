using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pokémon_Æterna;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private readonly GameStateManager _gameStateManager;

    private double ticRate = 30;
    private double[] ticOptions = { 30, 60, 90, 120 };
    private int ticIndex = 0;

    private KeyboardState currentKey;
    private KeyboardState previousKey;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _gameStateManager = new GameStateManager();

        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / ticRate);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _gameStateManager.ChangeState(new MainMenuState(this, _gameStateManager));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if (IsKeyPressed(Keys.Add)) IncreaseTic();
        if (IsKeyPressed(Keys.Subtract)) DecreaseTic();

        previousKey = currentKey;
        base.Update(gameTime);
    }

    private bool IsKeyPressed(Keys key)
    {
        return currentKey.IsKeyDown(key) && previousKey.IsKeyUp(key);
    }

    private void IncreaseTic()
    {
        ticIndex = Math.Min(ticIndex + 1, ticOptions.Length -1);
        ticRate = ticOptions[ticIndex];
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / ticRate);
        Console.WriteLine($"Tics por segundo: {ticRate}");
    }

    private void DecreaseTic()
    {
        ticIndex = Math.Max(ticIndex -1, 0);
        ticRate = ticOptions[ticIndex];
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / ticRate);
        Console.WriteLine($"Tics por segundo: {ticRate}");
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
