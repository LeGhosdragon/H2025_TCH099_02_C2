using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.gameobjects;

public class Fond : IGameObject
{


    //Increment du fond
    private float increment = 10;
    //Vitesse du mouvement de la grille en px/s
    private float vitesse = 10;
    //Largeur de la grille en pixels
    private float largeur = 100;

    public void Draw(SpriteBatch spriteBatch)
    {
                //Dessine des lignes verticales
        for(float act = increment;act < GetLargeurEcran(spriteBatch);act += largeur ){
            spriteBatch.DrawLine(act,0,act,GetHauteurEcran(spriteBatch),Color.White);
        }
        //Dessine des lignes horizontales
        for(float act = increment;act < GetLargeurEcran(spriteBatch);act += largeur ){
            spriteBatch.DrawLine(0,act,GetLargeurEcran(spriteBatch),act,Color.White);
        }
    }

    public void Update(float deltaT)
    {
        increment = (increment + vitesse * (float) deltaT) % largeur;
    }

    public float GetLargeurEcran(SpriteBatch spriteBatch){
        return spriteBatch.GraphicsDevice.Viewport.Width;
    }
    public float GetHauteurEcran(SpriteBatch spriteBatch){
        return spriteBatch.GraphicsDevice.Viewport.Height;
    }
}