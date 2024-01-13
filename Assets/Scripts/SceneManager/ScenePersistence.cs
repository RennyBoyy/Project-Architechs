using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScenePersistence : MonoBehaviour
{
    [System.Serializable]
    public class SceneObject
    {
        public GameObject gameObject;
        public string sceneName;
    }

    public List<SceneObject> sceneObjects;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var sceneObject in sceneObjects)
        {
            if (sceneObject.gameObject != null)
            {
                sceneObject.gameObject.SetActive(sceneObject.sceneName == scene.name);
            }
        }
    }
}
