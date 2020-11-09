using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
#if UNITY_EDITOR
	public bool addScore;
#endif

	[SerializeField]
	private Text display;
	[Tooltip("How much to increment score by")]
	[SerializeField]
	private int scorePoint;
	private int score;

	private void Start()
	{
		score = 0;
	}

	void Update()
    {
#if UNITY_EDITOR
		if(addScore)
		{
			addPoints();
			addScore = false;
		}
#endif
		display.text = score.ToString();
    }

	public void addPoints()
	{
		score += scorePoint;
	}
}
