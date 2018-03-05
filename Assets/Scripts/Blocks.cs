using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Blocks : MonoBehaviour {

    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject container1;
    [SerializeField] private GameObject container2;
    [SerializeField] private GameObject container3;
    [SerializeField] private GameObject container4;
    [SerializeField] private GameObject zone;
    [SerializeField] private LangSystem ls;
    private GameObject o1;
    private GameObject o2;
    private bool isBusy;
    private bool isBusy2;
    private bool isUse;
    private string current_platform;
    private string config_json;

    // Use this for initialization
    void Awake() {
            ls.CheckConfig();
            current_platform = LangSystem.cnfg.current_platform;
            PlayerPrefs.SetString("current_platform", current_platform);
    }
    void Start() {
        Generate();
        o1.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + PlayerPrefs.GetString("current_platform"), typeof(Sprite)) as Sprite;
        o2.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + PlayerPrefs.GetString("current_platform"), typeof(Sprite)) as Sprite;
        print(LangSystem.cnfg.current_platform);
    }

    // Update is called once per frame
    void Update() {
        CheckCount();
        if (isUse)
            DestroyBlocks();
    }

    public void Generate()
    {
        if ((container1.transform.childCount < 1) && (!isBusy))
        {
            o1 = Instantiate(platform, new Vector3(1, -4, 1), Quaternion.identity) as GameObject;
            o1.transform.SetParent(container1.transform);
            isBusy = !isBusy;
            o1.transform.position = new Vector3(1, -4, 1);
        } 
        else if (isBusy)
        {
            o1 = Instantiate(platform, new Vector3(1, -3, 1), Quaternion.identity) as GameObject;
            o1.transform.SetParent(container3.transform);
            o1.transform.position = new Vector3(1, -3, 1);
            isBusy = !isBusy;
        }
        if ((container4.transform.childCount < 1) && (!isBusy2))
        { 
            o2 = Instantiate(platform, new Vector3(-1, -3, 1), Quaternion.identity) as GameObject;
            o2.transform.SetParent(container4.transform);
            o2.transform.position = new Vector3(-1, -3, 1);
            isBusy2 = !isBusy2;
        }    
        else if(isBusy2)
        {
            o2 = Instantiate(platform, new Vector3(-1, -4, 1), Quaternion.identity) as GameObject;
            o2.transform.SetParent(container2.transform);
            o2.transform.position = new Vector3(-1, -4, 1);
            isBusy2 = !isBusy2;
        }
    }
	public void ClickedBtn()
    {
        isUse = true;
        o1.GetComponent<Rigidbody2D>().AddForce(new Vector2(o1.transform.position.x, o1.transform.position.y - 9), ForceMode2D.Impulse);     
        o2.GetComponent<Rigidbody2D>().AddForce(new Vector2(o2.transform.position.x, o2.transform.position.y - 9), ForceMode2D.Impulse);
        if(isBusy)
        {
            o1.transform.SetParent(container3.transform);
        } else
        {
            o1.transform.SetParent(container1.transform);
        }
        if (isBusy2)
        {
            o2.transform.SetParent(container2.transform);
        } else
        {
            o2.transform.SetParent(container4.transform);
        }
    }
    private void DestroyBlocks()
    {
        if (-o1.transform.position.y >= -zone.transform.position.y)
        {
            o2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(o1);
            Destroy(o2);
            isUse = false;
        }
        if (-o2.transform.position.y >= -zone.transform.position.y)
        {
            o1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(o1);
            Destroy(o2);
            isUse = false;
        }

    }
    private void CheckCount()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Block");
        if (gos.Length < 2)
        {
            Generate();
            o1.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + PlayerPrefs.GetString("current_platform"), typeof(Sprite)) as Sprite;
            o2.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + PlayerPrefs.GetString("current_platform"), typeof(Sprite)) as Sprite;
        }
    }
}
