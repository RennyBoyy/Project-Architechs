using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class PoliciesRequestManager : MonoBehaviour
{
    public TextMeshProUGUI description;

    private string _apiBaseURL = "https://localhost:7120/Data/"; // Ajuste para corresponder à URL da sua API

    void Start()
    {
        GetAllGovernmentPolicies();
    }

    public void GetAllGovernmentPolicies()
    {
        StartCoroutine(GetGovernmentPoliciesCoroutine());
    }

    IEnumerator GetGovernmentPoliciesCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_apiBaseURL + "GovernmentPolicies"))
        {
            // Envie a requisição e aguarde a resposta
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao receber informações: " + webRequest.error);
            }
            else
            {
                // Deserializar a resposta JSON para uma lista de objetos Government_Policy
                List<Government_Policy> policies = JsonConvert.DeserializeObject<List<Government_Policy>>(webRequest.downloadHandler.text);

                // Embaralhar a lista
                ShuffleList(policies);

                // Começa a rotina para mostrar as descrições
                StartCoroutine(DisplayDescriptions(policies));
            }
        }
    }

    IEnumerator DisplayDescriptions(List<Government_Policy> policies)
    {
        while (true) // Loop infinito para continuar mostrando as descrições
        {
            foreach (Government_Policy policy in policies)
            {
                if (description != null)
                {
                    description.text = policy.Description;
                }
                else
                {
                    Debug.LogError("Componente TextMeshProUGUI não está atribuído!");
                    yield break; // Sai da corrotina se o TextMeshProUGUI não estiver atribuído
                }

                yield return new WaitForSeconds(3); // Espera por 3 segundos antes de mostrar a próxima descrição
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