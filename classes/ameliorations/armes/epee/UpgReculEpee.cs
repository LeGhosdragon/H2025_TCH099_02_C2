using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.epee;

public class UpgReculEpee : AbstractUpgEpee
{
    public UpgReculEpee(Texture2D image) : base(image, "Ameliore le recul infligé aux monstres", -1)
    {
    }

    public override void Appliquer(Epee epee)
    {
        epee._recul += 10;
    }
}
