using desktop.gameobjects;
using Microsoft.Xna.Framework.Graphics;
namespace desktop.ameliorations.joueur;
public class UpgVieJoueur : AbstractUpgJoueur
{
    public UpgVieJoueur(Texture2D image) : base(image, "Augmente la vie tu personnage", -1)
    {
    }

    public override void Appliquer(Joueur joueur)
    {
        joueur._hp +=  joueur._hpBase * 0.2f;
    }
}