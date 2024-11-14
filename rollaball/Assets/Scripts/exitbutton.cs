using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void doExitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
