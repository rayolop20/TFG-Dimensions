using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stOpEx;
    public GameObject options;
    public GameObject instructions;

    bool optionsActive = true;
    bool menuActive = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {

        SceneManager.LoadScene("Level 1");
    }
    public void Instructions() {

        instructions.SetActive(true);
    }
    
    public void ExitGame() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       
        Application.Quit();
#endif
    }


    public void Level2() {

        SceneManager.LoadScene("Level 2");
    }
    public void Level3() {

        SceneManager.LoadScene("Level 3");
    }
    
    public void ReturnMainMenu() {

        SceneManager.LoadScene("Main Menu");
    }
    
    public void Close() {

        options.SetActive(false);
    }

    
    
    public void OptionsOpen() {

        optionsActive = !optionsActive;
        menuActive = !menuActive;

        stOpEx.SetActive(optionsActive);
        options.SetActive(menuActive);
    }   





}
