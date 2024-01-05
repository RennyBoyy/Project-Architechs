using UnityEngine;
using UnityEngine.EventSystems; // Adicionar este namespace para eventos de UI

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel; // Referência ao painel de informações
    public string sceneToLoad; // Nome da cena para carregar
    public GameObject areaButton;

    private bool isMouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        areaButton.SetActive(false);
        infoPanel.SetActive(true);
        {
            if (!(isMouseOver = false))
            {
                return;
            }
            areaButton.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        infoPanel.SetActive(false); // Desativar o painel de informações
    }

}