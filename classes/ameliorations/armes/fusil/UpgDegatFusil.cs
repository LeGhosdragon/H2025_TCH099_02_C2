using desktop.armes;
using Microsoft.Xna.Framework.Graphics;
namespace desktop.ameliorations.arme.fusil;

public class UpgDegatFusil : AbstractUpgFusil
{
    public UpgDegatFusil(Texture2D image) : base(image, "Augmente le degat des projectiles", -1)
    {
    }

    public override void Appliquer(Fusil fusil)
    {
        fusil._degat = fusil._degat * 1.2f;
    }
}