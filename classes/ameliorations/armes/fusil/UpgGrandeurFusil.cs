using desktop.armes;
using Microsoft.Xna.Framework.Graphics;
namespace desktop.ameliorations.arme.fusil;

public class UpgGrandeurFusil : AbstractUpgFusil
{
    public UpgGrandeurFusil(Texture2D image) : base(image, "Augmente la taille des projectiles", -1)
    {
    }

    public override void Appliquer(Fusil fusil)
    {
        fusil._rayonBalles += 3;
    }
}