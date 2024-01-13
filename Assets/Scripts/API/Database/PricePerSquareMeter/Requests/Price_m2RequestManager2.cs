using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI; // Necessário para o componente Button

public class Price_m2RequestManager2 : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public Button previousButton;
    public Button nextButton;

    private List<Price_m2> prices;
    private int currentIndex = 0;

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para a URL da sua API

    void Start()
    {
        GetAllPrices();
        if (previousButton != null) previousButton.onClick.AddListener(PreviousPrice);
        if (nextButton != null) nextButton.onClick.AddListener(NextPrice);
    }

    public void GetAllPrices()
    {
        StartCoroutine(GetPricesCoroutine());
    }

    IEnumerator GetPricesCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "Prices"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informações: " + webRequest.error);
            }
            else
            {
                prices = JsonConvert.DeserializeObject<List<Price_m2>>(webRequest.downloadHandler.text);
                UpdatePriceDisplay();
            }
        }
    }

    private void UpdatePriceDisplay()
    {
        if (prices != null && prices.Count > 0 && descriptionText != null)
        {
            descriptionText.text = prices[currentIndex].PriceValue;
            StartCoroutine(DownloadAndDisplayText(prices[currentIndex].LinkExtractedText));
        }
    }

    IEnumerator DownloadAndDisplayText(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao baixar texto: " + webRequest.error);
            }
            else
            {
                // Aqui você pode adicionar o texto baixado ao seu UI
                // Por exemplo, adicionando ao final da descrição atual
                descriptionText.text += "\n" + webRequest.downloadHandler.text;
            }
        }
    }

    private void PreviousPrice()
    {
        if (prices != null && currentIndex > 0)
        {
            currentIndex--;
            UpdatePriceDisplay();
        }
    }

    private void NextPrice()
    {
        if (prices != null && currentIndex < prices.Count - 1)
        {
            currentIndex++;
            UpdatePriceDisplay();
        }
    }
}