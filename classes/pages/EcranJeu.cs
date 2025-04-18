
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using desktop;
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


public class EcranJeu : GameScreen
{
    private new Geometrik Game => (Geometrik)base.Game;
    public Joueur _joueur { get; }
    protected Fond _fond = new Fond();
    protected List<IGameObject> _objets;
    public int _banqueExp { get; set; }
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
        _banqueExp = 0;
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

        new Camera(GraphicsDevice, _joueur.getPosition());
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
        foreach (ProjectileEnnemi projectile in MonstreGunner.getProjectiles())
        {
            projectile.Draw(Game.GetSpriteBatch());
        }
        _fond.Draw(Game.GetSpriteBatch());

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
            if (_etat == EtatJeu.PAUSE)
            {
                _etat = EtatJeu.EN_COURS;
            }
            else if (_etat == EtatJeu.EN_COURS)
            {
                _etat = EtatJeu.PAUSE;
            }
        }

        if (_etat == EtatJeu.EN_COURS)
        {
            _directeurEvenement.Update(deltaT);

            foreach (ProjectileEnnemi projectile in MonstreGunner.getProjectiles().Reverse<ProjectileEnnemi>())
            {
                projectile.Update(deltaT, _joueur);

            }
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

        _fond.Update(_joueur.getPosition());
        UserInterface.Active.Update(gameTime);
    }
    /// <summary>
    /// Genere une quantitee desiree de monstres
    /// </summary>
    /// <param name="quantitee">quantitee de monstres a generer</param>
    public void GenererMonstres(string type, int quantitee, int degreeDiff, bool fromBoss = false)
    {
        for (int i = 0; i < quantitee; i++)
        {
            if(!fromBoss)
                GenerateurMonstre.GenererMonstre(type, this, _objets, 1, degreeDiff);
            else
                GenerateurMonstre.GenererMonstre(type, this, _objets, 1, degreeDiff, fromBoss, Monstre.GetMonstre("bossNormal").getPosition());
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
        if (_etat != EtatJeu.EN_COURS)
        {
            return;
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
