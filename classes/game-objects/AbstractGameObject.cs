using System.Diagnostics;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public abstract class AbstractGameObject : IGameObject
{
    protected Vector2[] _forme;
    protected Vector3 _position;
    protected BasicEffect _effet;
    protected EffectTechnique _tecEffets;
    protected EffectPassCollection _passes;

    public AbstractGameObject(Vector2[] forme, GraphicsDevice graphics)
    {
        this._forme = forme;
        resEffet(graphics);
    }

    public void Draw(GraphicsDevice device)
    {
        VertexPositionColor[] formeVis = PolyGen.GenererFormeVide(_forme, _position, Color.White);
        foreach (EffectPass pass in _passes)
        {
            pass.Apply();
            device.DrawUserPrimitives(PrimitiveType.LineStrip, formeVis, 0, formeVis.Length-1);
        }
    }

    public void Update(float deltaT)
    {
        _forme = PolyGen.tournerMatrice(_forme, deltaT * 0.2f);
    }

    public Vector3 getPosition()
    {
        return _position;
    }

    public void setPosition(Vector3 position)
    {
        _position = position;
    }

    public void resEffet(GraphicsDevice device)
    {
        _effet = new BasicEffect(device);
        _effet.World = Matrix.CreateOrthographicOffCenter(
            0,
            device.Viewport.Width,
            device.Viewport.Height,
            0,
            0,
            1
        );

        _tecEffets = _effet.Techniques[0];
        _passes = _tecEffets.Passes;
    }
}
