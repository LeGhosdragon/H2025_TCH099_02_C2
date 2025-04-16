using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.epee;

public abstract class AbstractUpgEpee : AbstractUpgArme
{
    public AbstractUpgEpee(Texture2D image, string description, int limite) : base(image, description, limite)
    {
    }

    public override void Appliquer(IArme arme)
    {
        Appliquer((Epee) arme);
    }
    public abstract void Appliquer(Epee epee);

    public override bool estBonType(IArme arme){
            return arme is Epee;
    }

}