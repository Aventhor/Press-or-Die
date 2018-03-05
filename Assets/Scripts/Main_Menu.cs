using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour {
    [SerializeField] private Text start_game;


    // Use this for initialization
    void Start () {
        start_game.text = LangSystem.lng.loc_menu[0];
    }
	
	// Update is called once per frame
	void Update () {
       
	}
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void MoveToShopMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Shop");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
