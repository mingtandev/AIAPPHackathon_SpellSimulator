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
    Color freezeColor = new Color(0, 90f, 255f);
    Rigidbody body;

    public static int numOfEnemy = 1;
    public static int numOfEnemyKill = 0;
    SpellController controller;

    private void Awake()
    {
        rend = Skin.GetComponent<Renderer>();
        curMats = Skin.GetComponent<Renderer>().materials;
        curColor = rend.material.color;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<SpellController>();
        body = GetComponent<Rigidbody>();
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

        if (controller.darkObsSpawn)
        {
            DarkObsEffect();
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

    void DarkObsEffect()
    {

        Vector3 posHole = controller.posGenSpell;
        Vector3 dir = posHole - transform.position;
        body.AddForce(dir * 100f);
    }

    IEnumerator CurrentDestroy_SpawnContinue()
    {

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", curColor);
        Skin.GetComponent<Renderer>().SetPropertyBlock(props);

    }







}
