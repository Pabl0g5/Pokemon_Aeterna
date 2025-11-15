using System;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokémon_Æterna;

public abstract class OverworldSceneBase : IGameState
{
    protected Game1 _game;
    protected GameStateManager _manager;
    protected EncounterType _encounterType;
    protected TileMap _map;
    protected Player _player;

    //Control
    protected Random _random = new Random();
    protected double _stepsSinceLastEncounter = 0;

    public OverworldSceneBase(Game1 game1, GameStateManager manager, EncounterType encounterType)
    {
        _game = game1;
        _manager = manager;
        _encounterType = encounterType;
    }

    public virtual void LoadContent()
    {
        _map = new TileMap("Content/Maps/route1.json", _game.Content);
        _player = new Player(new Vector2(100, 100));
    }

    public virtual void UnloadContent()
    {

    }

    public virtual void Update(GameTime gameTime)
    {
        _player.Update(gameTime, _map);

        if (_encounterType != EncounterType.None)
        {
            _stepsSinceLastEncounter += _player.DistanceMovedSinceLastFrame;

            if (_stepsSinceLastEncounter > 32)
            {
                _stepsSinceLastEncounter = 0;

                if (ShouldTriggerEncounter())
                    StartEncounter();
            }
        }
    }


public virtual void Draw(SpriteBatch spriteBatch)
    {
        _map.Draw(spriteBatch);
        _player.Draw(spriteBatch);
    }

    protected virtual bool ShouldTriggerEncounter()
    {
        double chance = 0.05;

        if (_encounterType == EncounterType.OnSpecificTiles)
        {
            var currentTile = _map.GetTileAt(_player.Position);
            if (currentTile != null && currentTile.HasProperty("ValidEncounter"))
                return _random.NextDouble() < chance;
            else
                return false;
        }

        if (_encounterType == EncounterType.Anywhere)
            return _random.NextDouble() < chance;

        return false;
    }
    
    protected virtual void StartEncounter()
    {
        _manager.ChangeState(new BattleScene(_game, _manager));
    }
}