using System;
using System.Linq;
using desktop.gameobjects;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;

public class EvenementBoss : Evenement
{
    protected Chrono _delaiGeneration;
    protected Chrono _delaiGenerationNormal;
    protected Chrono _delaiGenerationBoss;

    public EvenementBoss(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 0f, false) { }
    public override void DebuterEvenement(float difficulte, float degreeDiff)
    {
        base.DebuterEvenement(difficulte, degreeDiff);

        _delaiGenerationNormal = new Chrono(1f / degreeDiff);
        _delaiGenerationBoss = new Chrono(120f / degreeDiff);

    }
    public override void Update(float deltaT)
    {
        //Console.WriteLine($"Evenement Boss : {Monstre.CheckSiBoss("bossNormal")} {Monstre.CheckSiBoss("bossRunner")} {Monstre.CheckSiBoss("bossTank")} {Monstre.CheckSiBoss("bossGunner")}");
        if (_delaiGenerationBoss.Update(deltaT))
        {
            string boss = ChooseBossToFind();
            if(boss != "")
                _ecran.GenererMonstres(boss, 1, (int)_degreeDiff);
        }
        if (_delaiGenerationNormal.Update(deltaT) && Monstre.CheckSiBoss("bossNormal"))
            _ecran.GenererMonstres("normal", 1, (int)_degreeDiff, true);
        while (_delaiGenerationNormal.Update(deltaT) && Monstre.CheckSiBoss("bossNormal"))
            _ecran.GenererMonstres("normal", 1, (int)_degreeDiff, true);
        base.Update(deltaT);
    }
    private static string ChooseBossToFind()
    {
        bool[] i = [false, true, true, false, true, true, true];
        if(Monstre.CheckSiBoss("bossNormal"))
        {
            i[0] = true;
            MusiqueAPI.Jouer(MusiqueAPI.Musique.BOSS);
        } 
        // if(Monstre.CheckSiBoss("bossRunner"))
        // {
        //     i[1] = true;
        // } 
        // if(Monstre.CheckSiBoss("bossTank"))
        // {
        //     i[2] = true;
        // } 
        if(Monstre.CheckSiBoss("bossGunner"))
        {
            i[3] = true;
            MusiqueAPI.Jouer(MusiqueAPI.Musique.KIM);
        }
        // if(Monstre.CheckSiBoss("err404"))
        // {
        //     i[4] = true;
        // }
        // if(Monstre.CheckSiBoss("milkMan"))
        // {
        //     i[5] = true;
        // }
        // if(Monstre.CheckSiBoss("bossBunny"))
        // {
        //     i[6] = true;
        // }

        if(i.Contains(false))
        {
            bool _isFindingBoss = true;
            while(_isFindingBoss)
            {
                int randomIndex = new Random().Next(0, 7);
                if(!i[randomIndex])
                {
                    return randomIndex switch
                    {
                        0 => "bossNormal",
                        // 1 => "bossRunner",
                        // 2 => "bossTank",
                        3 => "bossGunner",
                        // 4 => "err404",
                        // 5 => "milkMan",
                        // 6 => "bossBunny",
                        _ => "" 
                    };
                }
            } 
        }
        return ""; 
    }
}