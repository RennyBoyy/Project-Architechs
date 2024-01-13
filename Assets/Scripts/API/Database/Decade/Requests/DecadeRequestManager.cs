using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI; // Importante para usar o componente Button

public class DecadeRequestManager : MonoBehaviour
{
    public TextMeshProUGUI decadeText; // Texto para exibir a década atual
    public Button previousButton; // Botão para a década anterior
    public Button nextButton; // Botão para a próxima década

    private List<Decade> decades; // Lista de décadas
    private int currentDecadeIndex; // Índice da década atual na lista

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para corresponder à URL da sua API

    void Start()
    {
        GetAllDecades();
        if (previousButton != null) previousButton.onClick.AddListener(PreviousDecade);
        if (nextButton != null) nextButton.onClick.AddListener(NextDecade);
    }

    public void GetAllDecades()
    {
        StartCoroutine(GetDecadesCoroutine());
    }

    IEnumerator GetDecadesCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "Decade"))
        {
            // Envie a requisição e aguarde a resposta
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informações: " + webRequest.error);
            }
            else
            {
                // Deserializar a resposta JSON para uma lista de objetos Decade
                decades = JsonConvert.DeserializeObject<List<Decade>>(webRequest.downloadHandler.text);
                if (decades != null && decades.Count > 0)
                {
                    currentDecadeIndex = 0;
                    UpdateDecadeDisplay();
                }
            }
        }
    }

    private void PreviousDecade()
    {
        if (decades == null || decades.Count == 0) return;
        if (currentDecadeIndex > 0)
        {
            currentDecadeIndex--;
            UpdateDecadeDisplay();
        }
    }

    private void NextDecade()
    {
        if (decades == null || decades.Count == 0) return;
        if (currentDecadeIndex < decades.Count - 1)
        {
            currentDecadeIndex++;
            UpdateDecadeDisplay();
        }
    }

    private void UpdateDecadeDisplay()
    {
        if (decadeText != null)
        {
            decadeText.text = "Decade: " + decades[currentDecadeIndex].DecadeValue.ToString();
        }
    }
}