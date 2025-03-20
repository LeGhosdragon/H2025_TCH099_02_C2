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
    /// Génere une forme qui peut être affiché
    /// </summary>
    /// <param name="forme">forme a afficher</param>
    /// <param name="position">position de la forme sur l'écran</param>
    /// <param name="couleur">couleur des cotées de la forme</param>
    /// <returns></returns>
    public static VertexPositionColor[] GenererFormeVide(
        Vector2[] forme,
        Vector3 position,
        Color couleur
    )
    {
        VertexPositionColor[] res = new VertexPositionColor[forme.Length];
        for (int i = 0; i < forme.Length; i++)
        {
            res[i] = new VertexPositionColor(
                new Vector3(forme[i].X + position.X, forme[i].Y + position.Y, position.Z),
                couleur
            );
        }
        return res;
    }

    /// <summary>
    ///Tourne une matrice selon les Z
    /// </summary>
    /// <param name="matrice">Tableau de Matrices qui doivent etre torunées</param>
    /// <param name="angleRad">Angle dont la matrice doit être tournée</param>
    /// <returns>Retourne le tableau avec tout les points tourné</returns>
    public static Vector2[] tournerMatrice(Vector2[] matrice, float angleRad)
    {
        Vector2[] res = new Vector2[matrice.Length];
        for (int i = 0; i < matrice.Length; i++)
        {
            res[i] = Vector2.Transform(matrice[i], Matrix.CreateRotationZ(angleRad));
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
        Vector2[] points = new Vector2[nbCote + 1];

        for (int i = 0; i < points.Length; i++)
        {
            double j = i * 2 * Math.PI / nbCote;
            Vector2 point = new Vector2((float)Math.Cos(j), (float)Math.Sin(j)) * rayon;
            points[i] = point;
        }

        return points;
    }
}
