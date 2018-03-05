using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

	[SerializeField] private Text score_status;
    [SerializeField] private Text score_points;

    private int points;
	public int _points {
		get { return points; }
		set { if(value >= 0) points = value; }
	}
    public int a;
	void Awake () {
	}
	// Use this for initialization
	void Start () {
        a = 1;
        score_status.text = LangSystem.lng.loc_main[0] + ":";
    }
	
	// Update is called once per frame
	void Update () {
        score_points.text = _points.ToString();
	}
	public void ScorePointsPlus () {
		_points += a;
	}
    public void PointsAnim()
    {
        score_points.GetComponent<Animation>().Play("IncreasePoints");
    }
}
