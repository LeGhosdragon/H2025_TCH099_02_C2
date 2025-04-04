using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme.fusil;

public abstract class AbstractUpgFusil : AbstractUpgArme
{
    public AbstractUpgFusil(Texture2D image, string description, int limite) : base(image, description, limite)
    {
    }

    public override void Appliquer(IArme arme)
    {
        Appliquer((Fusil) arme);
    }
    public abstract void Appliquer(Fusil fusil);

    public override bool estBonType(IArme arme){
            return arme is Fusil;
    }

}