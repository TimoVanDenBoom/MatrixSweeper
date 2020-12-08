using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void Load2x2()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void Load3x3()
    {
        StartCoroutine(LoadLevel(2));
    }
    public void Load4x4()
    {
        StartCoroutine(LoadLevel(3));
    }
    public void Load5x5()
    {
        StartCoroutine(LoadLevel(4));
    }

    public void LoadNext()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLvl(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public void LoadNextWithMusic()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        float startVolume = AudioListener.volume;
        while (AudioListener.volume > 0)
        {
            AudioListener.volume -= startVolume * Time.deltaTime / transitionTime;

            yield return null;
        }
        AudioListener.volume = startVolume;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioScript>().GetCurrentMusicPlaying()[0].Stop();
       



        SceneManager.LoadScene(levelIndex);
    }
}
