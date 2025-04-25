using System;
using desktop.pages;
using desktop.utils;
using Microsoft.Xna.Framework.Media;

namespace desktop.evenements;

public class EvenementFort : Evenement
{
    protected Chrono _delaiGenerationTank;

    public EvenementFort(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 30f) { }
    public override void DebuterEvenement(float difficulte, float degreeDiff)
    {
        base.DebuterEvenement(difficulte, degreeDiff);
        _delaiGenerationTank = new Chrono(2f / degreeDiff);
        MusiqueAPI.Jouer(MusiqueAPI.Musique.DIFFICULTY2);

    }
    public override void Update(float deltaT)
    {
        if (_delaiGenerationTank.Update(deltaT))
            _ecran.GenererMonstres("tank", 1, (int)_degreeDiff);

        while (_delaiGenerationTank.Update(deltaT))
            _ecran.GenererMonstres("tank", 1, (int)_degreeDiff);
            
        base.Update(deltaT);
    }
}