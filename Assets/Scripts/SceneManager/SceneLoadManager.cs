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
        // Carrega a nova cena de forma ass�ncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        // Espera at� que a cena seja completamente carregada
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}