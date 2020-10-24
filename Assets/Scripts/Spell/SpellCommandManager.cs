using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpellCommandManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpellCommandManager instance;

    public static string currentCommand;

    [System.Serializable]
    public struct command
    {
        public string nameSpell;
        public string[] listCommand;
    }


    public command[] commands;

    public Text result;

    public bool canRec = true;

    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentCommand = result.text;
    }

    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ResetRecognize()
    {
        canRec = true;
    }
}
