using System;
using System.Collections.Generic;
using System.Linq;
using desktop.pages;

namespace desktop.evenements;
public class DirecteurEvenement{
    public List<Evenement> _evenementsActifs {get;set;} = new List<Evenement>();
    public List<Evenement> _evenementsDisponnibles {get;set;} = new List<Evenement>();
    public EcranJeu _ecranJeu {get;}
    public float _difficulte {get;private set;} = 1;
    public float _degreeDiff {get;private set;} = 1;
    
    public DirecteurEvenement(EcranJeu ecranJeu){
        this._ecranJeu = ecranJeu;
        _evenementsDisponnibles.Add(new EvenementNormal(this,_ecranJeu));
        DebuterEvenement();
    }

    public void Update(float deltaT){

        foreach(Evenement evenement in _evenementsActifs.Reverse<Evenement>()){
            evenement.Update(deltaT);
        }

    }
    public void FinEvenement(Evenement evenement){
        _difficulte = _difficulte *1.1f;
        _evenementsActifs.Remove(evenement);
        
        DebuterEvenement();
    }
    public void DebuterEvenement(){
        Evenement evenementChoisis = _evenementsDisponnibles[Random.Shared.Next(_evenementsDisponnibles.Count)];
        _evenementsActifs.Add(evenementChoisis);
        evenementChoisis.DebuterEvenement(_difficulte,_degreeDiff);
    }
}