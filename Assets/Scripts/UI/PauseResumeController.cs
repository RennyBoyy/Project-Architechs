using UnityEngine;
using UnityEngine.UI;

public class PauseResumeController : MonoBehaviour
{
    public GameObject pausePanel; // Refer�ncia ao painel de pausa
    public Button pauseResumeButton; // Refer�ncia ao bot�o de pausa/play
    public Sprite pauseSprite; // Sprite para o bot�o de pausa
    public Sprite playSprite; // Sprite para o bot�o de play
    private bool isPaused = false;

    // Refer�ncia que precisa persistir entre as cenas
    private AudioSource musicSource; // Refer�ncia ao componente AudioSource
    private OrbitCamera orbitCameraScript; // Refer�ncia ao script OrbitCamera
    private SceneLoadManager sceneLoadManager; // Refer�ncia ao SceneLoadManager

    private void Start()
    {
        // Obter as refer�ncias uma vez no in�cio do jogo
        musicSource = BackgroundMusic.Instance.GetComponent<AudioSource>();
        orbitCameraScript = FindObjectOfType<OrbitCamera>();
        sceneLoadManager = FindObjectOfType<SceneLoadManager>();

        // Configura��es iniciais
        pausePanel.SetActive(false);
        pauseResumeButton.image.sprite = pauseSprite;
    }

    // M�todo chamado quando o bot�o de pausa/play � clicado
    public void TogglePauseResume()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pausar o jogo
            musicSource.Pause(); // Pausar a m�sica
            orbitCameraScript.enabled = false; // Desativar o script OrbitCamera
            sceneLoadManager.gameObject.SetActive(false); // Desativar o SceneLoadManager
        }
        else
        {
            Time.timeScale = 1; // Retomar o jogo
            musicSource.UnPause(); // Retomar a m�sica
            orbitCameraScript.enabled = true; // Ativar o script OrbitCamera
            sceneLoadManager.gameObject.SetActive(true); // Ativar o SceneLoadManager
        }

        pausePanel.SetActive(isPaused);
        pauseResumeButton.image.sprite = isPaused ? playSprite : pauseSprite;
    }
}