using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	// クリックしたオブジェクト取得
	public GetClickedGameObject getClickedGameObject;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // スペースで移動
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
        // クリックに反応
        if (getClickedGameObject.clickedGameObject != null)
        { 
            GameObject go = getClickedGameObject.clickedGameObject;
            switch (go.name)
            {
                case "Title":
                    break;
                case "Space":
                    SceneManager.LoadScene("SampleScene");
                    break;
            }
        }
    }
}
