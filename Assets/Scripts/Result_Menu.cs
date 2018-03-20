using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result_Menu : MonoBehaviour {
    [SerializeField] private Image result_panel;
    [SerializeField] private Text result_text;
    [SerializeField] private Text score_text;
    [SerializeField] private ScoreSystem points_script;
    [SerializeField] private Text fragment;
    [SerializeField] private GameObject level_bar;
    [SerializeField] private Effects eff;
    [SerializeField] private RecordsSystem rc;
    [SerializeField] private FakeButton fb;
    public AdvertisingBanner ab;

    private int bal_points; 
    public BalanceSystem bg;

    public LevelSystem ls;
    float a;
	// Use this for initialization
    void Awake()
    { 
    }
	void Start () {
        result_text.text = LangSystem.lng.loc_main[1];
        GameObject a = result_panel.transform.Find("Retry Button").gameObject;
        a.GetComponentInChildren<Text>().text = LangSystem.lng.loc_main[2];
        GameObject b = result_panel.transform.Find("Exit Button").gameObject;
        b.GetComponentInChildren<Text>().text = LangSystem.lng.loc_main[3];
        GameObject c = result_panel.transform.Find("Shop Button").gameObject;
        c.GetComponentInChildren<Text>().text = LangSystem.lng.loc_main[4];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MoveMainMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Menu");
    }
    public void MoveShopMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Shop");
    }
    public void ReloadCurrentScene()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void CalcFragments() {
        float temp = Mathf.Round(((float)(points_script._points)) / 6 * 1.2f); //20%
        bal_points = (int)temp;
        fragment.text = "+ " + bal_points.ToString();
        BalanceSystem.gb.game_balance += bal_points;
        bg.SaveGameBal();

    }
    void CalcExp()
    {
        ls._current_exp += (((float)(points_script._points)) / 25) ;
        ls.IncreaseLevel();
        LangSystem.cnfg.current_exp = ls._current_exp;
        ls.SaveExpLv();
        ls.CheckExpLv();
    }
    public void DisplayResult()
    {
        eff.DisableEffects();
        fb.GlobalTurnOfFakeButton();
        level_bar.GetComponent<Animation>().Play("animBar");
        result_panel.gameObject.SetActive(true);
        score_text.text = points_script._points.ToString();
        rc.WriteReult();
        CalcFragments();
        CalcExp();
        GenerateNumber();
        if(a > 0.6)
        ab.ShowAdvertising();


    }
    float GenerateNumber()
    {
        a = Random.Range(0f, 1f);
        return a;
    }
}
