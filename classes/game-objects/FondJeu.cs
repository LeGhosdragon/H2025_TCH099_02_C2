using System;
using System.Collections.Generic;
using System.Linq;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace desktop.gameobjects;

public class FondJeu : IGameObject
{


    //Increment du fond
    private Vector2 increment = new Vector2(10, 10);

    //Largeur de la grille en pixels
    private float largeur = 100;
    //Position actuele de la grille
    public Vector2 _position { get; set; } = new Vector2(0, 0);
    EcranJeu _ecranJeu;
    protected List<Forme3D> _formes = new List<Forme3D>();

    public FondJeu(EcranJeu ecranJeu)
    {
        _ecranJeu = ecranJeu;
/*
        _formes.Add(new Cube3D(new Vector3(), 100));
        _formes.Add(new Octaedre(new Vector3(100, 100, 100), 200));
        _formes.Add(new Pyramide(new Vector3(-100, -100, -100), 100));
        _formes.Add(new Tetraedre(new Vector3(-200, -200, -200), 400));
*/
        GenererFormes(10);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Forme3D forme in _formes.Reverse<Forme3D>())
        {
            forme.Draw(spriteBatch);
        }
        //Dessine des lignes verticales
        for (float act = increment.X; act < GetLargeurEcran(spriteBatch); act += largeur)
        {
            spriteBatch.DrawLine(act, 0, act, GetHauteurEcran(spriteBatch), Color.White);
        }
        //Dessine des lignes horizontales
        for (float act = increment.Y; act < GetLargeurEcran(spriteBatch); act += largeur)
        {
            spriteBatch.DrawLine(0, act - _position.Y, GetLargeurEcran(spriteBatch), act - _position.Y, Color.White);
        }

    }
    public void GenererFormes(int quantite)
    {
        for (int i = 0; i < quantite; i++)
        {
            GenererForme();
        }
    }
    public void GenererForme()
    {
        float size = (float)Random.Shared.NextDouble() * 250 + 50;
        const float minDistance = 100;

        int shapeType = Random.Shared.Next(4);
        Vector2 bornes = new Vector2(_ecranJeu.GraphicsDevice.Viewport.Width, _ecranJeu.GraphicsDevice.Viewport.Height);


        Vector3 pos = new Vector3();
        int attempts = 0;

        int maxAttempts = 100;

        bool validPosition = false;

        while (!validPosition && (attempts < maxAttempts))
        {
            pos.X = Random.Shared.Next((int)bornes.X) - _ecranJeu._joueur.getPosition().X;
            pos.Y = Random.Shared.Next((int)bornes.Y) - _ecranJeu._joueur.getPosition().Y;
            pos.Z = Random.Shared.Next(100);

            // Vérifie si la nouvelle position est trop proche d'une forme existante
            if (!proximiteForme(pos,size,minDistance)) {
                validPosition = true;
            } else
            {
                // Déplace la forme plus loin avec un facteur aléatoire si la position est invalide
                float moveFactor = 1 + Random.Shared.Next(3) + 1; // Facteur de déplacement entre 1x et 3x
                pos.X += (Random.Shared.NextDouble() > 0.5 ? 1 : -1) *moveFactor * minDistance;
                pos.Y += (Random.Shared.NextDouble() > 0.5 ? 1 : -1) * moveFactor * minDistance;
                attempts++;
            }



        }
        if (validPosition)
        {
            Console.WriteLine(pos.ToString());
            Console.WriteLine(size);
            switch (shapeType)
            {
                case 0:
                   _formes.Add( new Cube3D(pos, size));
                    break;
                case 1:
                   _formes.Add( new Pyramide(pos, size));
                    break;
                case 2:
                   _formes.Add( new Tetraedre(pos, size));
                    break;
                case 3:
                   _formes.Add( new Octaedre(pos, size));
                    break;
            }
        }



    }
    public bool proximiteForme(Vector3 pos, float taille,float minDistance)
    {
        Vector2 vector2D = new Vector2(pos.X, pos.Y);
        foreach (Forme3D forme in _formes)
        {
            Vector2 vector2DAutre = new Vector2(forme._position.X, forme._position.Y);

            if (Vector2.Distance(vector2D, vector2DAutre) < taille + forme._taille + minDistance)
            {
                return true;
            }

        }
        return false;
    }

    public void EnleverForme3D(Forme3D forme)
    {
        _formes.Remove(forme);
        GenererForme();
    }

    public void Update(float deltaT)
    {
        foreach (Forme3D forme in _formes.Reverse<Forme3D>())
        {
            forme.Update(deltaT);
            if (forme.doitEnlever())
            {
                EnleverForme3D(forme);
            }
        }
    }
    public void Update(Vector2 positon, float deltaT)
    {
        increment.X = -positon.X % largeur;
        increment.Y = -positon.Y % largeur;

        Update(deltaT);
    }

    public float GetLargeurEcran(SpriteBatch spriteBatch)
    {
        return spriteBatch.GraphicsDevice.Viewport.Width;
    }
    public float GetHauteurEcran(SpriteBatch spriteBatch)
    {
        return spriteBatch.GraphicsDevice.Viewport.Height;
    }
}
