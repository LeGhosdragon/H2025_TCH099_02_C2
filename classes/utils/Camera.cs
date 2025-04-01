using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.utils;

public class Camera
{
    private static Camera instance = getInstance();
    protected Vector2 _position;
    protected GraphicsDevice _graphics;

    /// <summary>
    /// Permet d'obtenir l'instance unique de la camera, la cree si elle n'existe pas
    /// </summary>
    /// <returns>retourne l'instance de la camera</returns>
    public static Camera getInstance()
    {
        return instance;
    }

    public Camera(GraphicsDevice device, Vector2 position)
    {
        this._graphics = device;
        this._position = position;
        instance = this;
    }

    public static void setPosition(Vector2 position)
    {
        instance._position = new Vector2(
            position.X - getInstance()._graphics.Viewport.Width / 2,
            position.Y - getInstance()._graphics.Viewport.Height / 2
        );
    }

    public Vector2 getPosition()
    {
        return _position;
    }

    /// <summary>
    /// Convertis la position d'un objet pour selon sa position en pixel sur l'ecran
    /// </summary>
    /// <param name="posObj">position de l'objet a converir</param>
    /// <returns>position de lobjet en pixel selon lecran</returns>
    public Vector2 objetPosEnPX(Vector2 posObj)
    {
        return new Vector2(
            posObj.X + _graphics.Viewport.Width / 2 - _position.X,
            posObj.Y + _graphics.Viewport.Height / 2 - _position.Y
        );
    }

    /// <summary>
    /// Donne la position de la souris par rapport au coin haut gauche de la camera
    /// </summary>
    /// <returns>La position de la souris selon la position de la camera</returns>
    public Vector2 getPosSourisCamera()
    {
        return Controle.getPosSouris() - objetPosEnPX(getPosition());
    }
}
