using desktop.gameobjects;
using desktop.pages;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.joueur;

public class UpgVitMouvement : AbstractUpgJoueur
{
    public UpgVitMouvement(Texture2D image) : base(image, "description",-1)
    {
    }

    public override void Appliquer(Joueur joueur)
    {
        joueur._vitesse += 100;
    }
}