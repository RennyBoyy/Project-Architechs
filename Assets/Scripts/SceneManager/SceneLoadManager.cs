using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        // Carrega a nova cena de forma assíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        // Espera até que a cena seja completamente carregada
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}