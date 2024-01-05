using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel; // Referência ao painel de informações
    public int sceneIndex; // Índice da cena para carregar
    private Button buttonComponent; // Componente Button do GameObject
    private Image buttonImage; // Componente Image do botão
    private Behaviour halo; // Componente Halo

    void Start()
    {
        buttonComponent = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        halo = (Behaviour)GetComponent("Halo");
        infoPanel.SetActive(false);

        // Adicionar EventTrigger ao infoPanel
        EventTrigger trigger = infoPanel.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { OnInfoPanelExit((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.SetActive(true);
        buttonComponent.interactable = false;
        if (buttonImage != null)
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0); // Torna o botão transparente
        if (halo != null)
            halo.enabled = false; // Desativa o Halo
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonComponent.interactable = true;
        if (buttonImage != null)
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1); // Restaura a opacidade
        if (halo != null)
            halo.enabled = true; // Reativa o Halo
    }

    public void OnInfoPanelExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
        if (buttonImage != null)
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1); // Restaura a opacidade
        if (halo != null)
            halo.enabled = true; // Reativa o Halo
    }

    public void LoadScene()
    {
        if (sceneIndex >= 0 && sceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Índice da cena inválido: " + sceneIndex);
        }
    }
}
