using System;
using System.Threading;
using desktop.utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranScore : GameScreen
{
    public EcranScore(Game game, Score score) : base(game)
    {
        GenererUI();
        AjouterPalmares(score);


    }
    private void GenererUI()
    {

    }
    private void AjouterPalmares(Score score)
    {

        Thread t1 = new Thread(async () =>
        {
            ReponseAjouterPalmares reponse = await LocalAPI.AjouterPalmares(score);
            Console.WriteLine(reponse.Erreurs);
        });
        t1.Start();
    }

    private new Geometrik Game => (Geometrik)base.Game;

    public override void Draw(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {

    }
}
