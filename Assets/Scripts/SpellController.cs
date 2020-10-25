using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpellController instance;
    public Transform hand;
    public Transform EffectHand;



    [HideInInspector]
    Animator anim;

    public Vector3 direcSpell;

    [System.Serializable]
    public struct Spells
    {
        public GameObject spellFX;
        public GameObject EffectHand;
        public string name;
    }


    public Spells[] spells;
    public GameObject explosion;

    bool canSpell = true;
    public bool darkObsSpawn;
    public Vector3 posGenSpell;
    string completeSpell;

    private void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Hand").GetComponent<Animator>();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


    }

    void Start()
    {
        SpellRecognize();
    }

    // Update is called once per frame
    void Update()
    {
        //FIRE

        if (canSpell)
        {
            SpellRecognize();
        }
        else
        {
            StartCoroutine(CheckRecognizeContinue());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpellFlame();
        }
        //FREEZE
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpellFreeze();
        }
        //Teleport
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpellTeleport();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpellDarkObs();
        }

    }

    void SpellRecognize()
    {

        for (int i = 0; i < SpellCommandManager.instance.commands.Length; i++)
        {
            for (int j = 0; j < SpellCommandManager.instance.commands[i].listCommand.Length; j++)
            {
                if (SpellCommandManager.instance.commands[i].listCommand[j].Equals(SpellCommandManager.currentCommand))
                {

                    Debug.Log("HAVE : " + SpellCommandManager.instance.commands[i].nameSpell);
                    canSpell = false;
                    completeSpell = SpellCommandManager.currentCommand;
                    switch (SpellCommandManager.instance.commands[i].nameSpell)
                    {
                        case "Flame":
                            SpellFlame();
                            break;
                        case "Freeze":
                            SpellFreeze();
                            break;
                        case "Teleport":
                            SpellTeleport();
                            break;
                        case "Impossible":
                            SpellDarkObs();
                            break;
                    }
                    return;
                }
            }
        }
    }

    IEnumerator CheckRecognizeContinue()
    {
        yield return new WaitForSeconds(1f);
        if (!completeSpell.Equals(SpellCommandManager.currentCommand))
        {
            canSpell = true;
        }
    }

    IEnumerator resetAnimation()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("isSpell", false);
    }


    IEnumerator Teleport(Vector3 pos)
    {
        yield return new WaitForSeconds(1.5f);
        transform.position = pos;
    }

   
    void SpellFlame()
    {
        anim.SetBool("isSpell", true);
        Vector3 posGen = transform.position + hand.transform.forward * 5f;
        GameObject mySpell = Instantiate(spells[0].spellFX, transform.position + hand.transform.forward * 5f, transform.rotation);
        GameObject FXhand = Instantiate(spells[0].EffectHand, EffectHand.transform.position, Quaternion.identity, EffectHand);
        StartCoroutine(resetAnimation());
        Destroy(mySpell,2f);
    }

    void SpellFreeze()
    {
        direcSpell = hand.transform.forward;
        anim.SetBool("isSpell", true);
        GameObject mySpell = Instantiate(spells[1].spellFX, transform.position, Camera.main.transform.rotation);
        GameObject FXhand = Instantiate(spells[1].EffectHand, EffectHand.transform.position, Quaternion.identity, EffectHand);
        Destroy(mySpell, 3f);
        Destroy(FXhand, 3f);
        StartCoroutine(resetAnimation());
    }

    void SpellTeleport()
    {
        anim.SetBool("isSpell", true);
        Vector3 posGen = transform.position + hand.transform.forward * 10f;
        GameObject mySpell = Instantiate(spells[2].spellFX, posGen, Camera.main.transform.rotation);
        GameObject FXhand = Instantiate(spells[2].EffectHand, EffectHand.transform.position, Quaternion.identity, EffectHand);
        StartCoroutine(resetAnimation());
        StartCoroutine(Teleport(posGen));
    }


    void SpellDarkObs()
    {
        anim.SetBool("isSpell", true);
        posGenSpell = transform.position + hand.transform.forward * 10f;
        GameObject mySpell = Instantiate(spells[3].spellFX, EffectHand.transform.position, Camera.main.transform.rotation);
        mySpell.GetComponent<SpellForward>().constraintPos = posGenSpell;
        mySpell.GetComponent<SpellForward>().isObs = true;
        darkObsSpawn = true;

        GameObject FXhand = Instantiate(spells[3].EffectHand, EffectHand.transform.position, Quaternion.identity, EffectHand);
        StartCoroutine(resetAnimation());

    }






}
