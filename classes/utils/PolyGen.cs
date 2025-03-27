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
    /// <returns>tableau d'arretes utilise avec les basicEffects</returns>
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
    /// <param name="matrice">Tableau de Matrices qui doivent etre tournées</param>
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
    ///Tourne un vecteur selon les Z
    /// </summary>
    /// <param name="vecteur">Tableau de vecteur qui doivent etre tournées</param>
    /// <param name="angleRad">Angle dont la matrice doit être tournée</param>
    /// <returns>Retourne le tableau avec tout les points tourné</returns>
    public static Vector2 tournerMatrice(Vector2 vecteur, float angleRad)
    {
        return tournerMatrice(new Vector2[] { vecteur }, angleRad)[0];
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

    /// <summary>
    /// Permet d'obtenir l'angle entre 2 vecteurs
    /// </summary>
    /// <param name="v1">premier vecteur</param>
    /// <param name="v2">deuxieme vecteur</param>
    /// <returns>Angle entre les vecteur (de PI/2 a -PI/2)</returns>
    public static float AngleEntre(Vector2 v1, Vector2 v2)
    {
        return (float)Math.Atan2(v2.Y - v1.Y, v2.X - v1.X);
    }

    /// <summary>
    /// Obtiens l'angle du vecteur par rapport a un cercle trigonometrique
    /// </summary>
    /// <param name="v">vecteur dont on veut savoir langle</param>
    /// <returns>angle en radians</returns>
    public static float angleVecteur(Vector2 v)
    {
        Vector2 normal = Vector2.Normalize(v);
        return (float)Math.Atan2(normal.Y, normal.X);
    }
}
