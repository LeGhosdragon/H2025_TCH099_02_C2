using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using MonoGame.Extended;

namespace desktop.gameobjects;

public class BarreExp : IGameObject
{
    Joueur _joueur;
    public BarreExp(Joueur joueur){
        _joueur =joueur;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        float largeur = 5;
        float hauteur = (float) _joueur._experience/(float) _joueur.getExpReq() * (float) spriteBatch.GraphicsDevice.Viewport.Height;
        Vector2 size = new Vector2(largeur,hauteur);


        Vector2 pos = new Vector2(largeur/2,spriteBatch.GraphicsDevice.Viewport.Height - hauteur);
        //Dessine a gauche
        spriteBatch.DrawRectangle(pos,size,Color.DarkGreen,largeur);


        //Dessine a droite
        pos = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width - largeur,spriteBatch.GraphicsDevice.Viewport.Height - hauteur);
        spriteBatch.DrawRectangle(pos,size,Color.Green,largeur);

    }

    public void Update(float deltaT)
    {

    }
}