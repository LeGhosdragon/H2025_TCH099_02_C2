using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.fusil;

public class UpgReculFusil : AbstractUpgFusil
{
    public UpgReculFusil(Texture2D image) : base(image, "Ameliore le recul infligé aux monstres", -1)
    {
    }

    public override void Appliquer(Fusil fusil)
    {
        fusil._recul +=2;
    }
}