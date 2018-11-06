using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float m_moveSpeed;

    private bool m_jumping;
 
    private Rigidbody2D m_rigidbody2D;

    private SpriteRenderer m_spriteRenderer;

    private void Start(){
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update (){
        PlayerAction();
	}

    private void PlayerAction(){
        if (!m_jumping){
            if (Input.GetKeyDown(KeyCode.W)){
                m_rigidbody2D.AddForce(transform.up * 1000f);
               m_jumping = true;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(m_moveSpeed, 0, 0);
            m_spriteRenderer.flipX = true;
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-m_moveSpeed, 0, 0);
            m_spriteRenderer.flipX = false;
            GetComponent<AudioSource>().Play();
        }
        if (m_rigidbody2D.velocity.y == 0)
            m_jumping = false;
    }

    private void OnCollisionStay2D(Collision2D arg_col){
        if (arg_col.collider.tag == "bomb")
        {
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    //左向き
                    if (!m_spriteRenderer.flipX)
                        arg_col.gameObject.transform.position += new Vector3(-0.05f, 0, 0);

                    //右向き
                    if (m_spriteRenderer.flipX)
                        arg_col.gameObject.transform.position += new Vector3(0.05f, 0, 0);
                    Debug.Log("ぐり");
                }
            }
        }
    }

}
