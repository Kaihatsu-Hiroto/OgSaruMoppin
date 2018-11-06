using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour {

    [SerializeField]
    private GameObject targetObject;

    private Rigidbody2D m_rigidbody2D;

    //爆弾の着地フラグ
    private bool Landing;

    //ランダム変数
    private int randNum;

    [SerializeField]
    private LandManager m_landManager;

    [SerializeField]
    private GameObject landManagerObj;

    private void Start(){
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        //生成時に0～3のランダム数値格納
        randNum = Random.Range(0, 4);

        //HierarchyからLandManagerのオブジェクト格納
        landManagerObj = GameObject.Find("LandManager");
        m_landManager = landManagerObj.GetComponent<LandManager>();

        //生成時にランダムに位置取得 
        targetObject = m_landManager.landPosition[randNum];

        Debug.Log(randNum);

    }

    private void BombMove(){
        if (!Landing){
            transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, 0.05f);
        }
    }

	// Update is called once per frame
	void Update () {
        BombMove();

        //上に飛び出たらオブジェクト破壊
        if (transform.position.y > Camera.main.transform.position.y + 5)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision){
        Landing = true; Debug.Log("着地");
        m_rigidbody2D.gravityScale = 1f;
    }

}
