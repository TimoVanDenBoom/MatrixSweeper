using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public int numberOfLevels = 120;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount = 0;
    private float sideMarginsPanel;
    private float topMarginPanel;
    public float sideMarginsIcon;
    private int maxInACol;
    
    // Start is called before the first frame update
    void Start()
    {
        maxInACol = 3;
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        sideMarginsPanel = panelDimensions.width / 5;
        topMarginPanel = 50;
        int maxInARow = Mathf.FloorToInt((panelDimensions.width - 2 * sideMarginsPanel) / (iconDimensions.width + 2*sideMarginsIcon));
        amountPerPage = maxInACol * maxInARow;
        //int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        //LoadPanels(totalPages);
        LoadPanels(4);
    }
    void LoadPanels(int numberOfPanels)
    {
        GameObject panelClone = Instantiate(levelHolder) as GameObject;

        for (int i = 1; i <= numberOfPanels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page " + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
            SetUpGrid(panel);
            int numberOfIcons = i == numberOfPanels ? numberOfLevels - currentLevelCount : amountPerPage;
            LoadIcons(numberOfIcons, panel, i);
            panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1) + "x" + (i + 1);
        }
        Destroy(panelClone);
    }
    void SetUpGrid(GameObject panel)
    {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.padding.left = Mathf.FloorToInt(sideMarginsPanel);
        grid.padding.right = Mathf.FloorToInt(sideMarginsPanel);
        grid.padding.top = Mathf.FloorToInt(topMarginPanel);
        grid.spacing = new Vector2(sideMarginsIcon, sideMarginsIcon);
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = maxInACol;
        grid.childAlignment = TextAnchor.MiddleCenter;
    }
    void LoadIcons(int numberOfIcons, GameObject parentObject, int panelNumber)
    {
        for (int i = 1; i <= numberOfIcons; i++)
        {
            currentLevelCount++;
            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level " + i;
            icon.transform.GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(i.ToString());
            Button b = icon.GetComponent<Button>();
            AddListener(b, 5 + i + (panelNumber - 1) * 30);
        }
    }

    void AddListener(Button b, int i)
    {
        b.onClick.AddListener(() => { LoadLevel(i); });
    }
    // Update is called once per frame
    void LoadLevel(int level)
    {
        GameObject.Find("LevelLoader").GetComponent<LevelLoaderScript>().LoadLvl(level);
    }
}
