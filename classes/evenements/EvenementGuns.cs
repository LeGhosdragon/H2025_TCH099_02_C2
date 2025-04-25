using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;

public class EvenementGuns : Evenement
{
    protected Chrono _delaiGenerationGunner;

    public EvenementGuns(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 30f) { }
    public override void DebuterEvenement(float difficulte, float degreeDiff)
    {
        base.DebuterEvenement(difficulte, degreeDiff);
        _delaiGenerationGunner = new Chrono(2f / degreeDiff);
        MusiqueAPI.Jouer(MusiqueAPI.Musique.NORMAL_INTENSE);
    }
    public override void Update(float deltaT)
    {
        if (_delaiGenerationGunner.Update(deltaT))
            _ecran.GenererMonstres("gunner", 1, (int)_degreeDiff);

        while (_delaiGenerationGunner.Update(deltaT))
            _ecran.GenererMonstres("gunner", 1, (int)_degreeDiff);
            
        base.Update(deltaT);
    }
}