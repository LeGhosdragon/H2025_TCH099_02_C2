using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.epee;

public class UpgAireEpee : AbstractUpgEpee
{
    public UpgAireEpee(Texture2D image) : base(image, "Augmente l'angle des attaques", -1)
    {
    }

    public override void Appliquer(Epee epee)
    {
        epee._angleZone *= 1.1f;
    }
}