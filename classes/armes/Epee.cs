using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.armes;

public class Epee : AbstractArme
{
    public Epee(Joueur joueur)
        : base(PolyGen.GetPoly(4, 5), joueur.getPosition(), joueur, 1f, 5) { }

    public override void utiliser() { }

    /*
    Section Graphique
    */
    public override void Draw(GraphicsDevice device)
    {
        base.Draw(device);
    }
}
