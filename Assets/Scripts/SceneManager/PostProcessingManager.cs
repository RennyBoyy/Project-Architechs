using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetPostProcessing()
    {
        var postProcessVolumes = FindObjectsOfType<PostProcessVolume>();
        foreach (var volume in postProcessVolumes)
        {
            volume.isGlobal = false;
            volume.enabled = false;
        }

        var postProcessLayers = FindObjectsOfType<PostProcessLayer>();
        foreach (var layer in postProcessLayers)
        {
            layer.enabled = false;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPostProcessing();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}