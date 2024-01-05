using UnityEngine;

public class HornSoundTrigger : MonoBehaviour
{
    private bool hasPlayed = false;
    public AudioSource hornSound;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter chamado com: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Barco") && !hasPlayed)
        {
            hornSound.Play();
            hasPlayed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit chamado com: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Barco"))
        {
            hasPlayed = false;
        }
    }
}