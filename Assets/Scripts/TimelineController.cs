using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector director; // Reference to the PlayableDirector
    public GameObject canvasToDeactivate; // Reference to the Canvas or Panel you want to deactivate

    void Start()
    {
        // Get the Button component and add a listener for the onClick event
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // Play the timeline associated with the PlayableDirector
        if (director != null)
        {
            director.Play();
        }

        // Deactivate the canvas or UI panel
        if (canvasToDeactivate != null)
        {
            canvasToDeactivate.SetActive(false);
        }
    }
}