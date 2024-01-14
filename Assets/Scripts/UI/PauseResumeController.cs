using UnityEngine;
using UnityEngine.UI;

public class PauseResumeController : MonoBehaviour
{
    public GameObject pausePanel; // Referência ao painel de pausa
    public Button pauseResumeButton; // Referência ao botão de pausa/play
    public Sprite pauseSprite; // Sprite para o botão de pausa
    public Sprite playSprite; // Sprite para o botão de play
    private bool isPaused = false;

    // Referência que precisa persistir entre as cenas
    private AudioSource musicSource; // Referência ao componente AudioSource
    private OrbitCamera orbitCameraScript; // Referência ao script OrbitCamera
    private SceneLoadManager sceneLoadManager; // Referência ao SceneLoadManager

    private void Start()
    {
        // Obter as referências uma vez no início do jogo
        musicSource = BackgroundMusic.Instance.GetComponent<AudioSource>();
        orbitCameraScript = FindObjectOfType<OrbitCamera>();
        sceneLoadManager = FindObjectOfType<SceneLoadManager>();

        // Configurações iniciais
        pausePanel.SetActive(false);
        pauseResumeButton.image.sprite = pauseSprite;
    }

    // Método chamado quando o botão de pausa/play é clicado
    public void TogglePauseResume()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pausar o jogo
            musicSource.Pause(); // Pausar a música
            orbitCameraScript.enabled = false; // Desativar o script OrbitCamera
            sceneLoadManager.gameObject.SetActive(false); // Desativar o SceneLoadManager
        }
        else
        {
            Time.timeScale = 1; // Retomar o jogo
            musicSource.UnPause(); // Retomar a música
            orbitCameraScript.enabled = true; // Ativar o script OrbitCamera
            sceneLoadManager.gameObject.SetActive(true); // Ativar o SceneLoadManager
        }

        pausePanel.SetActive(isPaused);
        pauseResumeButton.image.sprite = isPaused ? playSprite : pauseSprite;
    }
}