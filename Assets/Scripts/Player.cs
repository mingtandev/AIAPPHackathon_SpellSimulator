using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsDeath = false;
    public int heal = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (heal <= 0)
        {
            IsDeath = true;
            heal = 0;
        }

    }
}
