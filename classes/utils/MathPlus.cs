using System;
using Microsoft.Xna.Framework;

namespace desktop.utils;

/// <summary>
/// Classe utilitaire pour des fonctions mathematique additionnelles
/// </summary>
public static class MathPlus
{
    /// <summary>
    /// Soustrais un vecteur 2d et un vecteur 3d, le Z reste le meme que celui du vecteur en 3d
    /// V1 - V2
    /// </summary>
    /// <param name="v1">vecteur 2d</param>
    /// <param name="v2">vecteur 3d qui est enleve au premier vecteur</param>
    /// <returns>Retourne un vecteur 3 qui correspond a la soustraction des 2 vecteurs</returns>
    public static Vector3 MoinsV3(Vector2 v1, Vector3 v2)
    {
        return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v2.Z);
    }

    /// <summary>
    /// Soustrais un vecteur 2d et un vecteur 3d, le Z reste le meme que celui du vecteur en 3d
    /// V1 - V2
    /// </summary>
    /// <param name="v1">vecteur 3d</param>
    /// <param name="v2">vecteur 2d qui est enleve au premier vecteur</param>
    /// <returns>Retourne un vecteur 3 qui correspond a la soustraction des 2 vecteurs</returns>
    public static Vector3 MoinsV3(Vector3 v1, Vector2 v2)
    {
        return new Vector3(-v1.X + v2.X, -v1.Y + v2.Y, v1.Z);
    }

    /// <summary>
    /// Transforme un vecteur 3d en vecteur 2d en enlevant le z
    /// </summary>
    /// <param name="vector">vecteur a convertir</param>
    /// <returns>vecteur 2d correspondant au vecteur passe en parametre</returns>
    public static Vector2 EnVector2(Vector3 vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    /// <summary>
    /// Transforme un vecteur 2d en vecteur 3d en ajoutant un Z
    /// </summary>
    /// <param name="vector">Vecteur a transformer</param>
    /// <param name="z">valeur du z</param>
    /// <returns>vecteur 3d correspondant au vecteur en parametre</returns>
    public static Vector3 EnVector3(Vector2 vector, float z)
    {
        return new Vector3(vector.X, vector.Y, z);
    }

    public static float AngleEntre(Vector2 v1, Vector2 v2)
    {
        return (float)Math.Atan2(v1.X * v2.Y - v2.X * v1.Y, v1.Y * v2.X - v2.Y * v1.X);
    }
}
