using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace desktop.utils;

/// <summary>
/// Permet de savoir si une touche a deja ete appuye et lachee
/// </summary>
public class Touche
{
    private Keys _key { get; }
    private ControlesEnum? _type { get; }

    public Touche(Keys key)
    {
        _key = key;
    }
    public Touche(ControlesEnum type)
    {
        _type = type;
    }
    /// <summary>
    /// Verifie si la touche a ete lachee
    /// </summary>
    /// <returns>true si la touche est encore appuye</returns>
    public bool Verifie()
    {
        if (_type == null)
        {
            return Keyboard.GetState().IsKeyDown(_key);
        }
        return Controle.enfonceClavier(_type ?? ControlesEnum.BOUGER_HAUT);
    }
    /// <summary>
    /// Enleve les touches qui ne sont plus appuyees
    /// </summary>
    /// <param name="touches">touches qui doivent etre verifie</param>
    /// <returns>liste de touches qui sont encore appuyes</returns>
    public static List<Touche> enleverTouches(List<Touche> touches)
    {
        touches.RemoveAll(touche =>
        {
            return !touche.Verifie();
        });
        return touches;
    }
    /// <summary>
    /// Determine si la touche est contenue dans la liste
    /// </summary>
    /// <param name="touches">listes de touches appuyees</param>
    /// <param name="controle">controle qui doit etre verifie</param>
    /// <returns>true si la touche nest pas dans la liste</returns>
    public static bool ValiderTouche(List<Touche> touches, ControlesEnum controle)
    {
        return touches.Find(touche =>
        {
            return touche._type == controle;
        }) == null;
    }
    /// <summary>
    /// Determine si la touche est contenue dans la liste
    /// </summary>
    /// <param name="touches">listes de touches appuyees</param>
    /// <param name="key">clee qui doit etre verifie</param>
    /// <returns>true si la touche nest pas dans la liste</returns>
    public static bool ValiderTouche(List<Touche> touches, Keys key)
    {
        return touches.Find(touche =>
        {
            return touche._key == key;
        }) == null;
    }
}