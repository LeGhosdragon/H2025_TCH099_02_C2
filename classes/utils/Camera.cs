using System;
using System.Collections.Generic;
using desktop.gameobjects;
using desktop.pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.utils;

/// <summary>
/// Singleton qui affiche tout les objets sur l'ecran
/// </summary>
public class Camera
{
    private static Camera instance = getInstance();
    protected Vector3 _position;

    private Camera()
    {
        _position = Vector3.Zero;
    }

    /// <summary>
    /// Permet d'obtenir l'instance unique de la camera, la cree si elle n'existe pas
    /// </summary>
    /// <returns>retourne l'instance de la camera</returns>
    public static Camera getInstance()
    {
        return instance == null ? new Camera() : instance;
    }

    public static void setPosition(Vector3 position)
    {
        instance._position = position;
    }

    public void Draw(GraphicsDevice device, List<IGameObject> objets)
    {
        resEffet(device);
        foreach (EffectPass pass in _passes)
        {
            pass.Apply();
        }
    }

    /// <summary>
    ///
    /// retourne le vecteur de la position
    /// </summary>
    /// <returns></returns>
    public Vector3 getPosition3D()
    {
        return _position;
    }

    /// <summary>
    /// Retourne un le vecteur de la position en format 2D
    /// </summary>
    /// <returns>Le vecteur position en 2D</returns>
    public Vector2 getPosition2D()
    {
        return MathPlus.EnVector2(_position);
    }

    /// <summary>
    /// Convertis la position d'un objet pour selon sa position en pixel sur l'ecran
    /// </summary>
    /// <param name="posObj">position de l'objet a converir</param>
    /// <returns>position de lobjet en pixel selon lecran</returns>
    public Vector2 objetPosEnPX(Vector2 posObj)
    {
        return new Vector2(
            posObj.X + _effet.GraphicsDevice.Viewport.Width / 2 - _position.X,
            posObj.Y + _effet.GraphicsDevice.Viewport.Height / 2 - _position.Y
        );
    }

    /// <summary>
    /// Donne la position de la souris par rapport au coin haut gauche de la camera
    /// </summary>
    /// <returns>La position de la souris selon la position de la camera</returns>
    public Vector2 getPosSourisCamera()
    {
        Console.WriteLine(objetPosEnPX(getPosition2D()) + " souris" + Controle.getPosSouris());

        return Controle.getPosSouris() - objetPosEnPX(getPosition2D());
    }

    /*
SECTION GRAPHIQUE
*/
    protected BasicEffect _effet;
    protected EffectTechnique _tecEffets;
    protected EffectPassCollection _passes;

    /// <summary>
    /// Rafraichis les effets de laffichage
    /// </summary>
    /// <param name="device">appareil graphique utilisé</param>
    public void resEffet(GraphicsDevice device)
    {
        _effet = new BasicEffect(device);
        _effet.World = Matrix.CreateOrthographicOffCenter(
            _position.X - device.Viewport.Width / 2,
            device.Viewport.Width / 2 + _position.X,
            device.Viewport.Height / 2 + _position.Y,
            _position.Y - device.Viewport.Height / 2,
            0,
            1
        );

        _tecEffets = _effet.Techniques[0];
        _passes = _tecEffets.Passes;
    }
}
