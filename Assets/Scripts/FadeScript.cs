using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    SoundEffectsScript soundFX;
    public static bool isTransitioning = false;
    public void Start()
    {
        soundFX = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>();
    }
     
    // Start is called before the first frame update
    public void FadeOut()
    {
        isTransitioning = true;
        StartCoroutine(DoFadeOut()); 
    }

    public void FadeIn()
    {
        transform.gameObject.SetActive(true);
        StartCoroutine(DoFadeIn());
       
    }

    IEnumerator DoFadeOut()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        soundFX.setSpecificSoundEffectsVolume(0, 1);
        while (isTransitioning)
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }   
        canvasGroup.alpha = 0;
        transform.gameObject.SetActive(false);
    }

    IEnumerator DoFadeIn()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * 2;
            yield return null;
        }
        soundFX.setSpecificSoundEffectsVolume(SoundEffectsScript.soundEffectsVolume, 1);
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        isTransitioning = false;

    }
}
