using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ArquivoSearchManager : MonoBehaviour
{
    public TMP_Text resultsDisplay;
    public Transform imagesParent;
    public GameObject imagePrefab;

    private Queue<IEnumerator> downloadQueue = new Queue<IEnumerator>();
    private bool isProcessingQueue = false;

    public void SearchText(string query)
    {
        string url = "https://arquivo.pt/textsearch?q=" + UnityWebRequest.EscapeURL(query);
        StartCoroutine(SearchCoroutine(url, false));
    }

    public void SearchImage(string query)
    {
        string url = "https://arquivo.pt/imagesearch?q=" + UnityWebRequest.EscapeURL(query) + "&maxItems=10";
        StartCoroutine(SearchCoroutine(url, true));
    }

    private IEnumerator SearchCoroutine(string url, bool isImageSearch)
    {
        Debug.Log($"Iniciando busca: {url}");
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Erro na requisição: {request.error}");
                resultsDisplay.text = "Erro: " + request.error;
            }
            else
            {
                Debug.Log("Resposta recebida com sucesso.");
                var jsonResponse = JSON.Parse(request.downloadHandler.text);
                var items = jsonResponse["response_items"];
                int maxResults = 10;
                string displayText = "";

                ClearImagesAndQueue();

                for (int i = 0; i < maxResults && i < items.Count; i++)
                {
                    var title = items[i]["title"].Value;
                    var pageUrl = items[i]["link_to_archive"].Value;
                    var snippet = items[i]["snippet"].Value;
                    displayText += $"Título: {title}\nURL da Página: {pageUrl}\nResumo: {snippet}\n\n";

                    if (isImageSearch && !string.IsNullOrEmpty(items[i]["link_to_image"].Value))
                    {
                        downloadQueue.Enqueue(DownloadImage(items[i]["link_to_image"].Value, i));
                    }
                }

                resultsDisplay.text = displayText;
                ProcessQueue();
            }
        }
    }

    IEnumerator DownloadImage(string imageUrl, int index)
    {
        Debug.Log($"Iniciando download da imagem: {imageUrl}");
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            if (texture != null)
            {
                Debug.Log($"Imagem baixada com sucesso. Tamanho: {texture.width}x{texture.height}");
                GameObject imageObject = Instantiate(imagePrefab, imagesParent);
                imageObject.SetActive(true);

                RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, -index * 100); // Ajuste conforme necessário

                Image imageComponent = imageObject.GetComponent<Image>();
                imageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogError("Erro: Textura recebida é nula.");
            }
        }
        else
        {
            Debug.LogError($"Erro ao baixar a imagem: {request.error}");
        }

        ProcessQueue();
    }

    void ClearImagesAndQueue()
    {
        foreach (Transform child in imagesParent)
        {
            Destroy(child.gameObject);
        }
        downloadQueue.Clear();
        isProcessingQueue = false;
    }

    void ProcessQueue()
    {
        if (isProcessingQueue || downloadQueue.Count == 0) return;
        isProcessingQueue = true;
        StartCoroutine(downloadQueue.Dequeue());
    }

    void Update()
    {
        if (!isProcessingQueue && downloadQueue.Count > 0)
        {
            ProcessQueue();
        }
    }
}