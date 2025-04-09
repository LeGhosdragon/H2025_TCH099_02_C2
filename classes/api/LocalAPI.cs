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
    public String[] Erreurs { get; set; } = [];
}
public class ReponseConnexion
{
    public bool Reussite { get; set; } = false;
    public String[] Erreurs { get; set; } = [];
    public String jeton { get; set; }
}
public static class LocalAPI
{
    public static String JetonConnexion {get; private set;}
    public static String _nomUtilisateur {get;private set;}
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
    public async static Task<ReponseConnexion> Connexion(String identifiant, String passe)
    {
        ReponseConnexion reponse = null;
        Dictionary<String, String> form = new() { { "passe", passe }, { "identifiant", identifiant } };
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
    /// Fait une string avec une liste derreurs (toutes les erreurs sont separes d'une ligne)
    /// </summary>
    /// <param name="errs">liste derreur a formatter</param>
    /// <returns>String correpsondant aux erreurs concatenees</returns>
    public static String formatterErreurs(String[] errs)
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