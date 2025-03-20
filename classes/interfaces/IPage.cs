using Microsoft.Xna.Framework;

namespace desktop.pages;

public interface IPage
{
    public void Draw(GameTime gameTime);
    public void Update(GameTime gameTime);
}
