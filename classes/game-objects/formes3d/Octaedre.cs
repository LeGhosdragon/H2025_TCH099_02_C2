using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace desktop.gameobjects;

public class Octaedre : Forme3D
{
    public Octaedre(Vector3 position,float taille) : base(position,taille, genererCotes(), genererCoins(taille))
    {
    }
    protected static List<Vector2> genererCotes(){
        return new List<Vector2>{            
            new Vector2(0, 2), 
            new Vector2(0, 3), 
            new Vector2(0, 4), 
            new Vector2(0, 5),
            new Vector2(1, 2), 
            new Vector2(1, 3), 
            new Vector2(1, 4), 
            new Vector2(1, 5),
            new Vector2(2, 4), 
            new Vector2(2, 5), 
            new Vector2(3, 4), 
            new Vector2(3, 5)
        };
    }
    protected static List<Vector3> genererCoins(float taille){
        taille = taille/2;
        return new List<Vector3>{
            new Vector3(0,taille,0),
            new Vector3(0,-taille,0),
            new Vector3(taille,0,0),
            new Vector3(-taille,0,0),
            new Vector3(0,0,taille),
            new Vector3(0,0,-taille)
        };
        
    }
    
}