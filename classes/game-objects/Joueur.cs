using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public class Joueur : AbstractGameObject
{
    public Joueur(Vector2[] forme, GraphicsDevice device)
        : base(forme, device) { }
}
