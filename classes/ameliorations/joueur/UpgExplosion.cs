using desktop.gameobjects;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.joueur;

public class UpgExplosion : AbstractUpgJoueur
{
    private int Niveau = 0;
    public UpgExplosion(Texture2D image) : base(image, "Genere une explosion lorsque le joueur se fait frapper", 3)
    {
    }

    public override void Appliquer(Joueur joueur)
    {
        Niveau += 1;
        switch(Niveau){
            case 1: _description = "Augmente le rayon de l'explosion"; 
            break;
            case 2: _description = "Augmente le rayon de l'explosion";
            break;
            case 3: _description = "Augmente le recul infligé par l'explosion";
            break;
        }
        joueur._generateurExplosion.Ameliorer();
    }
}