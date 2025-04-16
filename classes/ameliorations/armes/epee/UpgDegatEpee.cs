using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.epee;

public class UpgDegatEpee : AbstractUpgEpee
{
    public UpgDegatEpee(Texture2D image) : base(image, "Augmente le degat de l'épée", -1)
    {
    }

    public override void Appliquer(Epee epee)
    {
        epee._degat = epee._degat * 1.2f;
    }
}