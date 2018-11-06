using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    private GameObject col_bomb;
    private Rigidbody2D bombRigidbody;

    private bool Shooting;

    private void OnTriggerEnter2D(Collider2D arg_col){
        if (!Shooting)
        {
            if (arg_col.tag == "bomb")
            {
                col_bomb = arg_col.gameObject;
                bombRigidbody = col_bomb.GetComponent<Rigidbody2D>();
                col_bomb.transform.position = transform.position;
                col_bomb.SetActive(false);
                Shooting = true;
                StartCoroutine("bomb");
            }
        }
    }

    IEnumerator bomb(){
        yield return new WaitForSeconds(1f);
        col_bomb.SetActive(true);
        bombRigidbody.AddForce(new Vector3(0,50f,0),ForceMode2D.Impulse);
    }
}
