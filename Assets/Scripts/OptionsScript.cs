using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    AudioScript aud;
    SoundEffectsScript soundFX;
    Slider musicSlider;
    Slider soundFXSlider;
    Toggle toggle;
    TMP_Dropdown dropdown;
    private static List<Resolution> resolutions;
    // Start is called before the first frame update
    
    
    void Start()
    {
        aud = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioScript>();
        soundFX = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>();
        musicSlider = transform.GetChild(1).GetComponent<Slider>();
        soundFXSlider = transform.GetChild(2).GetComponent<Slider>();
        dropdown = transform.GetChild(3).GetComponent<TMP_Dropdown>();
        toggle = transform.GetChild(4).GetComponent<Toggle>();

        musicSlider.SetValueWithoutNotify(AudioScript.musicVolume);
        soundFXSlider.SetValueWithoutNotify(SoundEffectsScript.soundEffectsVolume);
        toggle.isOn = Screen.fullScreen;

        resolutions = GetResolutions();
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionsIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionsIndex = i;
            }
        }
        dropdown.AddOptions(options);
        dropdown.value = currentResolutionsIndex;
        dropdown.RefreshShownValue();
    }

   
    public void setMusicVolume()
    {
        aud.setMusicVolume(musicSlider.value);
    }

    public void setSoundEffectsVolume()
    {
        soundFX.setSoundEffectsVolume(soundFXSlider.value);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    private List<Resolution> GetResolutions()
    {
        //Filters out all resolutions with low refresh rate:
        Resolution[] resolutions = Screen.resolutions;
        HashSet<Tuple<int, int>> uniqResolutions = new HashSet<Tuple<int, int>>();
        Dictionary<Tuple<int, int>, int> maxRefreshRates = new Dictionary<Tuple<int, int>, int>();
        for (int i = 0; i < resolutions.GetLength(0); i++)
        {
            //Add resolutions (if they are not already contained)
            if(resolutions[i].width >= 1024 && resolutions[i].height >= 768)
            {
                Tuple<int, int> resolution = new Tuple<int, int>(resolutions[i].width, resolutions[i].height);
                uniqResolutions.Add(resolution);
                //Get highest framerate:
                if (!maxRefreshRates.ContainsKey(resolution))
                {
                    maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
                }
                else
                {
                    maxRefreshRates[resolution] = resolutions[i].refreshRate;
                }
            } 
        }
        //Build resolution list:
        List<Resolution> uniqResolutionsList = new List<Resolution>(uniqResolutions.Count);
        foreach (Tuple<int, int> resolution in uniqResolutions)
        {
            Resolution newResolution = new Resolution();
            newResolution.width = resolution.Item1;
            newResolution.height = resolution.Item2;
            if (maxRefreshRates.TryGetValue(resolution, out int refreshRate))
            {
                newResolution.refreshRate = refreshRate;
            }
            uniqResolutionsList.Add(newResolution);
        }
        return uniqResolutionsList;
    }
}
