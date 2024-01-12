using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SentimentRequestManager : MonoBehaviour
{
    public TextMeshProUGUI sentimentText;

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para a URL da sua API

    void Start()
    {
        GetAllPublicSentiments();
    }

    public void GetAllPublicSentiments()
    {
        StartCoroutine(GetPublicSentimentsCoroutine());
    }

    IEnumerator GetPublicSentimentsCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "PublicSentiment"))
        {
            // Envie a requisição e aguarde a resposta
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informações: " + webRequest.error);
            }
            else
            {
                // Deserializar a resposta JSON para uma lista de objetos Public_Sentiment
                List<Public_Sentiment> sentiments = JsonConvert.DeserializeObject<List<Public_Sentiment>>(webRequest.downloadHandler.text);

                // Embaralhar a lista
                ShuffleList(sentiments);

                // Começa a rotina para mostrar os comentários
                StartCoroutine(DisplaySentiments(sentiments));
            }
        }
    }

    IEnumerator DisplaySentiments(List<Public_Sentiment> sentiments)
    {
        while (true) // Loop infinito para continuar mostrando os comentários
        {
            foreach (Public_Sentiment sentiment in sentiments)
            {
                if (sentimentText != null)
                {
                    sentimentText.text = sentiment.Comments; // Aqui você mostra os comentários
                }
                else
                {
                    Debug.LogError("Componente TextMeshProUGUI não está atribuído!");
                    yield break; // Sai da corrotina se não houver TextMeshProUGUI
                }

                yield return new WaitForSeconds(3); // Espera por 3 segundos antes de mostrar o próximo comentário
            }
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        System.Random rand = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}