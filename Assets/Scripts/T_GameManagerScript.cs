using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// テスト
/// 一度このスクリプトで完成させる
/// </summary>
public class T_GameManagerScript : MonoBehaviour
{
	/// <summary>
	/// 外部からの設定
	/// </summary>

	// プレハブ
	public GameObject pNumberPlate;
	// サイコロのプレハブ
	public GameObject pDice;
	// クリックしたオブジェクト取得
	public GetClickedGameObject getClickedGameObject;

	// 板の横幅
	[SerializeField]
	private float kPlateWidth = 1.0f;
	// 板同士の隙間
	[SerializeField]
	private float kPlatePadding = 1.0f;

	// サイコロの横幅
	[SerializeField]
	private float kDiceWidth = 2.0f;
	// サイコロ同士の隙間
	[SerializeField]
	private float kDicePadding = 1.0f;

	/// <summary>
	/// 変数
	/// </summary>

	// 数字の配列
	private List<GameObject> numbers = new List<GameObject>();
	// サイコロの配列
	private List<GameObject> dices = new List<GameObject>();
	// 非効率だけどサイコロの数字
	private List<int> diceNumbers = new List<int>();

	// 数字の初期値
	[SerializeField]
	private int startNumber = 1;
	// 数字の枚数
	[SerializeField]
	private int kindNumber = 9;
	// サイコロを振れる数
	[SerializeField]
	private int maxThrow = 2;
	// ダイスを振った回数
	//private int throwing = 0;
	// スクリプト
	private T_TextChangeScript sTextChageScore;
	// サイコロを投げた回数
	private ScoreDataScript scoreDataScript;

	// Start is called before the first frame update
	void Start()
	{
		// スコア書き換え用
		sTextChageScore = gameObject.GetComponent<T_TextChangeScript>();
		// 取得
		GameObject scoreData = GameObject.Find("ScoreData");
		DontDestroyOnLoad(scoreData);
		scoreDataScript = scoreData.GetComponent<ScoreDataScript>();

		// オブジェクト生成
		GenerateNumbers();
		// 数字の初期化
		InitializeNumbers();
		// 整理
		AdjustmentNumbers();
	}

	// Update is called once per frame
	void Update()
	{
		// R ですべてリセット
		if (Input.GetKeyDown(KeyCode.R))
		{
			InitializeNumbers();
			ClearDice();
			scoreDataScript.ThrowingScore = 0;
			sTextChageScore.SetNumber(scoreDataScript.ThrowingScore);
		}
		// S を押して整理
		if (Input.GetKeyDown(KeyCode.S))
		{
			AdjustmentNumbers();
		}
		// D でダイスを振る
		if (Input.GetKeyDown(KeyCode.D))
		{
			ThrowDice();
		}
		// C でダイスリセット
		if (Input.GetKeyDown(KeyCode.C))
		{
			ClearDice();
		}

		// ダイス数が 1 以上の時
		if (1 <= dices.Count)
		{
			SelectNumber();
		}
		// 当たっている時
		if (getClickedGameObject.clickedGameObject != null)
		{
			// 取得
			GameObject go = getClickedGameObject.clickedGameObject;
			switch (go.name)
			{
				case "DescriptClick":
					// クリックの説明を押したとき
					break;
				case "DescriptDice":
					// サイコロの説明を押したとき
					ThrowDice();
					break;
				case "DescriptClear":
					// 消去の説明を押したとき
					ClearDice();
					break;
			}
		}
	}

	// 数字板を生成
	void GenerateNumbers()
	{
		for (int i = 0; i < kindNumber; i++)
		{
			numbers.Add(Instantiate(pNumberPlate, Vector3.zero, Quaternion.identity));
		}
	}
	// 数字を設定
	void InitializeNumbers()
	{
		int num = startNumber;
		foreach (GameObject number in numbers)
		{
			{
				number.name = "Number" + num.ToString();
				number.GetComponent<T_TextChangeScript>().SetNumber(num);
				number.SetActive(true);
				num++;
			}
		}
	}

	// 均等に座標を設定して並べる
	void AdjustmentNumbers()
	{
		// 設置するポジション
		Vector3 position = new Vector3(0, 5, 10);
		float wid = (kPlateWidth + kPlatePadding) * 0.5f * (numbers.Count - 1);
		position.x = -wid;
		for (int i = 0; i < numbers.Count; i++)
		{
			Vector3 pos = position;
			pos.x += i * (kPlateWidth + kPlatePadding);
			numbers[i].transform.position = pos;
		}
	}

	// サイコロ投げ
	void ThrowDice()
	{
		// 回数制限
		if (maxThrow <= dices.Count)
		{
			return;
		}
		// ランダムで 1 ~ 6 を出す
		int rand = Random.Range(1, 7);
		GameObject dice = Instantiate(pDice, new Vector3(0, 5, 10), Quaternion.identity);
		// クラスにして数字を参照できるようにしたい
		dice.GetComponent<T_TextChangeScript>().SetNumber(rand);
		dices.Add(dice);
		diceNumbers.Add(rand);
		// 整理
		AdjustmentDices();

		// スコアに表示
		scoreDataScript.ThrowingScore++;
		sTextChageScore.SetNumber(scoreDataScript.ThrowingScore);
	}

	// サイコロの整理
	void AdjustmentDices()
	{
		// 左上から
		Vector3 position = new Vector3(-15, 12, 10);
		for (int i = 0; i < dices.Count; i++)
		{
			Vector3 pos = position;
			pos.x += i * (kDiceWidth + kDicePadding);
			dices[i].transform.position = pos;
		}
	}

	// 消去
	void ClearDice()
	{
		foreach (GameObject dice in dices)
		{
			Destroy(dice);
		}
		dices.Clear();
		diceNumbers.Clear();
	}

	// 倒す数字を選ぶ
	void SelectNumber()
	{
		// 倒す数字を選んでいるとき
		if (getClickedGameObject.clickedGameObject != null &&
			getClickedGameObject.clickedGameObject.tag == "NumberPlate")
		{
			GameObject numObj = getClickedGameObject.clickedGameObject;
			// 取得成功
			int number = numObj.GetComponent<T_TextChangeScript>().Number;
			// サイコロの数字と一緒なら倒す
			if (IsMatchDices(number))
			{
				numbers[number - startNumber].SetActive(false);
				// サイコロを消してもいい
				//ClearDice();
				// 全部消したら
				bool isEnd = true;
				foreach (GameObject numberObj in numbers)
				{
					if (numberObj.activeSelf == true)
					{
						isEnd = false;
						break;
					}
				}
				if (isEnd)
				{
					SceneManager.LoadScene("EndScene");
				}
			}
		}
	}

	// サイコロとあっているか判定する
	bool IsMatchDices(int number)
	{
		// 合計値
		int sum = 0;
		// 複数の組み合わせも試せるようにする
		foreach (int i in diceNumbers)
		{
			sum += i;
			if (i == number || sum == number)
			{
				return true;
			}
		}
		return false;
	}

	// 消えていく演出

}
