using System;
using System.Collections.Generic;
using desktop.gameobjects;
using desktop.pages;
using Microsoft.Xna.Framework;

namespace desktop.utils;

public class GenerateurMonstre
{
    /// <summary>
    /// Genere un monstre aleatoire
    /// </summary>
    /// <param name="ecranJeu">Ecran de jeu</param>
    /// <returns>Un monstre aleatoire</returns>
    public static Vector2 GenererPositionMonstreBordures(int rayonMonstre, EcranJeu ecranJeu)
    {
        int largeur = ecranJeu.GraphicsDevice.Viewport.Width;
        int hauteur = ecranJeu.GraphicsDevice.Viewport.Height;
        
        Random rand = new Random(); // Create a Random object to reuse
        float cote = (float)rand.NextDouble();  // Random float between 0 and 1
        
        float rngX = (float)rand.NextDouble() * largeur;
        float rngY = (float)rand.NextDouble() * hauteur;
        
        int bordure = rayonMonstre * 2; // Ensure the monster is placed away from the borders

        if (cote < 0.2) {
            // Top side
            rngX = (float)rand.NextDouble() * largeur;
            rngY = -bordure - (float)rand.NextDouble() * bordure;
        } else if (cote < 0.4) {
            // Bottom side
            rngX = (float)rand.NextDouble() * largeur;
            rngY = hauteur + bordure + (float)rand.NextDouble() * bordure;
        } else if (cote < 0.6) {
            // Left side
            rngX = -bordure - (float)rand.NextDouble() * bordure;
            rngY = (float)rand.NextDouble() * hauteur;
        } else if (cote < 0.8) {
            // Right side
            rngX = largeur + bordure + (float)rand.NextDouble() * bordure;
            rngY = (float)rand.NextDouble() * hauteur;
        } else {
            // Corners (evenly distributed)
            int corner = rand.Next(0, 4); // Randomly choose one of the 4 corners
            switch (corner) {
                case 0: // Top-left
                    rngX = -bordure - (float)rand.NextDouble() * bordure;
                    rngY = -bordure - (float)rand.NextDouble() * bordure;
                    break;
                case 1: // Top-right
                    rngX = largeur + bordure + (float)rand.NextDouble() * bordure;
                    rngY = -bordure - (float)rand.NextDouble() * bordure;
                    break;
                case 2: // Bottom-left
                    rngX = -bordure - (float)rand.NextDouble() * bordure;
                    rngY = hauteur + bordure + (float)rand.NextDouble() * bordure;
                    break;
                case 3: // Bottom-right
                    rngX = largeur + bordure + (float)rand.NextDouble() * bordure;
                    rngY = hauteur + bordure + (float)rand.NextDouble() * bordure;
                    break;
            }
        }

        Console.WriteLine($"Position monstre: {rngX + Camera.getInstance().getPosition().X}, {rngY + Camera.getInstance().getPosition().Y}");
        return new Vector2(rngX + Camera.getInstance().getPosition().X, rngY + Camera.getInstance().getPosition().Y);
    }
    /// <summary>
    /// Cree un nouveau monstre et l'ajoute dans le jeu
    /// </summary>
    public static void GenererMonstre(string type, EcranJeu ecranJeu, List<IGameObject> _objets, float ennemiDifficultee, bool fromBoss = false, Vector2 pos = default(Vector2))
    {
        Monstre monstre = null;
        if (pos == default(Vector2))
        {
            pos = GenererPositionMonstreBordures(2, ecranJeu);
        }
        switch(type){
            case "normal":
                monstre = new MonstreNormal( 4, ecranJeu, ennemiDifficultee, fromBoss, pos);
                break;
            case "runner":
                monstre = new MonstreRunner( 3, ecranJeu, ennemiDifficultee);
                break;
            case "tank":
                monstre = new MonstreTank( 6, ecranJeu, ennemiDifficultee);
                break;
            case "gunner":
                monstre = new MonstreGunner( 2, ecranJeu, ennemiDifficultee);
                break;
            case "bossGunner":
                monstre = new BossGunner( 2, ecranJeu, ennemiDifficultee);
                break;
            case "bossNormal":
                monstre = new BossNormal( 3, ecranJeu, ennemiDifficultee);
                break;
            default:

                break;
        }
        
        
        _objets.Add(monstre);
    }
}

