using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class BalanceSystem : MonoBehaviour
{
    private string path;
    private string json_file;
    public static GameBalance gb = new GameBalance();
    [SerializeField] private Text bal_text;

    private int balance;
    public int _balance
    {
        get { return balance; }
        set { if (gb.game_balance < 100000) this.balance = value; }
    }

    void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "Balance.json");
        if (File.Exists(path))
        {
            gb = JsonUtility.FromJson<GameBalance>(File.ReadAllText(path));
#if UNITY_ANDROID && !UNITY_EDITOR
        WWW reader = new WWW(path);
        while(! reader.isDone) {}
        json_file = reader.text;
#endif
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Shop"))
        {
            _balance = gb.game_balance;
            bal_text.text = _balance.ToString();
        }
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGameBal();
        }
}
#endif
  
    void OnApplicationQuit()
    {
        SaveGameBal();
    }
    public void SaveGameBal()
    {
        path = Path.Combine(Application.persistentDataPath, "Balance.json");
        File.WriteAllText(path, JsonUtility.ToJson(gb));
    }
}
public class GameBalance
{
    public int game_balance = 0;
}
