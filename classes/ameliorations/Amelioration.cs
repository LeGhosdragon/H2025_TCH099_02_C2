using System;
using System.Collections.Generic;
using desktop.ameliorations.arme;
using desktop.ameliorations.arme.epee;
using desktop.ameliorations.arme.fusil;
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
    protected int _limite {get;set;}
    public Texture2D _image {get;set;}
    public string _description { get;set;}
    public Amelioration(Texture2D image,string description,int limite){
        _image = image;
        _description = description;
        _limite = limite;
    }

    public static void LoadContent(ContentManager content){

        //Ameliorations de joueur

        //Vitesse de mouvement
        Texture2D img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgVitMouvement(img));

        //Rayon d'attraction
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgRayonAttraction(img));

        //Vie 
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgVieJoueur(img));

        //Explosion
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgExplosion(img));

        //Chance crit
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgChanceCrit(img));

        //Degat crit
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgDegatCrit(img));

        //Ameliorations de fusil

        //Vitesse attaque
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgVitAttaqueFusil(img));

        //Taille projectile
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgGrandeurFusil(img));

        //Degat projectile
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgDegatFusil(img));

        //Pierce projectile
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgPierceFusil(img));


        //Ameliorations de l'epee

        //Degat de l'epee
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgDegatEpee(img));

        //Largeur de l'epee
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgTailleEpee(img));

        //Vitesse d'attaque de l'épée
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgVitAttaqueEpee(img));

        //Aire de l'épée
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgAireEpee(img));

        //Recul de l'épée
        img = content.Load<Texture2D>("ball");
        _ameliorations.Add(new UpgReculEpee(img));

    }
    public static Amelioration[] obtenirAmeliorations(int quantiee, IArme typeArme){
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
        return trouverUnique(quantiee,choixPossibles);
    }
    public static Amelioration[] trouverUnique(int quantiee, Amelioration[] choix){
        List<Amelioration> selection = new List<Amelioration>();
        if(choix.Length < quantiee){
            for(int i = 0; i < quantiee; i++){
                selection.Add( choix[i % choix.Length]);
            }
            return selection.ToArray();
        }
        HashSet<int> set = new HashSet<int>();
        while(set.Count < quantiee){
           set.Add(Random.Shared.Next(choix.Length));
        }
        foreach(int pos in set){
            selection.Add(choix[pos]);
        }
        return selection.ToArray();
    }
    public virtual void Appliquer(EcranJeu ecranJeu){
        _limite--;
        ecranJeu.terminerAmelioration();
    }
}