﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTrans : MonoBehaviour
{
    private Node thisNode;
    [SerializeField]
    private Transform target;
    private Node targetNode;
    // Start is called before the first frame update
    void Start()
    {
        thisNode = GetComponent<Node>();
        targetNode = target.GetComponent<Node>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerControl otherControl = other.GetComponent<PlayerControl>();
            otherControl.SetCurrentNode(target);
            if (otherControl.GetMoveDir() == PlayerControl.MoveDir.left)
            {
                otherControl.SetNextNode(targetNode.left);
            }
            if (otherControl.GetMoveDir() == PlayerControl.MoveDir.right)
            {
                otherControl.SetNextNode(targetNode.right);
            }
            other.transform.position = target.position;
        }
    }

}
