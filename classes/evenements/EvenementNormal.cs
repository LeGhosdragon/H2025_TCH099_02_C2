using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;

public class EvenementNormal : Evenement
{
    protected Chrono _delaiGenerationNormal;
    protected Chrono _delaiGenerationRunner;
    protected Chrono _delaiGenerationTank;
    public EvenementNormal(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 30f) { }
    public override void DebuterEvenement(float difficulte, float degreeDiff)
    {
        base.DebuterEvenement(difficulte, degreeDiff);
        _delaiGenerationNormal = new Chrono(2f / degreeDiff);
        _delaiGenerationRunner = new Chrono(2.5f / degreeDiff);
        _delaiGenerationTank = new Chrono(1.5f / degreeDiff);
    }
    public override void Update(float deltaT)
    {
        if (_delaiGenerationNormal.Update(deltaT))
            _ecran.GenererMonstres("normal", 1, (int)_degreeDiff);
        if (_degreeDiff > 1)
        {
            if (_delaiGenerationRunner.Update(deltaT))
                _ecran.GenererMonstres("runner", 1, (int)_degreeDiff);
            if (_delaiGenerationTank.Update(deltaT))
                _ecran.GenererMonstres("tank", 1, (int)_degreeDiff);
        }

        while (_delaiGenerationNormal.Update(deltaT))
            _ecran.GenererMonstres("normal", 1, (int)_degreeDiff);
        if (_degreeDiff > 1)
        {
            while (_delaiGenerationRunner.Update(deltaT))
                _ecran.GenererMonstres("runner", 1, (int)_degreeDiff);
            while (_delaiGenerationTank.Update(deltaT))
                _ecran.GenererMonstres("tank", 1, (int)_degreeDiff);
        }
        base.Update(deltaT);
    }
}