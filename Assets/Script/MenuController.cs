using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    SceneManager StartMenu;
    public GameObject[] Stars = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
       // if () { }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick_Start() {
        SceneManager.LoadScene(2);
    }

    public void Onclick_NextLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void Onclick_TryAgain()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void Onclick_Exit()
    {
        Application.Quit();
    }
}
