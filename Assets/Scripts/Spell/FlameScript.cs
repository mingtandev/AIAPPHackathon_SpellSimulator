using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Checking();
    }

   

    void Checking()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Enemy"));
        Debug.Log(hit.Length);
        if (hit.Length != 0)
        {

            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].transform.gameObject.GetComponent<Enemy>().heath = 0;
            }

            Destroy(gameObject,1f);
        }
    }
}
