using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // The button to attach the click event to
    public Button button;

    void Start()
    {
        // Add a listener to the button's click event
        button.onClick.AddListener(Reload);
    }

    void Reload()
    {
        // Get the current scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}