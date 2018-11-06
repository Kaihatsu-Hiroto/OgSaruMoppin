using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    [SerializeField]
    private float geneTime;

    [SerializeField]
    private GameObject m_bomb;

    private bool geneflg;

    // Update is called once per frame
    void Update()
    {
        if (!geneflg)
        {
            geneflg = true;
            StartCoroutine("GeneBomb");
        }
    }

    private IEnumerator GeneBomb()
    {
            yield return new WaitForSeconds(geneTime);
            geneflg = false;
            Instantiate(m_bomb,transform.position,Quaternion.identity);

    }
}
