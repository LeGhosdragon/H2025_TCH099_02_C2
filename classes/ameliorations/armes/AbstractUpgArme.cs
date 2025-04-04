using desktop.armes;
using desktop.gameobjects;
using desktop.pages;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.arme;

public abstract class AbstractUpgArme: Amelioration{
    protected AbstractUpgArme(Texture2D image, string description, int limite) : base(image, description, limite)
    {
    }

    public abstract bool estBonType(IArme arme);

    public abstract void Appliquer(IArme arme);
    public override void Appliquer(EcranJeu ecranJeu){
        Appliquer(ecranJeu._joueur._arme);
        base.Appliquer(ecranJeu);
    }

}