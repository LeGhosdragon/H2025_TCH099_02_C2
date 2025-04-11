using System;
using desktop.pages;
using desktop.utils;

namespace desktop.evenements;
public class Evenement{

    protected Chrono _duree {get;set;}
    protected float _difficulte {get;set;}
    protected float _degreeDiff;
    protected DirecteurEvenement _directeur {get;}
    protected EcranJeu _ecran {get;} 
    public Evenement(DirecteurEvenement directeur,EcranJeu ecranJeu,float duree){
        _duree = new Chrono(duree,true);
        _directeur = directeur;
        _ecran = ecranJeu;
    }
    public virtual void DebuterEvenement(float difficulte,float degreeDiff){
        this._difficulte = difficulte;
        this._degreeDiff = degreeDiff;
        _duree.reinitialiser();
    }
    public virtual void Update(float deltaT){
        if(_duree.Update(deltaT)){
            FinEvenement();
        }
    }

    public virtual void FinEvenement(){
        _directeur.FinEvenement(this);
    }
}