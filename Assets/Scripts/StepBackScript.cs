using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StepBackScript : MonoBehaviour
{

    private AudioSource mouseOverAudio;
    private AudioSource mouseDownAudio;
    public AudioClip mouseOverClip;
    public AudioClip mouseDownClip;

    private SpriteRenderer spriteRenderer;
    private Transform multiplyPanel;
    private Transform victoryPanel;
    private Transform menu;
    private GameObject canvas;
    private MatrixScript matrixScript;
    // Start is called before the first frame update
    void Start()
    {
        mouseOverAudio = AddAudio(false, false, 0.5f, mouseOverClip);
        mouseDownAudio = AddAudio(false, false, 0.5f, mouseDownClip);

        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        multiplyPanel = canvas.transform.GetChild(0);
        victoryPanel = canvas.transform.GetChild(1);
        menu = canvas.transform.GetChild(2);
        matrixScript = GameObject.FindGameObjectWithTag("Matrix").GetComponent<MatrixScript>();
    }

    private void OnMouseUp()
    {
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            mouseDownAudio.Play();
            spriteRenderer.material.color = Color.grey;
        }
    }

    private void OnMouseOver()
    {
        if (!multiplyPanel.gameObject.activeSelf && !Input.GetMouseButton(0) && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf)
        {
            spriteRenderer.material.color = new Color(0.8f, 0.8f, 0.8f);
        }
    }
    private void OnMouseExit()
    {
        spriteRenderer.material.color = Color.white;
    }
    private void OnMouseEnter()
    {
        if (!multiplyPanel.gameObject.activeSelf && !Input.GetMouseButton(0)
            && !victoryPanel.gameObject.activeSelf && !menu.gameObject.activeSelf)
        {
            mouseOverAudio.Play();
        }
    }


    public AudioSource AddAudio(bool loop, bool playAwake, float vol, AudioClip clip)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }
}
