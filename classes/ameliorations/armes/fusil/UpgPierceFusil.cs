using desktop.ameliorations.arme.fusil;
using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

public class UpgPierceFusil : AbstractUpgFusil
{
    public UpgPierceFusil(Texture2D image) : base(image, "Permet au projectile de transpercer un ennemi supplementaire", -1)
    {
    }

    public override void Appliquer(Fusil fusil)
    {
        fusil._pierce++;
    }
}