using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace desktop.utils;

public class ReponseInscription
{
    public bool Reussite { get; set; } = false;
    public string[] Erreurs { get; set; } = [];
}
public class ReponseConnexion
{
    public bool Reussite { get; set; } = false;
    public string[] Erreurs { get; set; } = [];
    public string jeton { get; set; }
}
public class ReponseAjouterPalmares
{
    public bool Reussite { get; set; } = false;
    public string Erreurs { get; set; }
}
public class ReponseObtenirPalmares
{
    public bool Reussite {get;set;} = false;
    public Palmares[] Palmares {get;set;} = [];
}
public class Palmares{
    public int Id {get;set;}
    public string Nom_Utilisateur {get;set;}
    public int Score {get;set;}
    public int Temps_Partie {get;set;}
    public int Experience {get;set;}
    public int Ennemis_Enleve {get;set;}
    public string Date_Soumission {get;set;}

}
public static class LocalAPI
{
    public static string JetonConnexion { get; private set; }
    public static string _nomUtilisateur { get; private set; }
    static LocalAPI()
    {
        client.BaseAddress = new Uri("http://localhost/serveur/api/api.php/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        JetonConnexion = null;
    }
    static HttpClient client = new HttpClient();

    /// <summary>
    /// Permet d'inscrire le joueur
    /// </summary>
    /// <param name="id">identifiant du joueur</param>
    /// <param name="mdp">mot de passe du joueur</param>
    /// <returns>Retourne la reponse de la requete</returns>
    public async static Task<ReponseInscription> Inscription(String id, String mdp)
    {
        ReponseInscription reponse = null;
        Dictionary<String, String> form = new() { { "passe", mdp }, { "identifiant", id } };
        HttpContent corps = new FormUrlEncodedContent(form);
        HttpResponseMessage response = await client.PostAsync("inscription", corps);
        if (response.IsSuccessStatusCode)
        {
            reponse = await response.Content.ReadFromJsonAsync<ReponseInscription>();
        }
        return reponse;
    }
    /// <summary>
    /// Connecte l'utilisateur
    /// </summary>
    /// <param name="identifiant">identifiant de l'utilisateur</param>
    /// <param name="passe">le mot de passe de l'utilisateur</param>
    /// <returns>Retourne la reponse le la rquete</returns>
    public async static Task<ReponseConnexion> Connexion(string identifiant, string passe)
    {
        ReponseConnexion reponse = null;
        Dictionary<string, string> form = new() { { "passe", passe }, { "identifiant", identifiant } };
        HttpContent corps = new FormUrlEncodedContent(form);
        HttpResponseMessage response = await client.PostAsync("connexion", corps);
        if (response.IsSuccessStatusCode)
        {
            reponse = await response.Content.ReadFromJsonAsync<ReponseConnexion>();
        }
        if (reponse.Reussite)
        {
            _nomUtilisateur = identifiant;
            JetonConnexion = reponse.jeton;
        }
        return reponse;
    }
    /// <summary>
    /// Envoie un score a la base de donnee avec les information du compte deja connecte
    /// </summary>
    /// <param name="score">score a soumettre</param>
    /// <returns>Retourne la reponse de la requete</returns>
    public async static Task<ReponseAjouterPalmares> AjouterPalmares(Score score)
    {
        ReponseAjouterPalmares reponse = null;

        //Valide si l'utilisateur est cconnecte
        if (JetonConnexion == null || _nomUtilisateur == null)
        {
            return null;
        }


        //Creer le corps de la requete 
        Dictionary<String, String> form = new() {   {"jeton",JetonConnexion},
                                                    {"score",score.getScore()+""},
                                                    {"duree",score.GetTempsEnSec()+""},
                                                    {"experience",score._experience+""},
                                                    {"ennemis",score._ennemisEnleve+""}};
        HttpContent corps = new FormUrlEncodedContent(form);

        //Envoie la requete
        HttpResponseMessage response = await client.PostAsync("palmares/ajouter", corps);

        if (response.IsSuccessStatusCode)
        {
            reponse = await response.Content.ReadFromJsonAsync<ReponseAjouterPalmares>();
        }
        return reponse;
    }

    /// <summary>
    /// Obtiens les scores qui sont dans la base de donnee
    /// </summary>
    /// <returns>Reponse dela requete</returns>
    public async static Task<Palmares[]> ObtenirPalmares()
    {
        ReponseObtenirPalmares reponse = null;
        HttpResponseMessage response = await client.GetAsync("palmares/obtenir");

        if (response.IsSuccessStatusCode)
        {
            reponse = await response.Content.ReadFromJsonAsync<ReponseObtenirPalmares>();
            return reponse.Palmares;
        }
        return null;
        
    }
    /// <summary>
    /// Fait une string avec une liste derreurs (toutes les erreurs sont separes d'une ligne)
    /// </summary>
    /// <param name="errs">liste derreur a formatter</param>
    /// <returns>String correpsondant aux erreurs concatenees</returns>
    public static string formatterErreurs(String[] errs)
    {
        String res = "";
        foreach (String s in errs)
        {
            res += s + "\n";
        }
        return res;
    }
    /// <summary>
    /// Deconnecte l'utilisateur
    /// </summary>
    public static void Deconnexion()
    {
        JetonConnexion = null;
    }
    /// <summary>
    /// Verifie si l'utilisateur est connecte
    /// </summary>
    /// <returns>True si l'utilisateur est connecte</returns>
    public static bool EstConnecte()
    {
        return JetonConnexion != null;
    }

}