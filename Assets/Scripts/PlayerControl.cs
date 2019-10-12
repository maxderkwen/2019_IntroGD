﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed=2.5f;
    [SerializeField]
    public Transform currentNode;
    [SerializeField]
    private Transform nextNode;
    public MoveDir currentDir;
    public enum MoveDir {up,down,left,right}

    private float distanceNext=-1f;
    private float distanceCurr=-1f;


    // Start is called before the first frame update
    void Start()
    {
        currentDir = MoveDir.up;
    }

    // Update is called once per frame
    private void Update()
    {
        calDis();
        checkNextNode();
        checkInput();
        moving();
        checkAnimation();

    }

    private void calDis()
    {
        if (nextNode!=null)
        {
            distanceNext = Vector3.Distance(transform.position, nextNode.position);
        }
        if (currentNode != null)
        {
            distanceCurr = Vector3.Distance(transform.position, currentNode.position);
        }
    }
    private void checkNextNode()
    {
        if (distanceNext > -1 && distanceNext < 0.1f )
        {
            currentNode = nextNode;
            if (currentDir == MoveDir.up)
            {
                if (currentNode.GetComponent<Node>().up != null)
                {
                    nextNode = currentNode.GetComponent<Node>().up;
                }
             }
            if (currentDir == MoveDir.down)
            {
                if (currentNode.GetComponent<Node>().down != null)
                {
                    nextNode = currentNode.GetComponent<Node>().down;
                }
            }
            if (currentDir == MoveDir.left)
            {
                if (currentNode.GetComponent<Node>().left != null)
                {
                    nextNode = currentNode.GetComponent<Node>().left;
                }
            }
            if (currentDir == MoveDir.right)
            {
                if (currentNode.GetComponent<Node>().right != null)
                {
                    nextNode = currentNode.GetComponent<Node>().right;
                }
            }
        }
        

    }
    private void checkInput()
    {
        if (distanceCurr > -1 && distanceCurr > 0.5f)
        {
            switch (currentDir)
            {
                case MoveDir.up:
                    {
                        if (Input.GetKeyDown(KeyCode.S))
                        {
                            Transform temp;
                            temp = currentNode;
                            currentNode = nextNode;
                            nextNode = temp;
                            currentDir = MoveDir.down;
                        }
                        break;
                    }
                case MoveDir.down:
                    {
                        if (Input.GetKeyDown(KeyCode.W))
                        {
                            Transform temp;
                            temp = currentNode;
                            currentNode = nextNode;
                            nextNode = temp;
                            currentDir = MoveDir.up;
                        }
                        break;
                    }
                case MoveDir.left:
                    {
                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            Transform temp;
                            temp = currentNode;
                            currentNode = nextNode;
                            nextNode = temp;
                            currentDir = MoveDir.right;
                        }
                        break;
                    }
                case MoveDir.right:
                    {
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            Transform temp;
                            temp = currentNode;
                            currentNode = nextNode;
                            nextNode = temp;
                            currentDir = MoveDir.left;
                        }
                        break;
                    }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentDir = MoveDir.up;
                if (currentNode.GetComponent<Node>().up != null)
                {
                    nextNode = currentNode.GetComponent<Node>().up;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentDir = MoveDir.down;
                if (currentNode.GetComponent<Node>().down != null)
                {
                    nextNode = currentNode.GetComponent<Node>().down;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentDir = MoveDir.left;
                if (currentNode.GetComponent<Node>().left != null)
                {
                    nextNode = currentNode.GetComponent<Node>().left;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentDir = MoveDir.right;
                if (currentNode.GetComponent<Node>().right != null)
                {
                    nextNode = currentNode.GetComponent<Node>().right;
                }
            }
        }
    }
    private void moving()
    {
        
        if (nextNode != null )
        {
            transform.position = Vector3.MoveTowards(transform.position, nextNode.position, speed * Time.deltaTime);
            checkRotation();
        }
    }
    private void checkRotation()
    {
        if (currentDir == MoveDir.up )
        {
            if (nextNode!=null && currentNode != nextNode)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (currentDir == MoveDir.down)
        {
            if (nextNode != null && currentNode != nextNode)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }

        if (currentDir == MoveDir.left)
        {
            if (nextNode != null && currentNode != nextNode)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }

        if (currentDir == MoveDir.right)
        {
            if (nextNode != null && currentNode != nextNode)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }
    private void checkAnimation()
    {
        if (currentNode == nextNode || nextNode == null)
        {
            transform.GetComponent<Animator>().speed=0;
        }
        else
        {
            transform.GetComponent<Animator>().speed = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.tag);
        if (other.transform.tag=="Nodes")
        {
            currentNode = other.transform;
        }
        if (other.transform.tag == "Beans" )
        {
            other.gameObject.SetActive(false);
        }
        if (other.transform.tag == "BigBeans")
        {
            other.gameObject.SetActive(false);
        }
    }

    public void SetCurrentNode(Transform t)
    {
        currentNode = t;
    }

    public void SetNextNode(Transform t)
    {
        nextNode = t;
    }

    public MoveDir GetMoveDir()
    {
        return currentDir;
    }
}
