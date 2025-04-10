
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using desktop.ameliorations;
using desktop.armes;
using desktop.gameobjects;
using desktop.ui;
using desktop.utils;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranJeu : GameScreen
{
    private new Geometrik Game => (Geometrik)base.Game;
    public Joueur _joueur { get; }
    protected List<IGameObject> _objets;
    protected Chrono _chronoMonstre;
    protected int _banqueExp = 0;

    public SpriteFont _font {get;set;}
    public Score _score {get;set;}
    /// <summary>
    /// Touches qui ont ete appuye pour lesquels les eevenements on deja ete actives
    /// </summary>
    public List<Touche> _touches = new List<Touche>();

    /// <summary>
    /// Variables de statut de partie
    /// </summary>
    public bool _arrete = false;
    public bool _menuPause = false;
    public bool _finPartie = false;
    

    public EcranJeu(Game game) : base(game)
    {
        string nomUtilisateur = "Invite";
        if(LocalAPI._nomUtilisateur != null){
            nomUtilisateur = LocalAPI._nomUtilisateur;
        }
        _score = new Score(nomUtilisateur);

        _objets = new List<IGameObject>();
        _joueur = new Joueur(new Vector2(0, 0), this);

        AbstractArme arme = new Fusil(_joueur, this);
        _joueur._arme = arme;
        _objets.Add(arme);

        _objets.Add(_joueur);

        new Camera(GraphicsDevice, _joueur.getPosition());
        _chronoMonstre = new Chrono(3f);

    }

    public override void LoadContent()
    {
        Amelioration.LoadContent(Content);
        _font = Content.Load<SpriteFont>("GeonBit.UI/Themes/editor/fonts/Regular");
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {

        Camera.setPosition(_joueur.getPosition());
        Game.GraphicsDevice.Clear(Color.Black);
        Game.GetSpriteBatch().Begin();
        foreach (IGameObject objet in _objets)
        {
            objet.Draw(Game.GetSpriteBatch());
        }
        Game.GetSpriteBatch().End();

        UserInterface.Active.Draw(Game.GetSpriteBatch());

    }

    public override void Update(GameTime gameTime)
    {           
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Touche.enleverTouches(_touches);

        //Permet de mettre le jeu en pause
        if (Touche.ValiderTouche(_touches, ControlesEnum.PAUSE) && Controle.enfonceClavier(ControlesEnum.PAUSE))
        {
            _touches.Add(new Touche(ControlesEnum.PAUSE));
            _menuPause = !_menuPause;
        }

        if (!_arrete && !_menuPause)
        {
            if (_chronoMonstre.Update(deltaT))
            {
                GenererMonstres(5);
            }

            foreach (IGameObject objet in _objets.Reverse<IGameObject>())
            {
                objet.Update(deltaT);
            }

        }
        if(boites != null){
            foreach (BoiteAmelioration boite in boites){
                boite.Update(deltaT,Game.GraphicsDevice);
            }
        }
        _score.Update((int)gameTime.ElapsedGameTime.TotalMilliseconds);
        UserInterface.Active.Update(gameTime);
    }
    /// <summary>
    /// Genere une quantitee desiree de monstres
    /// </summary>
    /// <param name="quantitee">quantitee de monstres a generer</param>
    public void GenererMonstres(int quantitee)
    {
        for (int i = 0; i < quantitee; i++)
        {
            GenererMonstre();
        }
    }

    /// <summary>
    /// Ajoute une quantiee d'experience qui est sortie des bornes
    /// </summary>
    /// <param name="valeur">quantitee d'experience a ajouter</param>
    public void ajouteBanqueExp(int valeur)
    {
        _banqueExp += valeur;
    }

    /// <summary>
    /// Cree un nouveau monstre et l'ajoute dans le jeu
    /// </summary>
    public void GenererMonstre()
    {
        Monstre monstre = new Monstre(PolyGen.GetPoly(3, 20), new Vector2(100, 100), this, 20);
        _objets.Add(monstre);
    }
    /// <summary>
    /// Retourne tout les objets de type monstre dans le jeu
    /// </summary>
    /// <returns>Objets de type monstre</returns>
    public List<Monstre> GetMonstres()
    {
        return _objets.OfType<Monstre>().ToList();
    }

    /// <summary>
    /// Retourne tout les objets de type experience dans le jeu
    /// </summary>
    /// <returns>Objets de type experience</returns>
    public List<Experience> GetExperiences()
    {
        return _objets.OfType<Experience>().ToList();
    }
    /// <summary>
    /// Enleve un objet a la scene, il sera enlever apres l'update actuelle
    /// </summary>
    /// <param name="gameObject"></param>
    public void EnleverObjet(IGameObject gameObject)
    {
        _objets.Remove(gameObject);
    }
    /// <summary>
    /// Ajoute un objet a la scene, il sera update  et dessine apres l'image actuelle
    /// </summary>
    /// <param name="gameObject">objet a ajouter</param>
    public void AjouerObjet(IGameObject gameObject)
    {
        _objets.Add(gameObject);
    }
    private BoiteAmelioration[] boites;
    public void augmenterNiveau(Joueur joueur)
    {
        _arrete = true;
        boites = BoiteAmelioration.genererAmelioration(3, this);
        foreach (BoiteAmelioration boite in boites)
        {
            UserInterface.Active.AddEntity(boite);
        }

    }
    public void terminerAmelioration(){
                foreach (BoiteAmelioration boite in boites)
        {
            UserInterface.Active.RemoveEntity(boite);
        }
        _arrete =false;
    }
    public void FinPartie(){
        _arrete = true;
        BoiteScore boiteScore = new BoiteScore(_score,this);
        UserInterface.Active.AddEntity(boiteScore);
    }
    public void ChargerEcranScore(Score score){
        UnloadContent();
        Game.LoadEcranScore(score);
    }
    public void UnloadContent(){
        UserInterface.Active.Clear();
    }
}