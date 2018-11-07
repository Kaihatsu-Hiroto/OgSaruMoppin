using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RimitTime : MonoBehaviour {

    //制限時間
    private float time = 60;

    private Text m_text;
	// Use this for initialization
	void Start () {
        m_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        time -= Time.deltaTime;
        m_text.text = "制限時間:"+Mathf.Round(time);

        //ゲーム終了処理
        if (time <= 0)
        {
            m_text.text = "終了";
            SceneManager.LoadScene("Clear");
        }
	}
}
