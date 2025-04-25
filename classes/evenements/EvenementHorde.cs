using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;

public class EvenementHorde : Evenement
{
    protected Chrono _delaiGenerationNormal;

    public EvenementHorde(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 30f) { }
    public override void DebuterEvenement(float difficulte, float degreeDiff)
    {
        base.DebuterEvenement(difficulte, degreeDiff);
        _delaiGenerationNormal = new Chrono(2f / degreeDiff);
        MusiqueAPI.Jouer(MusiqueAPI.Musique.SPACE1);

    }
    public override void Update(float deltaT)
    {
        if (_delaiGenerationNormal.Update(deltaT))
            _ecran.GenererMonstres("normal", 1, (int)_degreeDiff);

        while (_delaiGenerationNormal.Update(deltaT))
            _ecran.GenererMonstres("normal", 1, (int)_degreeDiff);

        base.Update(deltaT);
    }
}