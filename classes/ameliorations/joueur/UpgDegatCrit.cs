using desktop.gameobjects;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.joueur;

public class UpgDegatCrit : AbstractUpgJoueur
{
    public UpgDegatCrit(Texture2D image) : base(image, "Augmente les Degats critiques", 7)
    {
    }

    public override void Appliquer(Joueur joueur)
    {
        joueur._arme._degatCritique *= 1.3f;
    }
}