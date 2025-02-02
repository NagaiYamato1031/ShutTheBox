using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
	// スコア表示
	public GameObject Score;

	// クリックしたオブジェクト取得
	public GetClickedGameObject getClickedGameObject;
	// スコア取得
	private ScoreDataScript scoreDataScript;

	// Start is called before the first frame update
	void Start()
	{
		// スコア取得
		scoreDataScript = GameObject.Find("ScoreData").GetComponent<ScoreDataScript>();
		// 表示
		GetComponent<T_TextChangeScript>().SetNumber(scoreDataScript.ThrowingScore);
	}

	// Update is called once per frame
	void Update()
	{
		// スペースで移動
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene("TitleScene");
		}
		// クリックに反応
		if (getClickedGameObject.clickedGameObject != null)
		{
			GameObject go = getClickedGameObject.clickedGameObject;
			switch (go.name)
			{
				case "Score":
					break;
				case "Space":
					SceneManager.LoadScene("TitleScene");
					Destroy(scoreDataScript.gameObject);
					break;
			}
		}
	}
}
