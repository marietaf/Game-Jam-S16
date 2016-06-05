using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 

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
        Debug.Log("Show Menu");
        menuPanel.SetActive (true);
        myEventSystem.SetSelectedGameObject(GameObject.Find("Start"));
    }

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
        Debug.Log("Hide Menu");
        menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
        Debug.Log(GameObject.Find("Resume"));
        myEventSystem.firstSelectedGameObject = GameObject.Find("Resume");
        myEventSystem.SetSelectedGameObject(GameObject.Find("Resume"));
    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}
}
