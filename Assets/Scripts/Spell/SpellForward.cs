using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellForward : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isObs;
    public Vector3 constraintPos;
    SpellController controller;
    float timeToExplo = 5f;
    float timeExist = 0f;


    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<SpellController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!isObs)
        {
            gameObject.transform.position += SpellController.instance.direcSpell;
        }
        else
        {
            timeExist += Time.deltaTime;
            if (Vector3.Distance(gameObject.transform.position, constraintPos) >= 0.2f)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, constraintPos, Time.deltaTime);
            }
            else
            {

                if (gameObject.transform.localScale.x < 0.5f && gameObject.transform.localScale.y <= 0.5f && gameObject.transform.localScale.z <= 0.5f)
                {
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + Time.deltaTime / 2, gameObject.transform.localScale.y + Time.deltaTime / 2, gameObject.transform.localScale.z + Time.deltaTime / 2);
                }

                if (timeExist >= timeToExplo)
                {
                    controller.darkObsSpawn = false;
                    timeExist = 0;
                    GameObject exp = Instantiate(controller.explosion, transform.position, Quaternion.identity);

                    //DESTROY ALL ZOMBIE
                    // RaycastHit[] hit = Physics.SphereCastAll(transform.position , 20f , transform.position , 100f , LayerMask.GetMask("Enemy"));
                    Collider[] hit = Physics.OverlapSphere(transform.position, 50f, LayerMask.GetMask("Enemy"));

                    if (hit.Length != 0)
                    {

                        for (int i = 0; i < hit.Length; i++)
                        {
                            hit[i].transform.gameObject.GetComponent<Enemy>().heath = 0;

                        }
                    }
                    Destroy(gameObject);
                    Destroy(exp, 2f);
                    
                }
            }

        }

    }
}
