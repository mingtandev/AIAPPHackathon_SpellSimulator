using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyImpact : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Skin;
    Renderer rend;

    bool isFreeze;
    private void Awake()
    {
        rend = Skin.GetComponent<Renderer>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFreeze)
        {
            rend.material.color = Color.Lerp(rend.material.color, new Color(0, 90f, 255f), Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FreezeSpell"))
        {
            isFreeze = true;
            GetComponent<Animator>().enabled = false; //STOP BEHAVIOR
            NormalZombie nm = GetComponent<NormalZombie>();
            StopCoroutine(nm.Couroutine_PathFiding);
            //StartCoroutine(DisableBehaviorEnemy());
            nm.agent = null;
            GetComponent<NavMeshAgent>().enabled = false;  //stop path finding


        }
    }

    IEnumerator DisableBehaviorEnemy()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<NormalZombie>().enabled = false;
    
    }
}
