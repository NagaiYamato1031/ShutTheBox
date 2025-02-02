using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDataScript : MonoBehaviour
{
	// サイコロを投げた回数
	public int ThrowingScore;

	void Start()
	{		
		//DontDestroyOnLoadでシーン遷移後も保存出来る
		DontDestroyOnLoad(this.gameObject);
		//ゲームスタート時のスコア
		ThrowingScore = 0;
	}
}
