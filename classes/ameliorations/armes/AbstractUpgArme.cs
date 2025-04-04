using desktop.armes;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme;

public abstract class AbstractUpgArme: Amelioration{
    protected AbstractUpgArme(Texture2D image, string description, int limite) : base(image, description, limite)
    {
    }

    public abstract bool estBonType(AbstractArme arme);
    
}