using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using DogVR;
using System;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;
using Sirenix.OdinInspector;

public class SceneFader : MonoBehaviour
    {
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    private FadeTunnelingVignette fadeTunnelingVignette;
    private Renderer _renderer;
    public bool isFarmTruck = false;

    private void Start()
        {
        // Ensure the image is fully transparent at the start
        if (fadeImage != null)
            {

            fadeImage.color = new Color(0, 0, 0, 0);
            }
        if (fadeTunnelingVignette == null)
            {
            fadeTunnelingVignette = GameManager.Instance.playerGameObjectSO.persistentObject.GetComponentInChildren<FadeTunnelingVignette>();
            _renderer = fadeTunnelingVignette.GetComponent<Renderer>();
            _renderer.sharedMaterial.SetFloat("_ApertureSize", 1f);
            }
        }
    [Button("FadeInDrive")]
    public void SetVignetteForDrive()
        {
        _renderer.sharedMaterial.SetFloat("_ApertureSize", 0.8f);
        }

    [Button("FadeOutDrive")]
    public void SetVignetteForDriveStop()
        {
        _renderer.sharedMaterial.SetFloat("_ApertureSize", 1f);
        }

    [Button ("FadeIn")]
    public IEnumerator FadeIn()
        {
        //yield return StartCoroutine(Fade(fadeDuration));
        yield return StartCoroutine(FadeInVignette(fadeDuration));
        }
    [Button("FadeIn")]
    public IEnumerator FadeOut()
        {
       
        //yield return StartCoroutine(Fade(0));
        yield return StartCoroutine(FadeOutVignette(fadeDuration));
        }

    public IEnumerator FadeInVignette(float fadeDuration)
        {
        float elapsedTime = 0f;
        float startValue = 1f;
        float endValue = 0f;
          
        while (elapsedTime < fadeDuration)
            {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / fadeDuration);
            Debug.Log(newValue);           
            _renderer.sharedMaterial.SetFloat("_ApertureSize", newValue);
            //gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_YourParameter", someValue);
            // Set your vignette opacity to newValue here
            yield return null;
            }
        }

    public IEnumerator FadeOutVignette(float fadeDuration)
        {
        float elapsedTime = 0f;
        float startValue = 0f;
        float endValue = 1f;

        while (elapsedTime < fadeDuration)
            {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / fadeDuration);
            Debug.Log(newValue);
            _renderer.sharedMaterial.SetFloat("_ApertureSize", newValue);
            //gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_YourParameter", someValue);
            // Set your vignette opacity to newValue here
            if (isFarmTruck)
                {
                SetVignetteForDrive();
                }
            yield return null;
            }
        }

 

  

    public IEnumerator Fade(float targetAlpha)
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
