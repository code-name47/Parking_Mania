using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableStars : MonoBehaviour
{
    public GameObject[] stars = new GameObject[3];
    public int starcount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (starcount > 0)
        //{
            for (int i = 0; i < 3; i++)
            {
                if (i < starcount)
                {
                    stars[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    stars[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        //}
    }
}
