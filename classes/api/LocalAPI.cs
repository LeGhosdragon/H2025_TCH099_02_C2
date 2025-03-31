using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace desktop.utils;

public static class LocalAPI
{
    
    static HttpClient client = new HttpClient();
    static async Task RunAsync(){
        client.BaseAddress = new Uri("http");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async static Task<String[]> Inscription(String identifiant, String passe)
    {



        return new string[] { "REUSSITE" };
    }


}