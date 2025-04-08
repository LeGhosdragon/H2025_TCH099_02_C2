using desktop.ameliorations.joueur;
using desktop.gameobjects;
using Microsoft.Xna.Framework.Graphics;

public class UpgRayonAttraction : AbstractUpgJoueur
{
    public UpgRayonAttraction(Texture2D image) : base(image, "Attire l'expérience plus facilement", 4)
    {
    }

    public override void Appliquer(Joueur joueur)
    {
        joueur._rayonCollection = joueur._rayonCollection * 1.3f;
    }
}