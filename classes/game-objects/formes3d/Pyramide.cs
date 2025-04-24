using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace desktop.gameobjects;

public class Pyramide : Forme3D
{
    public Pyramide(Vector3 position,float taille) : base(position,taille, genererCotes(), genererCoins(taille))
    {
    }
    protected static List<Vector2> genererCotes(){
        return new List<Vector2>{            
            new Vector2(0, 1), 
            new Vector2(0, 2), 
            new Vector2(0, 3), 
            new Vector2(0, 4),
            new Vector2(1, 2), 
            new Vector2(2, 3), 
            new Vector2(3, 4), 
            new Vector2(4, 1)
        };
    }
    protected static List<Vector3> genererCoins(float taille){
        taille = taille/2;
        return new List<Vector3>{
            new Vector3(0,taille,0),
            new Vector3(-taille,-taille,-taille),
            new Vector3(taille,-taille,-taille),
            new Vector3(taille,-taille,taille),
            new Vector3(-taille,-taille,taille)
        };
        
    }
    
}