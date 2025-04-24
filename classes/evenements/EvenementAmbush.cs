using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;

public class EvenementAmbush : Evenement
{
    protected Chrono _delaiGenerationRunner;

    public EvenementAmbush(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 30f) { }
    public override void DebuterEvenement(float difficulte, float degreeDiff)
    {
        base.DebuterEvenement(difficulte, degreeDiff);
        _delaiGenerationRunner = new Chrono(2f / degreeDiff);

    }
    public override void Update(float deltaT)
    {
        if (_delaiGenerationRunner.Update(deltaT))
            _ecran.GenererMonstres("runner", 1, (int)_degreeDiff);

        while (_delaiGenerationRunner.Update(deltaT))
            _ecran.GenererMonstres("runner", 1, (int)_degreeDiff);
            
        base.Update(deltaT);
    }
}