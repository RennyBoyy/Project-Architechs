using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class NeighborhoodsRequestManager : MonoBehaviour
{
    public TextMeshProUGUI neighborhoodText; // Texto para exibir o nome do bairro
    public int selectedNeighborhoodID; // ID do bairro selecionado no editor

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para corresponder à URL da sua API
    private Dictionary<int, Neighborhood> neighborhoods; // Dicionário para armazenar os bairros

    void Start()
    {
        GetAllNeighborhoods();
    }

    public void GetAllNeighborhoods()
    {
        StartCoroutine(GetNeighborhoodsCoroutine());
    }

    IEnumerator GetNeighborhoodsCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "Neighborhood"))
        {
            // Envie a requisição e aguarde a resposta
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informações: " + webRequest.error);
            }
            else
            {
                // Deserializar a resposta JSON para uma lista de objetos Neighborhood
                List<Neighborhood> neighborhoodList = JsonConvert.DeserializeObject<List<Neighborhood>>(webRequest.downloadHandler.text);
                neighborhoods = new Dictionary<int, Neighborhood>();
                foreach (var neighborhood in neighborhoodList)
                {
                    neighborhoods[neighborhood.NeighborhoodID] = neighborhood;
                }

                UpdateNeighborhoodDisplay();
            }
        }
    }

    private void UpdateNeighborhoodDisplay()
    {
        if (neighborhoods != null && neighborhoods.ContainsKey(selectedNeighborhoodID))
        {
            var neighborhood = neighborhoods[selectedNeighborhoodID];
            if (neighborhoodText != null)
            {
                neighborhoodText.text = "Neighborhood: " + neighborhood.Name;
            }
        }
    }
}