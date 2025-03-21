using System;
using System.Collections.Generic;
using desktop.gameobjects;
using desktop.pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.utils;

public class Camera
{
    private static Camera instance = getInstance();
    protected Vector3 _position;

    private Camera()
    {
        _position = Vector3.Zero;
    }

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

    public void Update(float deltaT)
    {
        throw new System.NotImplementedException();
    }

    public Vector3 getPosition3D()
    {
        return _position;
    }

    public Vector2 getPosition2D()
    {
        return MathPlus.EnVector2(_position);
    }

    public Vector2 objetPosEnPX(Vector2 posObj)
    {
        return new Vector2(
            posObj.X + _effet.GraphicsDevice.Viewport.Width / 2 + _position.X / 2,
            posObj.Y + _effet.GraphicsDevice.Viewport.Height / 2 + _position.Y / 2
        );
    }

    public Vector2 getPosSourisCamera()
    {
        //Console.WriteLine(Controle.getPosSouris());
        //Console.WriteLine(objetPosEnPX(getPosition2D()));

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
