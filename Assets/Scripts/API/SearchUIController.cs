using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SearchUIController : MonoBehaviour
{
    public TMP_InputField searchText;
    public TMP_InputField searchImage;
    public ArquivoSearchManager searchManager;
    public Button submit;

    public void OnSearchTextSubmit()
    {
        string query = searchText.text;
        searchManager.SearchText(query);
    }

    public void OnSearchImageSubmit()
    {
        string query = searchImage.text;
        searchManager.SearchImage(query);
    }

    public void SetupButtonListeners(Button searchTextButton, Button searchImageButton)
    {
        searchTextButton.onClick.AddListener(OnSearchTextSubmit);
        searchImageButton.onClick.AddListener(OnSearchImageSubmit);
    }
}