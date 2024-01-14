using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    private AudioSource musicSource;  // Referência dinâmica ao AudioSource para música
    public Slider musicSlider;       // Referência ao slider de volume da música
    public AudioSource sfxSource;
    public Slider sfxSlider;

    void Start()
    {
        // Obter a instância atual de BackgroundMusic e seu AudioSource
        if (BackgroundMusic.Instance != null)
        {
            musicSource = BackgroundMusic.Instance.GetComponent<AudioSource>();
        }

        // Inicializa o slider com o volume atual, se o AudioSource estiver disponível
        if (musicSource != null && musicSlider != null)
        {
            musicSlider.value = musicSource.volume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        // Inicializa o slider de efeitos sonoros com o volume atual
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxSource.volume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        // Ajusta o volume da música se o AudioSource estiver disponível
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        // Ajusta o volume dos efeitos sonoros
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
        }
    }
}