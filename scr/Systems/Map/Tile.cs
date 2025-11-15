using System.Collections.Generic;
using Microsoft.Xna.Framework;
public class Tile
{
    public Rectangle SourceRect { get; set; }
    public Vector2 Position { get; set; }
    public bool IsSolid { get; set; }
    public Dictionary<string, string> Properties { get; private set; } = new();

    public bool HasProperty(string key) => Properties.ContainsKey(key);
}
