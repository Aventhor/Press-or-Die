using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordsSystem : MonoBehaviour {
    [SerializeField] private ScoreSystem ss;
    [SerializeField] private Text rcd;
    [SerializeField] private GameObject your_record;
    [SerializeField] private Text newrecordText;

    [SerializeField ]private LangSystem ls;

    bool isActive;
    string path;
    // Use this for initialization
    void Start() {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu"))
        {
            ls.CheckConfig();
            your_record.GetComponentInChildren<Text>().text = LangSystem.lng.loc_menu[3];
            rcd.text = LangSystem.cnfg.record.ToString();
        }
    }

    // Update is called once per frame
    void Update() {

    }
    public void SaveData()
    {
        path = Path.Combine(Application.persistentDataPath, "Config.json");
        File.WriteAllText(path, JsonUtility.ToJson(LangSystem.cnfg));
    }
    public void ShowText()
    {
        if(!isActive)
        {
            your_record.SetActive(true);
        }
        else
        {
            your_record.SetActive(false);
        }
        isActive = !isActive;
    }
    public void WriteReult() {
        if(LangSystem.cnfg.record < ss._points)
        {
            newrecordText.text = LangSystem.lng.loc_main[5];
            LangSystem.cnfg.record = ss._points;
            SaveData();
        }
        }
}
