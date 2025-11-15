using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

public class TileMap
{
    private Texture2D _tileset;
    private int _tileWidth, _tileHeight;
    private int _mapWidth, _mapHeight;
    private Tile[,] _tiles;
    private ContentManager _content;

    public TileMap(string jsonPath)
    {
        LoadMap(jsonPath);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                Tile tile = _tiles[x, y];
                if (tile?.SourceRect != null)
                    spriteBatch.Draw(_tileset, tile.Position, tile.SourceRect, Color.White);
            }
        }
    }

    public Tile GetTileAt(Vector2 position)
    {
        int tileX = (int)(position.X / _tileWidth);
        int tileY = (int)(position.Y / _tileHeight);

        if (tileX < 0 || tileY < 0 || tileX >= _mapWidth || tileY >= _mapHeight)
            return null;

        return _tiles[tileX, tileY];
    }

    public bool IsSolidAt(Vector2 position)
    {
        var tile = GetTileAt(position);
        return tile != null && tile.IsSolid;
    }
    
    private void LoadMap(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);
        var map = JObject.Parse(json);

        _tileWidth = (int)map["tilewidth"];
        _tileHeight = (int)map["tileheight"];
        _mapWidth = (int)map["widht"];
        _mapHeight = (int)map["height"];

        string tilesetPath = "Content/Tilesets/overworld";
        _tileset = _content.Load<Texture2D>(tilesetPath);
        _tiles = new Tile[_mapWidth, _mapHeight];

        var layers = map["layers"];
        foreach (var layer in layers)
        {
            if ((string)layer["type"] == "tilelayer")
            {
                var data = layer["data"].ToObject<int[]>();
                for (int y = 0; y < _mapHeight; y++)
                {
                    for (int x = 0; x < _mapWidth; x++)
                    {
                        int grid = data[y * _mapWidth + x];
                        if (grid == 0) continue;

                        int tilesetWidht = _tileset.Width / _tileWidth;
                        int scrX = (grid - 1) % tilesetWidht * _tileWidth;
                        int scrY = (grid - 1) / tilesetWidht * _tileHeight;

                        if (_tiles[x, y] == null)
                            _tiles[x, y] = new Tile();

                        _tiles[x, y].SourceRect = new Rectangle(scrX, scrY, _tileWidth, _tileHeight);
                        _tiles[x, y].Position = new Vector2(x * _tileWidth, y * _tileHeight);
                    }
                }
            }
            
            if ((string)layer["name"] == "Collisions")
            {
                var data = layer["data"].ToObject<int[]>();
                for (int y = 0; y < _mapHeight; y++)
                {
                    for (int x = 0; x < _mapWidth; x++)
                    {
                        if (data[y * _mapWidth + x] != 0)
                        {
                            if (_tiles[x, y] == null)
                                _tiles[x, y] = new Tile();
                            _tiles[x, y].IsSolid = true;
                        }
                    }
                }
            }
        }
    }
}
