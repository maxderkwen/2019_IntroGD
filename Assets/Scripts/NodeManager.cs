using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is to record all the Nodes. And used by other component,
 * **/
public class NodeManager : MonoBehaviour
{
    [SerializeField]
    private Transform nodeParent;
    public List<Transform> nodes=new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < nodeParent.childCount; i++)
        {
            nodes.Add(nodeParent.GetChild(i));
        }
    }

}
