using desktop.gameobjects;
using desktop.pages;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.joueur;

public abstract class AbstractUpgJoueur : Amelioration
{
    protected AbstractUpgJoueur(Texture2D image, string description,int limite) : base(image, description,limite)
    {
        
    }
    public abstract void Appliquer(Joueur joueur);
    public override void Appliquer(EcranJeu ecranJeu){
        Appliquer(ecranJeu._joueur);
        base.Appliquer(ecranJeu);
    }
    
}