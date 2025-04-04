using System;
using System.Collections.Generic;
using desktop.ameliorations.arme;
using desktop.ameliorations.joueur;
using desktop.armes;
using desktop.gameobjects;
using desktop.pages;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace desktop.ameliorations;
public abstract class Amelioration{
    public static List<Amelioration> _ameliorations {get;set;} = new List<Amelioration>() ;
    protected int _limite {get;}
    public Texture2D _image {get;set;}
    public String _description { get;}
    public Amelioration(Texture2D image,String description,int limite){
        _image = image;
        _description = description;
        _limite = limite;
    }

    public static void LoadContent(ContentManager content){
        //Ameliorations de joueur
        Texture2D img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgVitMouvement(img));

        //Ameliorations de fusil

        //Ameliorations de l'epee
    }
    public static Amelioration[] obtenirAmeliorations(int quantiee, AbstractArme typeArme){
        Amelioration[] choixPossibles = _ameliorations.FindAll(a =>{
            if(a._limite == 0){
                return false;
            }
            if(a is AbstractUpgJoueur){
                return true;
            }
            if(a is AbstractUpgArme){
               return ((AbstractUpgArme) a).estBonType(typeArme);
            }
            return false;
        }).ToArray();
        return choixPossibles;
    }
    public virtual void Appliquer(EcranJeu ecranJeu){
        ecranJeu.terminerAmelioration();
    }
}