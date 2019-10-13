using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *BeansManager
 * is a class to manage all the Beans in the gamePlay
 *   
 */
public class BeansManager : MonoBehaviour
{
    
    [SerializeField]
    private Transform smallBeans;
    [SerializeField]
    private Transform bigBeans;

    //Active all the beans in the scene.
    //Remeber to active all the beans after all the Nodes are ready.
    void Start()
    {
        for (int i = 0; i < smallBeans.childCount; i++)
        {
            smallBeans.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < bigBeans.childCount; i++)
        {
            bigBeans.GetChild(i).gameObject.SetActive(true);
        }
    }

    //Check all the beans is disable. And return the result.
    public bool CheckBeans()
    {
        for (int i = 0; i < bigBeans.childCount; i++)
        {
            if (bigBeans.GetChild(i).gameObject.activeSelf == true)
            {
                return false;
            }
        }
        for (int i = 0; i < smallBeans.childCount; i++)
        {
            if (smallBeans.GetChild(i).gameObject.activeSelf == true)
            {
                return false;
            }
        }

        return true;
    }


}
