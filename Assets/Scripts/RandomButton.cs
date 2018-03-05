using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomButton : MonoBehaviour {
	
	public Button click;
	[SerializeField] private ScoreSystem score;
    public Transform time_bar;
    [SerializeField] private Result_Menu res_menu;
    [SerializeField] private Image second_circle;
    [SerializeField] private GameObject platformContainer;
    [SerializeField] private GameObject perfect;

    public Cube cube = new Cube();

	private Vector2 random_position;
	private bool clicked = true;
	private float _x;
	private float _y;
	private float a;
	private float b;
	private float interval_time;
	private float s_time = 0f;
	public float f_time = 2f;
    Vector2 startScalse;
    Vector2 endScalse;




    private void Awake() {

	}
	private void Start () {		
		a = click.GetComponent<RectTransform>().sizeDelta.x;
		b = click.GetComponent<RectTransform>().sizeDelta.y;
        StartCoroutine(WaitFewSecond());
        startScalse = new Vector2(1.5f, 1.5f);
        endScalse = new Vector2(1, 1);
    }
		
	void Update () {
        if (!clicked)
        {
            s_time += Time.deltaTime;
            time_bar.GetComponent<Image>().fillAmount = s_time / f_time;
            second_circle.transform.localScale = Vector2.Lerp(startScalse, endScalse, s_time / f_time );
            if (s_time > f_time)
            {
            getPosButton();
                if (!Input.GetMouseButtonDown(0) && !clicked)
                {
                    DestroyGameProcess();
                    res_menu.DisplayResult();
                }
            }
        }
    }
    IEnumerator WaitFewSecond()
    {
        yield return new WaitForSeconds(3f);
        clicked = !clicked;
        click.gameObject.SetActive(true);
    }
	private void setPosition (float x, float y) {
		this._x = x;
		this._y = y;
	    random_position = new Vector2(_x, _y);
	}
	private Vector2 getPosition() {
		return random_position;
	}
	public void getPosButton() {
        //setPosition (Random.Range (-Screen.width / 2 + a / 2, Screen.width / 2 - a / 2), Random.Range (-Screen.height / 2 + b / 2, Screen.height / 2 - b / 2)); full screen
        setPosition(Random.Range(-Screen.width / 2 + a / 2, Screen.width / 2 - a / 2), Random.Range(-Screen.height / 6 + b / 2, Screen.height / ((float)(2.4)) - b / 2));
		getPosition ();
		click.transform.localPosition = random_position;
        click.GetComponent<Animation>().Play("buttonSpawn");
        s_time = 0f;
        if (click.GetComponent<Animation>().IsPlaying("buttonSpawn"))
        {
            click.GetComponent<Animation>().Stop("buttonSpawn");
            click.GetComponent<Animation>().Play("buttonSpawn");
        }
        if (!click.GetComponent<Animation>().IsPlaying("buttonSpawn"))
        {
            click.GetComponent<Animation>().Play("buttonSpawn");
        }
    }
    public void ChangeIntervalTime()
    {
        Debug.Log(f_time);
        if (score._points != 0)
        {
            interval_time = 0.005f;
            f_time -= interval_time;
            if (score._points == 100)
            {
                perfect.GetComponent<Animation>().Play("perfect");
            }
        }
	}
    public void DestroyGameProcess()
    {
        platformContainer.GetComponent<Animation>().Play("platform_fall");
        cube.cube2d.GetComponent<Rigidbody2D>().AddForce(new Vector2(cube.cube2d.transform.position.x, cube.cube2d.transform.position.y - 1), ForceMode2D.Impulse);
        Destroy(click.gameObject);
        clicked = !clicked;
        
    }
}
