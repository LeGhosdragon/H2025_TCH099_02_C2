using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.fusil;

public class UpgVitAttaqueFusil : AbstractUpgFusil
{
    public UpgVitAttaqueFusil(Texture2D image) : base(image, "Vitesse attaque", -1)
    {
    }

    public override void Appliquer(Fusil fusil)
    {
        fusil._delai._delai = fusil._delai._delai * 0.9f;
    }
}