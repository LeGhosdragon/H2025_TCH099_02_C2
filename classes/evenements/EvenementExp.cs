using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;

public class EvenementExp : Evenement
{
    protected Chrono _delaiGeneration;

    public EvenementExp(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 999f, false) { }
    public override void DebuterEvenement(float difficulte, float degreeDiff)
    {
        base.DebuterEvenement(difficulte, degreeDiff);
        _delaiGeneration = new Chrono(60f);
    }
    public override void Update(float deltaT)
    {
        if (_delaiGeneration.Update(deltaT))
            _ecran.GenererMonstres("exp", 1, (int)_degreeDiff);

        while (_delaiGeneration.Update(deltaT))
            _ecran.GenererMonstres("exp", 1, (int)_degreeDiff);

        base.Update(deltaT);
    }
}