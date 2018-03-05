using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class BgLoader : MonoBehaviour {

    [SerializeField] private Image bg_image;
    [SerializeField] private LangSystem ls;

    private string config_json;
    private string current_bg;

    void Awake()
    {
        ls.CheckConfig();
            current_bg = LangSystem.cnfg.current_bg;
            PlayerPrefs.SetString("current_bg", current_bg);
    }
    void Start () {
        print(LangSystem.cnfg.current_bg);
        bg_image.sprite = Resources.Load("Sprites/" + PlayerPrefs.GetString("current_bg"), typeof(Sprite)) as Sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
