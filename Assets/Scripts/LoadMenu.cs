using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitFewSecond());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator WaitFewSecond()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
}
