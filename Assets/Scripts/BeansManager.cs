using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeansManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform smallBeans;
    [SerializeField]
    private Transform bigBeans;


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
