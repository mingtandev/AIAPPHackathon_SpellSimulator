using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpellController instance;
    public Transform hand;
    public Transform EffectHand;
    

    [HideInInspector]
    public Ray ray;
    public RaycastHit hit;
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

    private void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Hand").GetComponent<Animator>();
        if(instance==null)
            instance = this;
        else
            Destroy(gameObject);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Aimming();
        //FIRE
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            
            anim.SetBool("isSpell", true);
            GameObject mySpell = Instantiate(spells[0].spellFX, transform.position + hand.transform.forward * 5f, transform.rotation);
            GameObject FXhand = Instantiate(spells[0].EffectHand, EffectHand.transform.position, Quaternion.identity, EffectHand);
            StartCoroutine(resetAnimation());

        }
        //FREEZE
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            direcSpell = hand.transform.forward;
            anim.SetBool("isSpell", true);
            GameObject mySpell = Instantiate(spells[1].spellFX, transform.position , Camera.main.transform.rotation);
            GameObject FXhand = Instantiate(spells[1].EffectHand, EffectHand.transform.position, Quaternion.identity, EffectHand);
            Destroy(mySpell,3f);
            Destroy(FXhand,3f);
            StartCoroutine(resetAnimation());
        }

        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            
        }
        
    }

    void Aimming()
    {
        ray.origin = Camera.main.transform.forward;
        ray.direction = Camera.main.transform.forward;
        Physics.Raycast(ray , out hit);
        
    }

    IEnumerator resetAnimation()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("isSpell", false);
    }





    
}
