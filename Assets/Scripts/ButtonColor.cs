using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    private Image im;
    // Start is called before the first frame update
    void Start()
    {
        im = transform.GetChild(0).GetComponent<Image>();
    }

    public void changeColorMouseOver()
    {
        im.color = new Color(0.8f, 0.8f, 0.8f);
        GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>().playMenuMouseOverSound();
    }
    public void changeColorMouseExit()
    {
        im.color = Color.white;
    }

    public void MouseDown()
    {
        im.color = Color.grey;
        GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffectsScript>().playMenuMouseDownSound();
    }

}
