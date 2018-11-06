using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    //爆弾本体
    GameObject m_mainObj;

    //アニメーター
    Animator m_anime;

    //爆破エフェクト
    [SerializeField]
    GameObject m_brastEffect;

    Rigidbody2D m_rigitbody;

    //停止してから爆破に入る秒数
    float m_addTime = 0;
    float m_startExplosionTime = 0.3f;

    //爆破開始フラグ
    bool m_startExplosionFlag = false;

    float m_ToExplosionTime = 0;
    float m_EffectTotalTime = 0;

    void Start()
    {
        m_mainObj = transform.Find("Bomb").gameObject;

        m_anime = m_mainObj.GetComponent<Animator>();

        m_rigitbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //動きが無くなった&爆破フラグが経っていないならタイム加算
        if (Mathf.Abs(m_rigitbody.velocity.x) <= 1 && Mathf.Abs(m_rigitbody.velocity.y) <= 1 && m_startExplosionFlag == false)
        {
            m_addTime += Time.deltaTime;
        }
        else
        {
            m_addTime = 0;
        }

        //指定秒動きを止めていたら爆破フラグオン
        if (m_startExplosionTime < m_addTime && m_startExplosionFlag == false)
        {
            m_anime.Play("Bomb");
            m_anime.Update(0);

            m_EffectTotalTime = m_anime.GetCurrentAnimatorStateInfo(0).length;

            m_startExplosionFlag = true;

        }

        if (m_startExplosionFlag)
        {

            m_ToExplosionTime += Time.deltaTime;

            if (m_EffectTotalTime < m_ToExplosionTime)
            {
                Explosion();
                m_EffectTotalTime = 200;
            }
        }

    }


    //爆破
    void Explosion()
    {
        //爆弾の表示を消す
        m_mainObj.GetComponent<SpriteRenderer>().enabled = false;

        //爆弾のコライダーの半径を取得し、爆風範囲とする
        float rad = GetComponent<CircleCollider2D>().radius;
        //爆風範囲はちょっと大きめで
        rad += rad * 3;

        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        foreach (var col in Physics2D.CircleCastAll(transform.position, rad, Vector3.forward, Mathf.Infinity, 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ground")))
        {
            hit.Add(col);
        }

        for (int i = 0; i < hit.Count; i++)
        {

            if (hit[i].transform.gameObject.tag == "Block")
            {
                Debug.Log(hit[i].transform.gameObject);
                //GameObject eff_breakwall = Instantiate(m_breakEffect, hit[i].transform.position, hit[i].transform.rotation);
                Destroy(hit[i].transform.gameObject);

                //Destroy(eff_breakwall, eff_breakwall.GetComponent<ParticleSystem>().duration);
            }
        }

        //リジッドボデェを切る
        m_rigitbody.Sleep();

        GameObject eff_brast = Instantiate(m_brastEffect, transform.position, Quaternion.identity);
        eff_brast.transform.parent = transform;

        Destroy(transform.gameObject, eff_brast.GetComponent<ParticleSystem>().duration);

    }

}

