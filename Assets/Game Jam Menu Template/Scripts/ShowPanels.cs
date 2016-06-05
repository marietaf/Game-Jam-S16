using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 

    public bool pauseMenuDirty = true;
    public bool mainMenuDirty = false;

    UnityEngine.EventSystems.EventSystem myEventSystem;

    void Start()
    {
        myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
    }

    //void Update()
    //{
    //    Debug.Log(myEventSystem);
    //}
	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
        GameObject.Find("MenuPanel").GetComponent<CanvasGroup>().interactable = false;
        optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
        myEventSystem.SetSelectedGameObject(GameObject.Find("OptionsBack"));
    }

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
        GameObject.Find("MenuPanel").GetComponent<CanvasGroup>().interactable = true;
        myEventSystem.SetSelectedGameObject(GameObject.Find("Options"));
        optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
        menuPanel.SetActive (true);
        if (mainMenuDirty)
        {
            Debug.Log("Cleaning main menu");
            mainMenuDirty = false;
            myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        }
        myEventSystem.SetSelectedGameObject(GameObject.Find("Start"));
    }

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
        menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
        pausePanel.SetActive (true);
		optionsTint.SetActive(true);

        if (pauseMenuDirty)
        {
            pauseMenuDirty = false;
            myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
            myEventSystem.SetSelectedGameObject(GameObject.Find("Resume"));
        }
        else if (!myEventSystem.alreadySelecting)
        {
            myEventSystem.SetSelectedGameObject(GameObject.Find("Resume"));
        }
    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);
	}
}
