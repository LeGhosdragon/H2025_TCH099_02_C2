using desktop.armes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.epee;

public class UpgLageurEpee: AbstractUpgEpee{

    public UpgLageurEpee(Texture2D image) : base(image, "Augmente la taille de l'épée", -1)
    {
    }

    public override void Appliquer(Epee epee)
    {
        epee.Grandir(30);
    }
}