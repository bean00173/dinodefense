using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DevConsole : MonoBehaviour
{
    public TMP_InputField textBox;
    bool visible;

    public List<string> commands = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        textBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (visible)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (ValidCommand(textBox.text))
                {
                    RunCommand(textBox.text);
                }
                else
                {
                    Debug.Log("Invalid command");
                }
                textBox.text = "";
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                textBox.gameObject.SetActive(false);
                visible = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                textBox.gameObject.SetActive(true);
                visible = true;
            }
        }
    }

    public bool ValidCommand(string command)
    {
        foreach(string cm in commands)
        {
            if(cm == command)
            {
                return true;
            }
        }

        return false;
    }

    public void RunCommand(string command)
    {
        switch (command)
        {
            case "unlockall": GameManager.instance.UnlockAll(); break;
            case "3star": LevelManager.instance.SimFinished(true, 3); break;
            case "motherlode": LevelManager.instance.Spend(-50000); break;
            default: Debug.Log("Action does not yet have function"); break;
        }
    }
}
