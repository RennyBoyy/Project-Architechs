using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    public OrbitCamera orbitCameraScript;  // Refer�ncia ao script OrbitCamera
    public GameObject sceneLoadManager;    // Refer�ncia ao GameObject SceneLoadManager

    void OnEnable()
    {
        // Desativa o script OrbitCamera e o GameObject SceneLoadManager quando o SettingsPanel � ativado
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
        // Reativa o script OrbitCamera e o GameObject SceneLoadManager quando o SettingsPanel � desativado
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