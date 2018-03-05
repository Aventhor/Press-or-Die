using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelSystem : MonoBehaviour {
    [SerializeField] private Text level_text;
    [SerializeField] private Transform progress_bar;
    [SerializeField] private GameObject level_info;
    [SerializeField] private Text curr_exp;


    private string path;
    private bool isActive;
    private int level;
    public int _level
    {
        get {  return level; }
        set {  this.level = value; }
    }
    private float current_exp;
    public float _current_exp
    {
        get { return current_exp; }
        set { this.current_exp = value; }
    }
    private float max_exp;
    public float _max_exp
    {
        get { return max_exp; }
        set { this.max_exp = value; }
    }

    void Awake()
    {
        CheckConfig();
        _level = LangSystem.cnfg.current_level;
        _current_exp = LangSystem.cnfg.current_exp;
        _max_exp = _level * 20;
    }

	void Start () {
        if ((SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu")) || (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main")))
        {
            CheckExpLv();
        }

    }
	public void CheckExpLv ()
    {
        level_text.text = LangSystem.lng.loc_menu[1] + ": " + _level;
        progress_bar.GetComponent<Image>().fillAmount = _current_exp / _max_exp;
    }
	void Update () {
    }
    public void CheckConfig()
    {
        string path_conf = Path.Combine(Application.persistentDataPath, "Config.json");
        if (File.Exists(path_conf))
        {
            LangSystem.cnfg = JsonUtility.FromJson<Config>(File.ReadAllText(path_conf));
#if UNITY_ANDROID && !UNITY_EDITOR
        WWW reader = new WWW(path_conf);
        while(! reader.isDone) {}
        string config_json = reader.text;
#endif


        }
    }
    public void SaveExpLv()
    {
        path = Path.Combine(Application.persistentDataPath, "Config.json");
        File.WriteAllText(path, JsonUtility.ToJson(LangSystem.cnfg));
    }
    public void IncreaseLevel()
    {
        if(_current_exp >= _level * 20)
        {
            _current_exp %= _max_exp;
            _level++;
            LangSystem.cnfg.current_level = _level;
            LangSystem.cnfg.current_exp = _current_exp;
        }
        SaveExpLv();
    }
    public void ShowInfo()
    {
        if (!isActive)
        {
            level_info.SetActive(true);
            curr_exp.text = LangSystem.lng.loc_menu[2] + ":\n" + _current_exp + " / " + _max_exp;
            isActive = !isActive;
        }
        else
        {
            level_info.SetActive(false);
            isActive = !isActive;
        }
    }
}