using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float m_moveSpeed;

    private bool m_jumping;
 
    private Rigidbody2D m_rigidbody2D;

    private void Start(){
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update (){
        PlayerAction();
	}

    private void PlayerAction(){
        if (Input.GetKeyDown(KeyCode.W)){
            if (!m_jumping){
                m_rigidbody2D.AddForce(transform.up * 1000f);
                m_jumping = true;
            }
        }
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(m_moveSpeed, 0, 0);     
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-m_moveSpeed, 0, 0);

        if (m_rigidbody2D.velocity.y == 0)
            m_jumping = false;
    }

}
