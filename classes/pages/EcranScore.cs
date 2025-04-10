using System;
using System.Collections.Generic;
using System.Threading;
using desktop.utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranScore : GameScreen
{
        private new Geometrik Game => (Geometrik)base.Game;

    public EcranScore(Game game, Score score) : base(game)
    {
        GenererUI();
        AjouterPalmares(score);


    }
    private void GenererUI()
    {

    }
    public List<Score> obtenirPalmares(){
        return new List<Score>();
    }
    public void AfficherPalmares(){

    }
    private void AjouterPalmares(Score score)
    {

        Thread t1 = new Thread(async () =>
        {
            ReponseAjouterPalmares reponse = await LocalAPI.AjouterPalmares(score);
            if(reponse != null){
                Console.WriteLine(reponse.Erreurs);
            }
        });
        t1.Start();
    }


    public override void Draw(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {

    }
}
