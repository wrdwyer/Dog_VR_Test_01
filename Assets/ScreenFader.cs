using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
    {
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    private void Start()
        {
        // Ensure the image is fully transparent at the start
        if (fadeImage != null)
            {
            fadeImage.color = new Color(0, 0, 0, 0);
            }
        }

    public IEnumerator FadeIn()
        {
        yield return StartCoroutine(Fade(1));
        }

    public IEnumerator FadeOut()
        {
        yield return StartCoroutine(Fade(0));
        }

    private IEnumerator Fade(float targetAlpha)
        {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
            {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, newAlpha);
            yield return null;
            }

        // Ensure the final alpha is set correctly
        fadeImage.color = new Color(0, 0, 0, targetAlpha);
        }
    }
