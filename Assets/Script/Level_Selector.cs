using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Selector : MonoBehaviour
{
    public GameObject LevelHolder;
    public GameObject LevelIcon;
    public int NoOfLevels;
    // Start is called before the first frame update
    void Start()
    {
        Rect panelDimensions = LevelHolder.GetComponent<RectTransform>().rect;
        Rect iconDimensions = LevelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt(panelDimensions.width / iconDimensions.width);
        int maxInACol = Mathf.FloorToInt(panelDimensions.height / iconDimensions.height);
        int amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)NoOfLevels / amountPerPage);
        LoadPanels(totalPages);
    }

    void LoadPanels(int numberOfPanels)
    {
        Debug.Log("no of panels " + numberOfPanels);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
