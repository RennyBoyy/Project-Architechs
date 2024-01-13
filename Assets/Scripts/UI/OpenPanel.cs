using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject panelToOpen;

    public void Open()
    {
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true); // Ativa o painel
        }
    }
}