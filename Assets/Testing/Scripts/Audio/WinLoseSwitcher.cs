using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinLoseSwitcher : MonoBehaviour
{
    public static WinLoseSwitcher instance;
    public UnityEvent gameLose = new UnityEvent();
    public UnityEvent gameWin = new UnityEvent();


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSound(int score)
    {
        if(score == 0)
        {
            gameLose.Invoke();
        }
        else
        {
            gameWin.Invoke();
        }
    }
}
