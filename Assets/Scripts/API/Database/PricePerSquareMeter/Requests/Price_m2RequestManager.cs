using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Price_m2RequestManager : MonoBehaviour
{
    public TextMeshProUGUI priceText;

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para a URL da sua API

    void Start()
    {
        GetAllPrices();
    }

    public void GetAllPrices()
    {
        StartCoroutine(GetPricesCoroutine());
    }

    IEnumerator GetPricesCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "Prices"))
        {
            // Envie a requisi��o e aguarde a resposta
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informa��es: " + webRequest.error);
            }
            else
            {
                // Deserializar a resposta JSON para uma lista de objetos Price_m2
                List<Price_m2> prices = JsonConvert.DeserializeObject<List<Price_m2>>(webRequest.downloadHandler.text);

                // Embaralhar a lista
                ShuffleList(prices);

                // Come�a a rotina para mostrar os valores dos pre�os
                StartCoroutine(DisplayPrices(prices));
            }
        }
    }

    IEnumerator DisplayPrices(List<Price_m2> prices)
    {
        while (true) // Loop infinito para continuar mostrando os valores de pre�os
        {
            foreach (Price_m2 price in prices)
            {
                if (priceText != null)
                {
                    priceText.text = price.PriceValue; // Aqui voc� mostra os valores dos pre�os
                }
                else
                {
                    Debug.LogError("Componente TextMeshProUGUI n�o est� atribu�do!");
                    yield break; // Sai da corrotina se n�o houver TextMeshProUGUI
                }

                yield return new WaitForSeconds(3); // Espera por 3 segundos antes de mostrar o pr�ximo valor
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
