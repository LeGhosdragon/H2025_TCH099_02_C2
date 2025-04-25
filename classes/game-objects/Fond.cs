using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.gameobjects;

public class Fond : IGameObject
{


    //Increment du fond
    private Vector2 increment = new Vector2(10,10);
    //Vitesse du mouvement de la grille en px/s
    private float vitesse = 10;
    //Largeur de la grille en pixels
    private float largeur = 100;
    public Vector2 _position {get;set;} = new Vector2(0,0);

    public void Draw(SpriteBatch spriteBatch)
    {
        //Dessine des lignes verticales
        for(float act = increment.X;act < GetLargeurEcran(spriteBatch);act += largeur ){
            spriteBatch.DrawLine(act,0, act, GetHauteurEcran(spriteBatch), Color.White);
        }
        //Dessine des lignes horizontales
        for(float act = increment.Y ;act < GetHauteurEcran(spriteBatch) ;act += largeur ){
            spriteBatch.DrawLine(0,act - _position.Y,GetLargeurEcran(spriteBatch),act- _position.Y,Color.White);
        }
    }

    public void Update(float deltaT)
    {
        increment.X = (increment.X + vitesse * (float) deltaT) % largeur;
        increment.Y = (increment.X + vitesse * (float) deltaT) % largeur;

    }
    public void Update(Vector2 positon){
        increment.X = -positon.X % largeur;
        increment.Y = -positon.Y % largeur;
    }

    public float GetLargeurEcran(SpriteBatch spriteBatch){
        return spriteBatch.GraphicsDevice.Viewport.Width;
    }
    public float GetHauteurEcran(SpriteBatch spriteBatch){
        return spriteBatch.GraphicsDevice.Viewport.Height;
    }
}