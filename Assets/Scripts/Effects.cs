using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour {
    [SerializeField] private Button effect_button;
    [SerializeField] private ScoreSystem ss;
    [SerializeField] private RandomButton rb;
    [SerializeField] private Image effect_image;
    [SerializeField] private Text cooldown;
    [SerializeField] private Image visible_defect;

    private Vector2 random_position;
    private int effect_number;
    private float time;
    private float effect_time;
    private float random_value;
    private bool isActive;
    public bool onStart = true;
    public bool onEnd;
    float cd;
    bool temp;
    float a;
    float b;
    float _x;
    float _y;
    // Use this for initialization
    void Start () {
        a = effect_button.GetComponent<RectTransform>().sizeDelta.x;
        b = effect_button.GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onEnd)
        {
            SpawnButton();
            time += Time.deltaTime;
            if (time >= random_value)
            {
                if (!temp)
                {
                    getPosButton();
                    effect_button.gameObject.SetActive(true);
                    temp = !temp;
                }
                if (isActive)
                {
                    time = random_value + 1.99f;
                    effect_time -= Time.deltaTime;
                    TimeRemaining(effect_time);
                    if (effect_time <= 0)
                    {
                        DefaultEffectsList(effect_number);
                    }
                }
                if (time > random_value + 2)
                {
                    Cooldown();
                }
            }
        }
    }
    private Vector2 getPosition()
    {
        return random_position;
    }
    private void setPosition(float x, float y)
    {
        this._x = x;
        this._y = y;
        random_position = new Vector2(_x, _y);
    }
    public void getPosButton()
    {
        setPosition(Random.Range(-Screen.width / 2 + a / 2, Screen.width / 2 - a / 2), Random.Range(-Screen.height / 6 + b / 2, Screen.height / ((float)(2.4)) - b / 2));
        getPosition();
        effect_button.transform.localPosition = random_position;
    }
    void Cooldown()
    {   
        effect_button.gameObject.SetActive(false);
        time = 0f;
        onStart = !onStart;
    }
    public void SpawnButton()
    {
        if (onStart)
        {
            random_value = Random.Range(5f, 20f);
            onStart = !onStart;
        }
    }
    public void DisableEffects()
    {
        onEnd = !onEnd;
        time = 0;
        effect_button.gameObject.SetActive(false);
        effect_image.gameObject.SetActive(false);
    }

    private float TimeRemaining(float time)
    {
        cd = ((int)(time * 100)) / 100;
        cooldown.text = cd.ToString();
        return cd;
    }
    public void GenerateEffect()
    {
        isActive = true;
        effect_image.gameObject.SetActive(true);
        effect_button.gameObject.SetActive(false);
        EffectsList(Random.Range(0, 4));
    }
    public int EffectsList(int index)
    {
        switch(index)
        {
            case 0:
                effect_image.GetComponent<Image>().sprite = Resources.Load("UI/points_effect", typeof(Sprite)) as Sprite;
                ss.a = 2;
                effect_time = 5;
                break;
            case 1:
                effect_image.GetComponent<Image>().sprite = Resources.Load("UI/points_defect", typeof(Sprite)) as Sprite;
                ss.a = -1;
                effect_time = 5;
                break;
            case 2:
                effect_image.GetComponent<Image>().sprite = Resources.Load("UI/invisible_defect", typeof(Sprite)) as Sprite;
                visible_defect.gameObject.SetActive(true);
                effect_time = 7;
                rb.time_bar.gameObject.SetActive(false);
                break;
            case 3:
                effect_image.GetComponent<Image>().sprite = Resources.Load("UI/freeze_effect", typeof(Sprite)) as Sprite;
                rb.click.onClick.RemoveListener(rb.ChangeIntervalTime);
                rb.f_time += 0.5f;
                effect_time = 7;
                break;
        }
        effect_number = index;
        return effect_number;
    }
    public void DefaultEffectsList(int index)
    {
        switch (index)
        {
            case 0:
                effect_image.gameObject.SetActive(false);
                ss.a = 1;
                break;
            case 1:
                effect_image.gameObject.SetActive(false);
                ss.a = 1;
                break;
            case 2:
                effect_image.gameObject.SetActive(false);
                visible_defect.gameObject.SetActive(false);
                rb.time_bar.gameObject.SetActive(true);
                break;
            case 3:
                effect_image.gameObject.SetActive(false);
                rb.click.onClick.AddListener(rb.ChangeIntervalTime);
                break;
        }
        onStart = !onStart;
        isActive = !isActive;
        time = 0f;
        temp = !temp;
    }
}
