using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using desktop.utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranScore : GameScreen
{
    private new Geometrik Game => (Geometrik)base.Game;

    
    public EcranScore(Game game, Score score) : base(game)
    {
        AjouterPalmares(score);
        GenererUI();


        Thread t1 = new Thread (async ()=>{
            List<Palmares> scores = await obtenirPalmares();
            GenererScores(scores);

        });
        t1.Start();


    }
    private void GenererUI()
    {
        
    }
    private void GenererScores(List<Palmares> scores){

    }
    public async Task<List<Palmares>> obtenirPalmares()
    {
        Palmares[] liste = await LocalAPI.ObtenirPalmares();
        return liste.ToList<Palmares>();
    }
    public void AfficherPalmares()
    {

    }
    private void AjouterPalmares(Score score)
    {

        Thread t1 = new Thread(async () =>
        {
            ReponseAjouterPalmares reponse = await LocalAPI.AjouterPalmares(score);
            if (reponse != null)
            {
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
