using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyImpact : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Skin;
    Renderer rend;
    Material[] curMats;
    Color curColor;
    bool isFreeze;
    Transform PosSpawn;
    Color freezeColor = new Color(0, 90f, 255f);

    public static int numOfEnemy = 1;
    public static int numOfEnemyKill = 0;
    private void Awake()
    {
        rend = Skin.GetComponent<Renderer>();
        curMats = Skin.GetComponent<Renderer>().materials;
        curColor = rend.material.color;
        PosSpawn = GameObject.FindGameObjectWithTag("SpawnPos").transform;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFreeze)
        {
            rend.material.color = Color.Lerp(rend.material.color, freezeColor, Time.deltaTime);
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
            nm.agent = null;
            GetComponent<NavMeshAgent>().enabled = false;  //stop path finding
            StartCoroutine(CurrentDestroy_SpawnContinue());
        }
    }
    IEnumerator CurrentDestroy_SpawnContinue()
    {

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", curColor);
        Skin.GetComponent<Renderer>().SetPropertyBlock(props);
        numOfEnemyKill++;

        if (numOfEnemyKill == numOfEnemy)
        {
            numOfEnemy++;
            numOfEnemyKill = 0;
            for (int i = 0; i < numOfEnemy; i++)
                EnemyPool.Instance.SpawnPool("Enemy", posSpawn().position, posSpawn().rotation);
        }

        if (numOfEnemy == 50)
            yield break;

    }

    Transform posSpawn()
    {
        int lengthChild = PosSpawn.childCount;
        int ran = Random.Range(0, lengthChild);

        return PosSpawn.GetChild(ran).gameObject.transform;

    }


}
