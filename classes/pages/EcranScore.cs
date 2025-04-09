using desktop.utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranScore : GameScreen
{
    public EcranScore(Game game,Score score) : base(game){    }

    private new Geometrik Game => (Geometrik)base.Game;

    public override void Draw(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {
        
    }
}
