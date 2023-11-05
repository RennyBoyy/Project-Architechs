using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Color Settings")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Color pressedColor = Color.black;
    private Image buttonImage;
    private Vector3 originalScale;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    public Vector3 pressedScale = new Vector3(0.9f, 0.9f, 0.9f);

    // Duration for the scaling animation
    public float duration = 0.2f;

    void Start()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(pressedScale));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(hoverScale));
    }

    IEnumerator ScaleButton(Vector3 targetScale)
    {
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
    IEnumerator TweenColor(Color targetColor)
    {
        float time = 0;
        Color startColor = buttonImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            buttonImage.color = Color.Lerp(startColor, targetColor, time / duration);
            yield return null;
        }

        buttonImage.color = targetColor;
    }

}