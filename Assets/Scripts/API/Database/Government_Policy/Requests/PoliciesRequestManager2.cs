using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class PoliciesRequestManager2 : MonoBehaviour
{
    public TextMeshProUGUI policyText;
    public Button previousButton;
    public Button nextButton;

    private List<Government_Policy> policies;
    private int currentIndex = 0;

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para corresponder à URL da sua API

    void Start()
    {
        GetAllGovernmentPolicies();
        if (previousButton != null) previousButton.onClick.AddListener(PreviousPolicy);
        if (nextButton != null) nextButton.onClick.AddListener(NextPolicy);
    }

    public void GetAllGovernmentPolicies()
    {
        StartCoroutine(GetGovernmentPoliciesCoroutine());
    }

    IEnumerator GetGovernmentPoliciesCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "GovernmentPolicies"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informações: " + webRequest.error);
            }
            else
            {
                policies = JsonConvert.DeserializeObject<List<Government_Policy>>(webRequest.downloadHandler.text);
                UpdatePolicyDisplay();
            }
        }
    }

    private void UpdatePolicyDisplay()
    {
        if (policies != null && policies.Count > 0 && policyText != null)
        {
            policyText.text = policies[currentIndex].Description;
            StartCoroutine(DownloadAndDisplayText(policies[currentIndex].LinkExtractedText));
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
                policyText.text += "\n" + webRequest.downloadHandler.text;
            }
        }
    }

    private void PreviousPolicy()
    {
        if (policies != null && currentIndex > 0)
        {
            currentIndex--;
            UpdatePolicyDisplay();
        }
    }

    private void NextPolicy()
    {
        if (policies != null && currentIndex < policies.Count - 1)
        {
            currentIndex++;
            UpdatePolicyDisplay();
        }
    }
}