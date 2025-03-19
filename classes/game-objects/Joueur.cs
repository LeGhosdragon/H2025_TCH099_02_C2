using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public class Joueur : AbstractGameObject
{
    public Joueur(VertexPositionColor[] forme, GraphicsDevice device)
        : base(forme, device) { }
}
