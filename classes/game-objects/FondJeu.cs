using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.gameobjects;

public class FondJeu : IGameObject
{


    //Increment du fond
    private Vector2 increment = new Vector2(10,10);
    //Vitesse du mouvement de la grille en px/s
    private float vitesse = 10;
    //Largeur de la grille en pixels
    private float largeur = 100;
    //Position actuele de la grille
    public Vector2 _position {get;set;} = new Vector2(0,0);
    
    protected List<Forme3D> Formes = new List<Forme3D>();

    public FondJeu(){
        Formes.Add(new Cube3D(new Vector3(),100));
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(Forme3D forme in Formes){
            forme.Draw(spriteBatch);
        }
        //Dessine des lignes verticales
        for(float act = increment.X;act < GetLargeurEcran(spriteBatch);act += largeur ){
            spriteBatch.DrawLine(act,0, act, GetHauteurEcran(spriteBatch), Color.White);
        }
        //Dessine des lignes horizontales
        for(float act = increment.Y ;act < GetLargeurEcran(spriteBatch) ;act += largeur ){
            spriteBatch.DrawLine(0,act - _position.Y,GetLargeurEcran(spriteBatch),act- _position.Y,Color.White);
        }
        
    }

    public void Update(float deltaT)
    {
        foreach(Forme3D forme in Formes.Reverse<Forme3D>()){
            forme.Update(deltaT);
        }
    }
    public void Update(Vector2 positon,float deltaT){
        increment.X = -positon.X % largeur;
        increment.Y = -positon.Y % largeur;

        Update(deltaT);
    }

    public float GetLargeurEcran(SpriteBatch spriteBatch){
        return spriteBatch.GraphicsDevice.Viewport.Width;
    }
    public float GetHauteurEcran(SpriteBatch spriteBatch){
        return spriteBatch.GraphicsDevice.Viewport.Height;
    }
}
