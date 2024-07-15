using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DogVR;


public class UpdaObjectiveUI : MonoBehaviour
    {
    public TMP_Text objectiveText;
    public Toggle Toggle;
    public Image objectiveImage;
    public float fadeDuration = 2f; // Duration of the fade effect
    public float waitOnScreen = 2f;

    private void Start()
        {
        UpdateObjective();
        }

    private void OnEnable()
        {
        UpdateObjective();
        StartCoroutine(FadeObjectiveText());
        }

    public void UpdateObjective()
        {
        objectiveText.text = GameManager.Instance.currentObjectiveSO.ObjectiveName;
        Debug.Log(GameManager.Instance.currentObjectiveSO.ObjectiveName);
        if (GameManager.Instance.currentObjectiveSO.objectiveComplete)
            {
            Toggle.isOn = true;
            }
        else
            {
            Toggle.isOn = false;
            }
        }

    private IEnumerator FadeObjectiveText()
        {
        yield return FadeText(true); // Fade in

        yield return new WaitForSeconds(waitOnScreen); // Wait for 3 seconds

        yield return FadeText(false); // Fade out
        gameObject.SetActive(false);
        }

    private IEnumerator FadeText(bool fadeIn)
        {
        Color originalColor = objectiveText.color;
        Color originalColor2 = objectiveImage.color;
        float timer = 0f;

        while (timer < fadeDuration)
            {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            if (!fadeIn) alpha = 1 - alpha; // Reverse alpha for fade out
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            Color newColor2 = new Color(originalColor2.r, originalColor2.g, originalColor2.b, alpha);
            objectiveText.color = newColor;
            objectiveImage.color = newColor2;
            yield return null;
            }

        // Ensure the text is fully visible or invisible at the end
        objectiveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeIn ? 1f : 0f);
        objectiveImage.color = new Color(originalColor2.r, originalColor2.g, originalColor2.b, fadeIn ? 1f : 0f);

        }
    }

