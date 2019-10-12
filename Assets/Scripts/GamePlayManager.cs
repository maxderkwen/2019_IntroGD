using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    private BeansManager beansManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        Winning();
    }

    private void Winning()
    {
        if (beansManager.CheckBeans() == true)
        {
            Time.timeScale=0;
        }
    }
}
