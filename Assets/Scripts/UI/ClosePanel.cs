using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public GameObject panelToClose; 
    public void Close()
    {
        if (panelToClose != null)
        {
            panelToClose.SetActive(false); // Desativa o painel
        }
    }
}