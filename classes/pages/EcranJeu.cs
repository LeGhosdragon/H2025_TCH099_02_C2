
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using desktop.ameliorations;
using desktop.armes;
using desktop.evenements;
using desktop.gameobjects;
using desktop.ui;
using desktop.utils;
using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace desktop.pages;

public class EcranJeu : GameScreen
{
    private Texture2D _imgHpBas;
    private new Geometrik Game => (Geometrik)base.Game;
    public Joueur _joueur { get; }
    protected FondJeu _fond;
    protected BarreExp _barreExp;
    protected List<IGameObject> _objets;
    protected BoitePause _boitePause;
    protected int _banqueExp = 0;
    protected DirecteurEvenement _directeurEvenement { get; }
    public SpriteFont _font { get; set; }
    public Score _score { get; set; }

    /// <summary>
    /// Touches qui ont ete appuye pour lesquels les eevenements on deja ete actives
    /// </summary>
    public List<Touche> _touches = new List<Touche>();


    public EtatJeu _etat { get; set; } = EtatJeu.EN_COURS;


    public EcranJeu(Game game, TypesArmes typesArme) : base(game)
    {

        _directeurEvenement = new DirecteurEvenement(this);
        string nomUtilisateur = "Invite";
        if (LocalAPI._nomUtilisateur != null)
        {
            nomUtilisateur = LocalAPI._nomUtilisateur;
        }
        _score = new Score(nomUtilisateur);

        _objets = new List<IGameObject>();
        _joueur = new Joueur(new Vector2(0, 0), this);

        AbstractArme arme;

        switch (typesArme)
        {
            case TypesArmes.EPEE:
                arme = new Epee(_joueur, this);
                break;
            case TypesArmes.FUSIL:
            default:
                arme = new Fusil(_joueur, this);
                break;
        }
        _joueur._arme = arme;
        _objets.Add(arme);

        _objets.Add(_joueur);

        _barreExp = new BarreExp(_joueur);
        _fond = new FondJeu(this);

        new Camera(GraphicsDevice, _joueur.getPosition());
    }

    public override void LoadContent()
    {
        Amelioration.LoadContent(Content);
        _font = Content.Load<SpriteFont>("GeonBit.UI/Themes/editor/fonts/Regular");

        _imgHpBas = Content.Load<Texture2D>("gradient-rouge");

        Effect effetFond = Content.Load<Effect>("effet-fond-1");
        _fond._effet = effetFond;
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        Camera.setPosition(_joueur.getPosition());
        Game.GraphicsDevice.Clear(Color.Black);
        Game.GetSpriteBatch().Begin();
        _fond.Draw(Game.GetSpriteBatch());

        foreach (IGameObject objet in _objets)
        {
            objet.Draw(Game.GetSpriteBatch());
        }
        if (_joueur.hpBas())
        {
            //On pourrait faire fade in la couleur
            int maxX = GraphicsDevice.Viewport.Width;
            int maxY = GraphicsDevice.Viewport.Height;
            Game.GetSpriteBatch().Draw(_imgHpBas, new Rectangle(Point.Zero, new Point(maxX, maxY)), new Color(0.8f, 0.8f, 0.8f, 0.2f));
        }

        _barreExp.Draw(Game.GetSpriteBatch());
        Game.GetSpriteBatch().End();


        UserInterface.Active.Draw(Game.GetSpriteBatch());


    }

    public void ChangerPause()
    {
        if (_etat == EtatJeu.PAUSE)
        {
            _etat = EtatJeu.EN_COURS;
            UserInterface.Active.RemoveEntity(_boitePause);
        }
        else if (_etat == EtatJeu.EN_COURS)
        {
            _etat = EtatJeu.PAUSE;
            _boitePause = new BoitePause(this);
            UserInterface.Active.AddEntity(_boitePause);
        }
    }

    public override void Update(GameTime gameTime)
    {
        float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Touche.enleverTouches(_touches);

        //Permet de mettre le jeu en pause
        if (Touche.ValiderTouche(_touches, ControlesEnum.PAUSE) && Controle.enfonceClavier(ControlesEnum.PAUSE))
        {
            _touches.Add(new Touche(ControlesEnum.PAUSE));
            ChangerPause();
        }

        if (_etat == EtatJeu.EN_COURS)
        {
            _directeurEvenement.Update(deltaT);

            foreach (IGameObject objet in _objets.Reverse<IGameObject>())
            {
                objet.Update(deltaT);
            }
            _score.Update((int)gameTime.ElapsedGameTime.TotalMilliseconds);
        }
        if (boites != null)
        {
            foreach (BoiteAmelioration boite in boites)
            {
                boite.Update(deltaT, Game.GraphicsDevice);
            }
        }

        _fond.Update(_joueur.getPosition(), deltaT);
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
        _etat = EtatJeu.AMELIORATION;
        boites = BoiteAmelioration.genererAmelioration(3, this);
        foreach (BoiteAmelioration boite in boites)
        {
            UserInterface.Active.AddEntity(boite);
        }

    }
    public void terminerAmelioration()
    {
        foreach (BoiteAmelioration boite in boites)
        {
            UserInterface.Active.RemoveEntity(boite);
        }
        _etat = EtatJeu.EN_COURS;
    }
    public void FinPartie()
    {
        
        if (_etat == EtatJeu.AMELIORATION)
        {
            return;
        }
        if(_etat == EtatJeu.PAUSE){
            UserInterface.Active.RemoveEntity(_boitePause);
        }
        _etat = EtatJeu.FIN;
        BoiteScore boiteScore = new BoiteScore(_score, this);
        UserInterface.Active.AddEntity(boiteScore);
    }
    public void ChargerEcranScore(Score score)
    {
        UnloadContent();
        Game.LoadEcranScore();
        AjouterPalmares(score);
    }
    public override void UnloadContent()
    {
        UserInterface.Active.Clear();
    }

    private void AjouterPalmares(Score score)
    {

        Thread t1 = new Thread(async () =>
        {
            ReponseAjouterPalmares reponse = await LocalAPI.AjouterPalmares(score);
            if (reponse != null)
            {
                Console.WriteLine(reponse.Erreurs);
            }
        });
        t1.Start();
    }
    public enum EtatJeu
    {
        EN_COURS,
        AMELIORATION,
        FIN,
        PAUSE,
    }
}
