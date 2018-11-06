using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    private GameObject col_bomb;
    private Rigidbody2D bombRigidbody;

    private bool Shooting;
    
    private void OnTriggerEnter2D(Collider2D arg_col){

        //射出待機時
        if (!Shooting)
        {
            //ボムと衝突時
            if (arg_col.tag == "bomb")
            {
				GetComponent<AudioSource> ().Play();
                col_bomb = arg_col.gameObject;
                bombRigidbody = col_bomb.GetComponent<Rigidbody2D>();

                //ボムが大砲の位置まで到達したら
                col_bomb.transform.position = transform.position;
                //ボムは一瞬消える
                col_bomb.SetActive(false);

                //射出状態に移行
                Shooting = true;
                StartCoroutine("bomb");
            }
        }

        else{
            //爆弾が入ったら再び射出待機
            if (col_bomb) { Shooting = false; }
        }
    }


    //衝突してから一秒後に発射
    IEnumerator bomb(){
        yield return new WaitForSeconds(1f);
        col_bomb.SetActive(true);
        bombRigidbody.AddForce(new Vector3(0,50f,0),ForceMode2D.Impulse);
    }
}
