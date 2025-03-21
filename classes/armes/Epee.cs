using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.armes;

public class Epee : AbstractArme
{
    public Epee(Joueur joueur)
        : base(PolyGen.GetPoly(4, 5), joueur.getPosition(), joueur, 1f) { }

    public override void utiliser()
    {
        throw new System.NotImplementedException();
    }


    /*
    Section Graphique
    */
    public override void Draw(GraphicsDevice device)
    {
        base.Draw(device);
    }
}
