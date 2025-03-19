using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.utils;

/// <summary>
/// Classe pour génerer des polygones réguliers
/// </summary>
//Idéalement, les polygones généré avec ceci devraient être mis dans une cache pour éviter de devoir les recalculer à chaque fois
public static class PolyGen
{
    /// <summary>
    /// Genere un polygone regulier vide qui peut etre affiche
    /// </summary>
    /// <param name="nbCote">nombre de cotes du poligone</param>
    /// <param name="taille">rayon du polygone</param>
    /// <param name="z">index z dans le VertexPositionColor</param>
    /// <param name="couleur">couleur du polygone</param>
    /// <returns>Retourne un tableau de VertexPositionColor representant chaque cote de la forme</returns>
    public static VertexPositionColor[] GetPolyVide(int nbCote, float taille, int z, Color couleur)
    {
        Vector2[] cotes = GetPoly(nbCote, taille);
        VertexPositionColor[] res = new VertexPositionColor[cotes.Length];
        for (int i = 0; i < cotes.Length; i++)
        {
            res[i] = new VertexPositionColor(new Vector3(cotes[i].X, cotes[i].Y, z), couleur);
        }
        return res;
    }

    /// <summary>
    /// Genere les points d'un polygone régulier
    /// </summary>
    /// <param name="nbCote">nombres de coté du polygone</param>
    /// <param name="rayon">rayon du polygone</param>
    /// <returns>un tableau de vecteur 2d représentant les points d'un polygone régulier</returns>
    public static Vector2[] GetPoly(int nbCote, float rayon)
    {
        Vector2[] points = new Vector2[nbCote];

        for (int i = 0; i < nbCote; i++)
        {
            double j = i * 2 * Math.PI / nbCote;
            Vector2 point = new Vector2((float)Math.Cos(j), (float)Math.Sin(j)) * rayon;
            points[i] = point;
        }

        return points;
    }
}
