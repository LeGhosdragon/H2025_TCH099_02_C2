using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public class Monstre : AbstractGameObject
{
    public Monstre(Vector2[] forme, Vector3 position)
        : base(forme, position) { }
}
