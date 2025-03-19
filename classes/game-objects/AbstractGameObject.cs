using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public abstract class AbstractGameObject : IGameObject
{
    protected VertexPositionColor[] _forme;

    protected BasicEffect _effet;
    protected EffectTechnique _tecEffets;
    protected EffectPassCollection _passes;

    public AbstractGameObject(VertexPositionColor[] forme, GraphicsDevice graphics)
    {
        this._forme = forme;
        _effet = new BasicEffect(graphics);
        _effet.World = Matrix.CreateOrthographicOffCenter(
            0,
            graphics.Viewport.Width,
            graphics.Viewport.Height,
            0,
            0,
            1
        );

        _tecEffets = _effet.Techniques[0];
        _passes = _tecEffets.Passes;
    }

    public void Draw(GraphicsDevice device)
    {
        foreach (EffectPass pass in _passes)
        {
            pass.Apply();
            device.DrawUserPrimitives(PrimitiveType.LineStrip, _forme, 0, 4);
        }
    }

    public void Update() { }
}
