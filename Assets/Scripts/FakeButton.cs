using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeButton : MonoBehaviour {
    public Button deadzone;
    public Button fake;
    public ScoreSystem ss;

    private Vector2 random_position;
    bool isActive = true;
    float chance;
    bool onStart;
    bool temp;
    float _x;
    float _y;
    float a;
    float b;
    float time;
    float cd_min;
    float cd_max;
    // Use this for initialization
    void Start () {
        a = fake.GetComponent<RectTransform>().sizeDelta.x;
        b = fake.GetComponent<RectTransform>().sizeDelta.y;
        StartCoroutine(WaitFewSecond());
    }
	
	// Update is called once per frame
	void Update () {
        SetCdMax();
        if (ss._points != 0) {
            if (isActive)
            {
                    if (!temp)
                    {
                        GetChance();
                        CheckChance(chance);
                    cd_min = 0f;
                }
                if (!onStart && temp)
                {
                    cd_min += Time.deltaTime;
                    if(cd_min > cd_max)
                        temp = !temp;
                }
                if (onStart)
                    {
                        time += Time.deltaTime;
                        if(time > 2f)
                        {
                            TurnOffButton();
                        }
                    }
                }
        }
	}
    IEnumerator WaitFewSecond()
    {
        yield return new WaitForSeconds(3f);
        deadzone.gameObject.SetActive(true);
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
    }
    public float GetChance()
    {
        chance = Random.Range(0f, 1f);
        return chance;
    }
    public void CheckChance(float chance)
    {
        if(ss._points < 100 && chance > 0.7) // 0.3 chance!
        {
            getPosButton();
            fake.transform.localPosition = random_position;
            fake.gameObject.SetActive(true);
            onStart = !onStart;
        }
        if (ss._points < 200 && ss._points > 100 && chance > 0.6) // 0.4 chance!
        {
            getPosButton();
            fake.transform.localPosition = random_position;
            fake.gameObject.SetActive(true);
            onStart = !onStart;
        }
        if (ss._points < 300 && ss._points > 200 && chance > 0.5) // 0.5 chance!
        {
            getPosButton();
            fake.transform.localPosition = random_position;
            fake.gameObject.SetActive(true);
            onStart = !onStart;
        }
        temp = !temp;
    }
    public void SetCdMax()
    {
        if (ss._points < 100)
            cd_max = 5;
        if (ss._points < 200 && ss._points > 100)
            cd_max = 3;
        if (ss._points < 300 && ss._points > 200)
            cd_max = 2;
    }
    public void TurnOffButton()
    {
        fake.gameObject.SetActive(false);
        time = 0f;
        onStart = !onStart;
    }
    public void GlobalTurnOfFakeButton()
    {
        fake.gameObject.SetActive(false);
        isActive = !isActive;
    }
}
