using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace desktop.utils;

/// <summary>
/// Les touches de la souris ne sont pas géré par cette classe
/// </summary>
public class Controle
{
    protected List<Keys> _clees;
    protected List<GamePadButtons> _boutons;

    /// <summary>
    /// Listes de boutons associé au controle sur une manette
    /// </summary>
    /// <returns>retourne la liste de boutons associé au controle</returns>
    public List<GamePadButtons> getBoutons()
    {
        return _boutons;
    }

    /// <summary>
    /// Listes de boutons associé au controle sur un clavier
    /// </summary>
    /// <returns>retourne la liste de boutons associé au controle</returns>
    public List<Keys> getClees()
    {
        return _clees;
    }

    public Controle(List<Keys> clees, List<GamePadButtons> boutons)
    {
        this._clees = clees;
        this._boutons = boutons;
    }

    public static List<Controle> _controles = genererControles();

    /// <summary>
    /// Genere la liste de tout les controles controle
    /// </summary>
    /// <returns>Une liste de controles ordonées selon l'enum</returns>
    private static List<Controle> genererControles()
    {
        List<Controle> controles = new List<Controle>();

        List<Keys> clees = new List<Keys> { Keys.W, Keys.Up };
        List<GamePadButtons> boutons = new List<GamePadButtons>();
        Controle bougerHaut = new Controle(clees, boutons);
        controles.Insert((int)ControlesEnum.BOUGER_HAUT, bougerHaut);

        clees = new List<Keys> { Keys.S, Keys.Down };
        boutons = new List<GamePadButtons>();
        Controle bougerBas = new Controle(clees, boutons);
        controles.Insert((int)ControlesEnum.BOUGER_BAS, bougerBas);

        clees = new List<Keys> { Keys.A, Keys.Left };
        boutons = new List<GamePadButtons>();
        Controle bougerGauche = new Controle(clees, boutons);
        controles.Insert((int)ControlesEnum.BOUGER_GAUCHE, bougerGauche);

        clees = new List<Keys> { Keys.D, Keys.Right };
        boutons = new List<GamePadButtons>();
        Controle bougerDroite = new Controle(clees, boutons);
        controles.Insert((int)ControlesEnum.BOUGER_DROIT, bougerDroite);

        clees = new List<Keys> { Keys.Q };
        boutons = new List<GamePadButtons>();
        Controle AttaquerAutomatique = new Controle(clees, boutons);
        controles.Insert((int)ControlesEnum.ATTAQUE_AUTOMATIQUE, AttaquerAutomatique);

        clees = new List<Keys> { Keys.Escape };
        boutons = new List<GamePadButtons>();
        Controle Pause = new Controle(clees, boutons);
        controles.Insert((int)ControlesEnum.PAUSE, Pause);
        
            clees = new List<Keys> { Keys.T };
        boutons = new List<GamePadButtons>();
        Controle AfficherHpText = new Controle(clees, boutons);
        controles.Insert((int)ControlesEnum.TEXT, AfficherHpText);

        return controles;
    }

    /// <summary>
    /// Verifie les touches de clavier d'un controle sont enfoncé
    /// </summary>
    /// <param name="controle">Controle qui doit être vérifié</param>
    /// <returns>true si une des touche est enfoncée</returns>
    public static bool enfonceClavier(ControlesEnum controle)
    {
        Controle controleObj = _controles[(int)controle];
        foreach (Keys clee in controleObj.getClees())
        {
            if (Keyboard.GetState().IsKeyDown(clee))
                return true;
        }

        return false;
    }
    

    /// <summary>
    /// Permet d'obtenir un vecteur qui correspond a la position de la souris
    /// </summary>
    /// <returns>Un vector2 de la position de la souris</returns>
    public static Vector2 getPosSouris()
    {
        return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
    }
}

/// <summary>
/// Liste de chaque controle dans l'ordre ou il sont placé dans la liste de controle
/// </summary>
public enum ControlesEnum
{
    BOUGER_HAUT = 0, // W ou Fleche haut
    BOUGER_BAS = 1, // S ou Fleche bas
    BOUGER_GAUCHE = 2, // A ou Fleche Gauche
    BOUGER_DROIT = 3, // D ou Fleche droite
    ATTAQUE_AUTOMATIQUE = 4, // Q
    PAUSE = 5, // Echap
    TEXT = 6 // Touche pour afficher la vie des Monstres
}
