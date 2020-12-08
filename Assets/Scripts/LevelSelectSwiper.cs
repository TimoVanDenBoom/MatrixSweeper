using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectSwiper : MonoBehaviour
{
    List<GameObject> panels;
    private bool arrayFilled;
    private Vector3 cameraPosition;
    private static bool isSwiping = false;
    private static int currentPage;
    // Start is called before the first frame update
    private void Start()
    {
        arrayFilled = false;
        panels = new List<GameObject>();
        currentPage = 1;
    }
    void Update()
    {
        if(GameObject.Find("MasterPanel").transform.childCount > 1 && !arrayFilled) {
            panels.Add(GameObject.Find("MasterPanel").transform.GetChild(1).gameObject);
            panels.Add(GameObject.Find("MasterPanel").transform.GetChild(2).gameObject);
            panels.Add(GameObject.Find("MasterPanel").transform.GetChild(3).gameObject);
            panels.Add(GameObject.Find("MasterPanel").transform.GetChild(4).gameObject);
            arrayFilled = true;
            cameraPosition = new Vector3(GameObject.FindGameObjectWithTag("MainCamera").transform.position.x, GameObject.FindGameObjectWithTag("MainCamera").transform.position.y);
            Destroy(GameObject.Find("MasterPanel").transform.GetChild(0).gameObject);
            
        }

        if (currentPage == 4)
        {
            GameObject.Find("RightButton").transform.GetChild(0).transform.GetComponent<Image>().color = Color.grey;
        }
        else if (GameObject.Find("RightButton").transform.GetChild(0).transform.GetComponent<Image>().color == Color.grey)
        {
            GameObject.Find("RightButton").transform.GetChild(0).transform.GetComponent<Image>().color = Color.white;
        }

        if (currentPage == 1)
        {
            GameObject.Find("LeftButton").transform.GetChild(0).transform.GetComponent<Image>().color = Color.grey;
        }
        else if(GameObject.Find("LeftButton").transform.GetChild(0).transform.GetComponent<Image>().color == Color.grey)
        {
            GameObject.Find("LeftButton").transform.GetChild(0).transform.GetComponent<Image>().color = Color.white;
        }
    }

    public void SwipeLeft()
    {
        if (currentPage != 4 && !isSwiping)
        {
            isSwiping = true;
            currentPage++;
            int i = 0;
            foreach (GameObject panel in panels)
            {
                if(i == 0)
                {
                    StartCoroutine(Swipe(panel.transform.position, new Vector3(panel.transform.position.x - Screen.width, panel.transform.position.y), 1, panel));
                }
                else
                {
                    StartCoroutine(Swipe(panel.transform.position, new Vector3(panels[i - 1].transform.position.x, panel.transform.position.y), 1, panel));
                }
               
                i++;
            }
        }
    }
    public void SwipeRight()
    {
        if (currentPage != 1 && !isSwiping)
        {
            currentPage--;
            isSwiping = true;
            int i = 0;
            foreach (GameObject panel in panels)
            {
                if (i == 3)
                {
                    StartCoroutine(Swipe(panel.transform.position, new Vector3(panel.transform.position.x + Screen.width, panel.transform.position.y), 1, panel));
                }
                else
                {
                    StartCoroutine(Swipe(panel.transform.position, new Vector3(panels[i + 1].transform.position.x, panel.transform.position.y), 1, panel));
                }
                i++;
            }
        }
    }

    IEnumerator Swipe(Vector3 startpos, Vector3 endpos, float seconds, GameObject panel)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            panel.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        isSwiping = false;
    }

    public void changeColorToWhite()
    {
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }
    public void changeColorMouseOver()
    {
        transform.GetChild(0).transform.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
    }
}
