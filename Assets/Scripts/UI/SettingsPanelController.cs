using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    public OrbitCamera orbitCameraScript;  // Referência ao script OrbitCamera
    public GameObject sceneLoadManager;    // Referência ao GameObject SceneLoadManager

    void OnEnable()
    {
        // Desativa o script OrbitCamera e o GameObject SceneLoadManager quando o SettingsPanel é ativado
        if (orbitCameraScript != null)
        {
            orbitCameraScript.enabled = false;
        }

        if (sceneLoadManager != null)
        {
            sceneLoadManager.SetActive(false);
        }
    }

    void OnDisable()
    {
        // Reativa o script OrbitCamera e o GameObject SceneLoadManager quando o SettingsPanel é desativado
        if (orbitCameraScript != null)
        {
            orbitCameraScript.enabled = true;
        }

        if (sceneLoadManager != null)
        {
            sceneLoadManager.SetActive(true);
        }
    }
}