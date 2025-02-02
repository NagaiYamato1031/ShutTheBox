using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClickedGameObject : MonoBehaviour
{
    // 画面上のクリックしたオブジェクト
    public GameObject clickedGameObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // オブジェクトを空にする
            clickedGameObject = null;

            // カメラからレイを出す
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            // 判定
            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
				Debug.Log(clickedGameObject.name);
			}

            Debug.Log(clickedGameObject);
        }
        else
        {
            clickedGameObject = null;
        }
    }
}
