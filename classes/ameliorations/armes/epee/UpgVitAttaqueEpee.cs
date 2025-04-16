using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.epee;

public class UpgVitAttaqueEpee : AbstractUpgEpee
{
    public UpgVitAttaqueEpee(Texture2D image) : base(image, "Augmente la vitesse d'attaque de l'épée", -1)
    {
    }

    public override void Appliquer(Epee epee)
    {
        epee._delai._delai = epee._delai._delai*0.8f;
    }
}