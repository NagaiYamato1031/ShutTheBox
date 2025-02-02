using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// テスト
/// 一度実装する
/// 板の情報を管理
/// </summary>
public class T_TextChangeScript : MonoBehaviour
{
	// 中身を参照する
	[SerializeField]
	private TextMeshProUGUI _textMeshProUGUI;
	// 数字
	public int Number { get; private set; }
	// Start is called before the first frame update
	void Start()
	{
		if (_textMeshProUGUI == null)
			_textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetNumber(int number)
	{
		Number = number;
		_textMeshProUGUI.text = Number.ToString();
	}
}
