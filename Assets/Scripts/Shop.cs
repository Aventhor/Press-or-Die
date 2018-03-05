using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Shop : MonoBehaviour
{
    [SerializeField] private LangSystem ls;
    [SerializeField] private GameObject buy_window;
    [SerializeField] private Toggle bg_button;
    [SerializeField] private Toggle platf_button;
    [SerializeField] private Toggle skins_button;
    [SerializeField] private GameObject bg_list;
    [SerializeField] private GameObject platforms_list;
    [SerializeField] private GameObject skins_list;
    [SerializeField] private Text[] price_text = new Text[20];
    [SerializeField] private Text[] value_text_bg = new Text[11];
    [SerializeField] private Text[] value_text_pl = new Text[3];
    [SerializeField] private Text[] value_text_sk = new Text[8];
    [SerializeField] private GameObject[] price_line_bgs = new GameObject[10];
    [SerializeField] private GameObject[] price_line_pts = new GameObject[3];
    [SerializeField] private GameObject[] price_line_sk = new GameObject[8];
    [SerializeField] private Image bg_image;
    [SerializeField] private GameObject notification_board;
    [SerializeField] private Text error_text;


    [SerializeField] private GameObject[] ActiveLinesBG = new GameObject[8];
    [SerializeField] private GameObject[] ActiveLinesPL = new GameObject[3];
    [SerializeField] private GameObject[] ActiveLineSK = new GameObject[8];
    ItemList itm = new ItemList();
    BalanceSystem bs = new BalanceSystem();
    private bool isActive_window;
    int temp;

    void Awake()
    {
        GameObject but1 = buy_window.transform.Find("Close Button").gameObject;
        but1.GetComponentInChildren<Text>().text = LangSystem.lng.loc_shop[0];
        GameObject but2 = buy_window.transform.Find("Buy Item Button").gameObject;
        but2.GetComponentInChildren<Text>().text = LangSystem.lng.loc_shop[1];
        for (int i = 0; i < price_text.Length; i++) {
            price_text[i].text = LangSystem.lng.loc_shop[2] + ":";
        }
        error_text.text = LangSystem.lng.loc_shop[3];
    }
    void Start()
    {
        TextAsset langFile = Resources.Load("ItemList/Items") as TextAsset;
        itm = JsonUtility.FromJson<ItemList>(langFile.text);
        LoadData();
    }

    void Update()
    {
    }
    public void BGItems()
    {
        GetComponent<AudioSource>().Play();
        bg_button.GetComponent<Image>().color = new Color32(255, 140, 32, 255);
        platf_button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        skins_button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        bg_list.SetActive(true);
        skins_list.SetActive(false);
        platforms_list.SetActive(false);
    }
    public void PlatformsItems()
    {
        GetComponent<AudioSource>().Play();
        platf_button.GetComponent<Image>().color = new Color32(255, 140, 32, 255);
        bg_button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        skins_button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        bg_list.SetActive(false);
        skins_list.SetActive(false);
        platforms_list.SetActive(true);
    }
    public void SkinsItems ()
    {
        GetComponent<AudioSource>().Play();
        skins_button.GetComponent<Image>().color = new Color32(255, 140, 32, 255);
        bg_button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        platf_button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        bg_list.SetActive(false);
        platforms_list.SetActive(false);
        skins_list.SetActive(true);
    }
    public void BackMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Menu");
    }
    public void BuyItemWindow(int id)
    {
        if (!isActive_window)
        {
            temp = id;
            buy_window.GetComponent<Animation>().Play("Up");
            isActive_window = !isActive_window;
        }
    }
    public void BuyButtonFunction(int id)
    {
        id = temp;
        ConfirmPurchase(id);
    }
    public void ConfirmPurchase(int price_index)
    {
        if (bg_list.activeSelf)
        {
            if (BalanceSystem.gb.game_balance >= itm.bgs_prices[price_index])
            {
                BalanceSystem.gb.game_balance -= itm.bgs_prices[price_index];
                LangSystem.cnfg.avaliable_bgs[price_index] = 1;
                CloseItemWindow();
                notification_board.SetActive(false);
            }
            else
            {
                ErrorMessage();
            }
        }
        else if (platforms_list.activeSelf)
        {
            if (BalanceSystem.gb.game_balance >= itm.platforms_prices[price_index])
            {
                BalanceSystem.gb.game_balance -= itm.platforms_prices[price_index];
                LangSystem.cnfg.avaliable_platforms[price_index] = 1;
                CloseItemWindow();
                notification_board.SetActive(false);
            }
            else
            {
                ErrorMessage();
            }
        }
        else if (skins_list.activeSelf)
        {
            if (BalanceSystem.gb.game_balance >= itm.skins_prices[price_index])
            {
                BalanceSystem.gb.game_balance -= itm.skins_prices[price_index];
                LangSystem.cnfg.avaliable_skins[price_index] = 1;
                CloseItemWindow();
                notification_board.SetActive(false);
            }
            else
            {
                ErrorMessage();
            }
        }
        string path_conf = Path.Combine(Application.persistentDataPath, "Config.json");
        File.WriteAllText(path_conf, JsonUtility.ToJson(LangSystem.cnfg));
        bs.SaveGameBal();
        LoadData();
    }
    public void ErrorMessage() {
        notification_board.SetActive(true);
        notification_board.GetComponent<Animation>().Play("notificationOn");
    }
    public void CloseItemWindow()
    {
        buy_window.GetComponent<Animation>().Play("Down");
        notification_board.SetActive(false);
        isActive_window = !isActive_window;
    }
    public void LoadData()
    {
        ls.CheckConfig();
        for (int a = 1; a < value_text_bg.Length; a++)
        {
            value_text_bg[a].text = itm.bgs_prices[a].ToString();
        }
        for (int b = 1; b < value_text_pl.Length; b++)
        {
            value_text_pl[b].text = itm.platforms_prices[b].ToString();
        }
        for (int a = 1; a < value_text_sk.Length; a++)
        {
            value_text_sk[a].text = itm.skins_prices[a].ToString();
        }
        for (int c = 0; c < price_line_bgs.Length; c++)
        {
            if (LangSystem.cnfg.avaliable_bgs[c] != 1)
            {
                price_line_bgs[c].SetActive(true);
            }
            else
            {
                price_line_bgs[c].SetActive(false);
            }

        }
        for (int d = 0; d < price_line_pts.Length; d++)
        {
            if (LangSystem.cnfg.avaliable_platforms[d] != 1)
            {
                price_line_pts[d].SetActive(true);
            }
            else
            { 

                price_line_pts[d].SetActive(false);
            }   

        }
        for (int c = 0; c < price_line_sk.Length; c++)
        {
            if (LangSystem.cnfg.avaliable_skins[c] != 1)
            {
                price_line_sk[c].SetActive(true);
            }
            else
            {
                price_line_sk[c].SetActive(false);
            }

        }
        for (int e = 0; e < ActiveLinesBG.Length; e++)
        {
            if(LangSystem.cnfg.current_bg == itm.bgs_list[e].ToString())
            {
                ActiveLinesBG[e].SetActive(true);
            }
        }
        for (int f = 0; f < ActiveLinesPL.Length; f++)
        {
            if (LangSystem.cnfg.current_platform == itm.platforms_list[f].ToString())
            {
                ActiveLinesPL[f].SetActive(true);
            }
        }
        for (int e = 0; e < ActiveLineSK.Length; e++)
        {
            if (LangSystem.cnfg.current_skin == itm.skins_list[e].ToString())
            {
                ActiveLineSK[e].SetActive(true);
            }
        }
    }
    public void CheckHaveItem(int index)
    {
        ls.CheckConfig();
        if (bg_list.activeSelf)
        {
            if (LangSystem.cnfg.avaliable_bgs[index] == 0)
            {
                BuyItemWindow(index);
            }
            else if (LangSystem.cnfg.avaliable_bgs[index] == 1)
            {
                LangSystem.cnfg.current_bg = itm.bgs_list[index];
                string path_conf = Path.Combine(Application.persistentDataPath, "Config.json");
                File.WriteAllText(path_conf, JsonUtility.ToJson(LangSystem.cnfg));
                bg_image.overrideSprite = Resources.Load("Sprites/" + LangSystem.cnfg.current_bg, typeof(Sprite)) as Sprite;
                GameObject[] a = GameObject.FindGameObjectsWithTag("ActLineBG");
                for (int i = 0; i < a.Length; i++)
                {
                    a[i].gameObject.SetActive(false);
                }
                GameObject b = GameObject.Find(index.ToString()).gameObject;
                b.transform.Find("ActiveLineBG").gameObject.SetActive(true);
            }
        }
        if(platforms_list.activeSelf) {
            if (LangSystem.cnfg.avaliable_platforms[index] == 0)
            {
                BuyItemWindow(index);
            }
            else if (LangSystem.cnfg.avaliable_platforms[index] == 1)
            {
                LangSystem.cnfg.current_platform = itm.platforms_list[index];
                string path_conf = Path.Combine(Application.persistentDataPath, "Config.json");
                File.WriteAllText(path_conf, JsonUtility.ToJson(LangSystem.cnfg));
                GameObject[] a = GameObject.FindGameObjectsWithTag("ActLinePlatf");
                for (int i = 0; i < a.Length; i++)
                {
                    a[i].gameObject.SetActive(false);
                }
                GameObject b = GameObject.Find(index.ToString()).gameObject;
                b.transform.Find("ActiveLinePlatf").gameObject.SetActive(true);
            }
        }
        if (skins_list.activeSelf)
        {
            if (LangSystem.cnfg.avaliable_skins[index] == 0)
            {
                BuyItemWindow(index);
            }
            else if (LangSystem.cnfg.avaliable_skins[index] == 1)
            {
                LangSystem.cnfg.current_skin = itm.skins_list[index];
                string path_conf = Path.Combine(Application.persistentDataPath, "Config.json");
                File.WriteAllText(path_conf, JsonUtility.ToJson(LangSystem.cnfg));
                GameObject[] a = GameObject.FindGameObjectsWithTag("ActLineSK");
                for (int i = 0; i < a.Length; i++)
                {
                    a[i].gameObject.SetActive(false);
                }
                GameObject b = GameObject.Find(index.ToString()).gameObject;
                b.transform.Find("ActiveLineSK").gameObject.SetActive(true);
            }
        }
    }
}
public class ItemList {
    public string[] bgs_list = new string[11];
    public int[] bgs_prices = new int[11];
    public string[] platforms_list = new string[3];
    public int[] platforms_prices = new int[3];
    public string[] skins_list = new string[8];
    public int[] skins_prices = new int[8];

}
