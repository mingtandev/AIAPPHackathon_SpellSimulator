using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    //REFERENCE
    Player player;
    
    public TextMeshProUGUI healUI;
    void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healUI.text = "Heal : " + player.heal.ToString();
    }
}
