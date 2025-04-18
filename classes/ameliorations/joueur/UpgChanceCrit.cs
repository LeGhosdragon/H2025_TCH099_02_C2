using desktop.gameobjects;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations.joueur;

public class UpgChanceCrit : AbstractUpgJoueur
{
    public UpgChanceCrit(Texture2D image) : base(image, "Augmente la chance de causer des degats critiques", 7)
    {
    }

    public override void Appliquer(Joueur joueur)
    {
        switch(_limite){
            case 7: 
                joueur._arme._chanceCritique += 5;
            break;
            case 6:
            case 5:
            case 4:
                joueur._arme._chanceCritique += 10;
            break;
            case 3:
            case 2:
            case 1:
                joueur._arme._chanceCritique += 20;
            break;
        }
    }
}