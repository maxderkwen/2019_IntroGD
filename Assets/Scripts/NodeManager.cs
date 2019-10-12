using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField]
    private Transform nodeParent;
    public static List<Transform> nodes=new List<Transform>();
    private void Awake()
    {
        for (int i = 0; i < nodeParent.childCount; i++)
        {
            nodes.Add(nodeParent.GetChild(i));
        }
    }

}
