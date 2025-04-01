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
public class ReponseConnexion{
    public bool Reussite {get;set;} = false;
    public String[] Erreurs {get;set;} = [];
    public String jeton {get;set;}
}
public static class LocalAPI
{
    static String JetonConnexion;
    static LocalAPI(){
        client.BaseAddress = new Uri("http://localhost/serveur/api/api.php/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            JetonConnexion = null;
    }
    static HttpClient client = new HttpClient();
    public async static Task<ReponseInscription> Inscription(String id, String mdp)
    {
        

        ReponseInscription reponse = null;
        Dictionary<String,String> form = new() {{"passe", mdp}, {"identifiant", id}};
        HttpContent corps = new FormUrlEncodedContent(form);
        HttpResponseMessage response = await client.PostAsync("inscription", corps);
        if (response.IsSuccessStatusCode)
        {
            reponse = await response.Content.ReadFromJsonAsync<ReponseInscription>();
        }
        return reponse;



    }
    public async static Task<ReponseConnexion> Connexion(String identifiant,String passe){
        ReponseConnexion reponse = null;
        Dictionary<String,String> form = new() {{"passe", passe}, {"identifiant", identifiant}};
        HttpContent corps = new FormUrlEncodedContent(form);
        HttpResponseMessage response = await client.PostAsync("connexion", corps);
        if (response.IsSuccessStatusCode)
        {
            reponse = await response.Content.ReadFromJsonAsync<ReponseConnexion>();
        }

        return reponse;
    }

    public static String formatterErreurs(String[] errs){
        String res = "";
        foreach(String s in errs){
            res += s + "\n";
        }
        return res;
    }

}