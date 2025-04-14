using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;

public class EvenementNormal : Evenement
{
    protected Chrono _delaiGeneration;
    public EvenementNormal(DirecteurEvenement directeur, EcranJeu ecranJeu) : base(directeur, ecranJeu, 30f)
    {


    }
    public override void DebuterEvenement(float difficulte,float degreeDiff)
    { 
        base.DebuterEvenement(difficulte,degreeDiff);
        _delaiGeneration = new Chrono(0.6f/degreeDiff);
    }
    public override void Update(float deltaT)
    {
                    

        if(_delaiGeneration.Update(deltaT)){
            _ecran.GenererMonstre();

        }
        while(_delaiGeneration.Update(deltaT)){
            _ecran.GenererMonstre();
        }
        base.Update(deltaT);

    }
}