using System;
using System.Collections.Generic;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace desktop.gameobjects;

public class Forme3D : IGameObject
{
    protected Vector3 _angle {get;set;}
    protected Vector3 _vitesse {get;set;}
    public Vector3 _position {get;set;}
    protected List<Vector2> _cotes {get;set;}
    protected List<Vector3> _coins {get;set;}
    protected const float _distMax = 1000;
    public float _taille {get;set;}
    public Forme3D(Vector3 position,float taille,List<Vector2> cotes, List<Vector3> coins){
        _angle = new Vector3(
           (float)( Random.Shared.NextDouble() * Math.PI * 2),
           (float)( Random.Shared.NextDouble() * Math.PI * 2),
           (float)( Random.Shared.NextDouble() * Math.PI * 2)
        );
        _vitesse = new Vector3(
            (float)((Random.Shared.NextDouble() - 0.2) * 0.002f),
            (float)((Random.Shared.NextDouble() - 0.2) * 0.002f),
            (float)((Random.Shared.NextDouble() - 0.2) * 0.002f)
        );
        _position = position;
        _coins = coins;
        _cotes = cotes;
        _taille = taille;
        


    }
    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 tailleEcran = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width,spriteBatch.GraphicsDevice.Viewport.Height);
        List<Vector2> projete = new List<Vector2>();
        foreach (Vector3 v in _coins) {
            Vector3 tourne = Tourner(v);
            Vector2 projeteP = this.Projeter(tourne,tailleEcran);
            projete.Add(projeteP);
        }
        foreach(Vector2 cote in _cotes){
            Vector2 pos1 = new Vector2(projete[(int) cote.X].X + _position.X,projete[(int) cote.X].Y + _position.Y);
            Vector2 pos2 = new Vector2(projete[(int) cote.Y].X + _position.X,projete[(int) cote.Y].Y + _position.Y);
            spriteBatch.DrawLine(pos1 - Camera.getInstance().getPosition(),pos2 - Camera.getInstance().getPosition(),Color.White);

        }
    }
    public Vector3 Tourner(Vector3 avant){
        float y = avant.Y;
        float x = avant.X;
        float z = avant.Z;
        

        // Rotation autour de l'axe X
        float y1 = (float)(y * Math.Cos(_angle.X) - z * Math.Sin(_angle.X));
        float z1 = (float)(y * Math.Sin(_angle.X) + z * Math.Cos(_angle.X));

        // Rotation autour de l'axe Y
        float x2 = (float)(x * Math.Cos(_angle.Y) + z1 * Math.Sin(_angle.Y));
        float z2 = (float)(-x * Math.Sin(_angle.Y) + z1 * Math.Cos(_angle.Y));

        // Rotation autour de l'axe Z
        float x3 = (float)(x2 * Math.Cos(_angle.Z) - y1 * Math.Sin(_angle.Z));
        float y3 = (float)(x2 * Math.Sin(_angle.Z) + y1 * Math.Cos(_angle.Z));

        return new Vector3(x3,y3,z2);
    }
    public Vector2 Projeter(Vector3 avant,Vector2 tailleEcran){
        float distance = 400;
        float scale = distance / Math.Max(distance + avant.Z, 1);
        
        float x = avant.X * scale + tailleEcran.X / 2;
        float y = avant.Y * scale + tailleEcran.Y / 2;
        return new Vector2( x, y);
    }

    public void Update(float deltaT)
    {
        _angle += _vitesse * deltaT * 100;


    }
    public bool doitEnlever(){
        float marges = 1f;
        float largeur = Camera.getInstance().getWidth();
        float hauteur = Camera.getInstance().getHeight();
        //Depasse a droite
        if(_position.X >  Camera.getInstance().getPosition().X +largeur + largeur * marges){
         return true;
        }
        //Depasse a gauche
        if(_position.X < Camera.getInstance().getPosition().X - largeur* marges){
            return true;
        }
        //Depasse en haut
        if (_position.Y > Camera.getInstance().getPosition().Y +hauteur + hauteur* marges){
            return true;
        }
        //Deppase en bas
        if(_position.Y < Camera.getInstance().getPosition().Y -hauteur * marges){
            return true;
        }
        return false;
    }

}