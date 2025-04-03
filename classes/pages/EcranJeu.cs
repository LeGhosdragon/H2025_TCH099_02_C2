
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using desktop.armes;
using desktop.gameobjects;
using desktop.utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranJeu : GameScreen
{
    private new Geometrik Game => (Geometrik)base.Game;
    protected Joueur _joueur;
    protected List<IGameObject> _objets;
    protected List<IGameObject> _aEnlever;
    protected List<IGameObject> _aAjouter;
    protected Chrono _chronoMonstre;

    public EcranJeu(Game game) : base(game)
    {
        _objets = new List<IGameObject>();
        _aEnlever = new List<IGameObject>();
        _aAjouter = new List<IGameObject>();
        _joueur = new Joueur(PolyGen.GetPoly(100, 100), new Vector2(0, 0));

        AbstractArme arme = new Fusil(_joueur,this);
        _joueur.setArme(arme);
        _objets.Add(arme);

        _objets.Add(_joueur);

        new Camera(GraphicsDevice,_joueur.getPosition());
        _chronoMonstre = new Chrono(3f);
        
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        Camera.setPosition(_joueur.getPosition());
        Game.GraphicsDevice.Clear(Color.Black);
        Game.GetSpriteBatch().Begin();
        foreach(IGameObject objet in _objets){
            if(_aEnlever.Contains(objet)){
                Console.WriteLine("ObjetEnleve updtate");
            }
            objet.Draw(Game.GetSpriteBatch());
        }
        Game.GetSpriteBatch().End();

    }

    public override void Update(GameTime gameTime)
    {
        float deltaT = (float) gameTime.ElapsedGameTime.TotalSeconds;
        if (_chronoMonstre.Update(deltaT))
        {
           GenererMonstres(5);
        }
        foreach(IGameObject gameObject in _aAjouter){
            _objets.Add(gameObject);
        }
        _aAjouter = new List<IGameObject>();
        foreach(IGameObject objet in _objets){
            objet.Update(deltaT);
        }

        foreach(IGameObject gameObject in _aEnlever){
            _objets.Remove(gameObject);
        }
        _aEnlever = new List<IGameObject>();
    }
    public void GenererMonstres(int quantitee){
        for(int i = 0;i < quantitee;i++){
            GenererMonstre();
        }
    }
    public void GenererMonstre(){
        Monstre monstre = new Monstre(PolyGen.GetPoly(3, 20), new Vector2(100, 100), this, 20);
        _objets.Add(monstre);
    }
    public List<Monstre> GetMonstres()
    {
        return _objets.OfType<Monstre>().ToList<Monstre>();
    }
    public List<Experience> GetExperiences(){
        return _objets.OfType<Experience>().ToList<Experience>();
    }
    public Joueur GetJoueur(){
        return _joueur;
    }
    public void EnleverObjet(IGameObject gameObject){
        _aEnlever.Add(gameObject);
    }
    public void AjouerObjet(IGameObject gameObject){
        _aAjouter.Add(gameObject);
    }
}