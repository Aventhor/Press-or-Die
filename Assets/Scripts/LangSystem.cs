using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LangSystem : MonoBehaviour {
    private string json;
    private string config_json;
    private string path_conf;
    public static Lang lng = new Lang();
    public static Config cnfg = new Config();
    public Button changeLang;

    void Awake()
    {
        string path_conf = Path.Combine(Application.persistentDataPath, "Config.json");
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
                cnfg.language_index = 0;
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
                cnfg.language_index = 1;
            }
            File.WriteAllText(path_conf, JsonUtility.ToJson(cnfg));
        }
        LangLoad();
    }
    void Start()
    {
        CheckIndexlang();
    }
    void CheckIndexlang()
    {
        CheckConfig();
            if (cnfg.language_index == 0)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
                if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu")) {
                    changeLang.GetComponent<Image>().sprite = Resources.Load("Sprites/flag_RU", typeof(Sprite)) as Sprite;
                }
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu")) 
                {
                    changeLang.GetComponent<Image>().sprite = Resources.Load("Sprites/flag_US", typeof(Sprite)) as Sprite;
                }
            }
            LangLoad();
    }
    public void CheckConfig()
    {
        path_conf = Path.Combine(Application.persistentDataPath, "Config.json");
        if (File.Exists(path_conf))
        {
            cnfg = JsonUtility.FromJson<Config>(File.ReadAllText(path_conf));
#if UNITY_ANDROID && !UNITY_EDITOR
        WWW reader = new WWW(path_conf);
        while(! reader.isDone) {}
        config_json = reader.text;
#endif
        }
    }
    void LangLoad()
    { 
        TextAsset langFile = Resources.Load("Languages/" + PlayerPrefs.GetString("Language")) as TextAsset;
        lng = JsonUtility.FromJson<Lang>(langFile.text);
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    void OnApplicationPause(bool pause)
    {
    if(pause) {
    File.WriteAllText(path_conf, JsonUtility.ToJson(cnfg));
    }
    }
#endif
    void OnApplicationQuit()
{
    File.WriteAllText(path_conf, JsonUtility.ToJson(cnfg));
}
public void ChangeLanguage()
{
        if(cnfg.language_index == 0)
        {
            cnfg.language_index = 1;
        } else
        {
            cnfg.language_index = 0;
        }
        File.WriteAllText(path_conf, JsonUtility.ToJson(cnfg));
        CheckIndexlang();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
[Serializable]
public class Lang {
    public string[] loc_menu = new string[4];
    public string[] loc_shop = new string[9];
    public string[] loc_main = new string[6];
}
[Serializable]
public class Config
{
    public int language_index;
    public int current_level = 1;
    public float current_exp;
    public string current_bg = "bg_default";
    public string current_platform = "platform_default";
    public string current_skin = "skin_default";
    public int record;
    public int[] avaliable_bgs = new int[11] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] avaliable_platforms = new int[6] { 1, 0, 0, 0, 0, 0 };
    public int[] avaliable_skins = new int[8] { 1, 0, 0, 0, 0, 0, 0, 0 };
}
