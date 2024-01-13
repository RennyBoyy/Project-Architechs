using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class SentimentRequestManager2 : MonoBehaviour
{
    public TextMeshProUGUI sentimentText;
    public Button previousButton;
    public Button nextButton;

    private List<Public_Sentiment> sentiments;
    private int currentIndex = 0;

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para corresponder à URL da sua API

    void Start()
    {
        GetAllPublicSentiments();
        if (previousButton != null) previousButton.onClick.AddListener(PreviousSentiment);
        if (nextButton != null) nextButton.onClick.AddListener(NextSentiment);
    }

    public void GetAllPublicSentiments()
    {
        StartCoroutine(GetPublicSentimentsCoroutine());
    }

    IEnumerator GetPublicSentimentsCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "PublicSentiment"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informações: " + webRequest.error);
            }
            else
            {
                sentiments = JsonConvert.DeserializeObject<List<Public_Sentiment>>(webRequest.downloadHandler.text);
                UpdateSentimentDisplay();
            }
        }
    }

    private void UpdateSentimentDisplay()
    {
        if (sentiments != null && sentiments.Count > 0 && sentimentText != null)
        {
            sentimentText.text = sentiments[currentIndex].Comments;
            StartCoroutine(DownloadAndDisplayText(sentiments[currentIndex].LinkExtractedText));
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
                sentimentText.text += "\n" + webRequest.downloadHandler.text;
            }
        }
    }

    private void PreviousSentiment()
    {
        if (sentiments != null && currentIndex > 0)
        {
            currentIndex--;
            UpdateSentimentDisplay();
        }
    }

    private void NextSentiment()
    {
        if (sentiments != null && currentIndex < sentiments.Count - 1)
        {
            currentIndex++;
            UpdateSentimentDisplay();
        }
    }
}