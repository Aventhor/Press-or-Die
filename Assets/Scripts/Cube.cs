using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Cube : MonoBehaviour {

    [SerializeField] private LangSystem ls;
    public GameObject cube2d;
    private bool changeAnim = false;
    string config_json;
    string current_skin;

    void Awake()
    {
        ls.CheckConfig();
            current_skin = LangSystem.cnfg.current_skin;
            PlayerPrefs.SetString("current_skin", current_skin);
    }
	void Start () {
        Generate();
        print(LangSystem.cnfg.current_skin);
        cube2d.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + PlayerPrefs.GetString("current_skin"), typeof(Sprite)) as Sprite;
    }
	
	void Update () {
		
	}
    private void Generate()
    {
        cube2d = Instantiate(cube2d, new Vector3(1, ((float)(-3.55)), 1), Quaternion.identity) as GameObject;
    }
    public void PlayAnimation()
    {
        if (!changeAnim)
        {
            if ((cube2d.GetComponent<Animation>().IsPlaying("rightanim")) || (cube2d.GetComponent<Animation>().IsPlaying("leftanim")))
            {
                cube2d.GetComponent<Animation>().Stop("rightanim");
                cube2d.GetComponent<Animation>().Stop("leftanim");
                cube2d.transform.position = new Vector3(1, (float)(-3.65), 1);
                cube2d.GetComponent<Animation>().Play("leftanim");
            }
            if ((!cube2d.GetComponent<Animation>().IsPlaying("leftanim")) && (!cube2d.GetComponent<Animation>().IsPlaying("rightanim")))
            { 
            cube2d.GetComponent<Animation>().Play("leftanim");
            }
        }
        else
        {
            if ((cube2d.GetComponent<Animation>().IsPlaying("leftanim")) || (cube2d.GetComponent<Animation>().IsPlaying("rightanim")))
            {
                cube2d.GetComponent<Animation>().Stop("leftanim");
                cube2d.GetComponent<Animation>().Stop("rightanim");
                cube2d.transform.position = new Vector3(-1, (float)(-3.65), 1);
                cube2d.GetComponent<Animation>().Play("rightanim");
            }
            if ((!cube2d.GetComponent<Animation>().IsPlaying("rightanim")) && (!cube2d.GetComponent<Animation>().IsPlaying("leftanim")))
            {
                cube2d.GetComponent<Animation>().Play("rightanim");
            }    
        }
        changeAnim = !changeAnim;
    }
}
