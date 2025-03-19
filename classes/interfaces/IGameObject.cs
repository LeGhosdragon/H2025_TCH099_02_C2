using Microsoft.Xna.Framework.Graphics;

namespace desktop.gameobjects;

public interface IGameObject
{
    public void Draw(GraphicsDevice device);
    public void Update();
}
