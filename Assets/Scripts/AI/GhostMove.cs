using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Ghost Move class
 * This class is handle all the Ghost Movement.
 * including 4 different type of Ghost AI.
 * **/
public class GhostMove : MonoBehaviour
{

    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private Transform currentNode;
    private int currentPathNumber;
    //using a list to record the path find by PathFinding class.
    private List<Transform> path= new List<Transform>();

    [SerializeField]
    private Transform targetNode;

    //use it to get the new Path for the movement;
    private PathFingding newFind;

    
    [SerializeField] //setting the start movement point of the Ghost
    private Transform startPos;
    [SerializeField] //setting the start moving time of the Ghost
    private float startTime;
    [SerializeField] //setting the ghost is active to move or not.
    private bool active = false;

    [SerializeField] //record the moving Around nodes index
    private int moveAroundIndex;
    [SerializeField] //setting the moving around nodes target point.
    private Transform[] moveAroundNode;

    [SerializeField] //record the player's PlayerControl component.
    private PlayerControl player;

    //enum to control 4 AI mode
    private enum AImode {Green,Pink,Blue,Red }
    [SerializeField]
    private AImode _AIMode; //setting AI mode

    [SerializeField]
    private NodeManager NodeManager; //using it to find the target node in all Nodes.

    void Start()
    {
        StartCoroutine(startMove());
        newFind= GetComponent<PathFingding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true) //if is active then Ghost can move
        {
            if (currentNode == null)
            {   //Those code is to move the Ghost to start position
                if (Vector3.Distance(transform.position, startPos.position) < 0.1f)
                {
                    currentNode = startPos;
                }
                transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
            }
            else
            { 
                if (path.Count > 0) //If it can find the path;
                {
                    checkCurrentPath(); //check current path is finished or not
                    checkPosition(); //checking it is move to next node or not
                    moving();  // Ghost position is change
                    AImanage(); //mange 4 different AI
                }
                else
                {
                    findPath(); 
                }

            }
        }
    }

    void checkCurrentPath()
    {
        if (currentPathNumber == path.Count)
        {
            path.Clear();
            currentPathNumber = 0;
        }
    }

    void findPath()
    {
        if (targetNode != null && currentNode != targetNode )
        {
            newFind.FindNodePath(currentNode, targetNode);
            path = newFind.FinalPath;
        }

    }

    void checkPosition()
    {
        if (path.Count > 0 && Vector3.Distance(transform.position, path[currentPathNumber].position) < 0.2f)
        {
            currentNode = path[currentPathNumber];
            currentPathNumber++;
        }
    }

    void moving()
    {
        if (currentPathNumber < path.Count) //make sure it has path to walk
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentPathNumber].position, speed * Time.deltaTime);
        }
    }

    void AImanage() // 4 AI mode to mange the Ghost
    {
        switch (_AIMode)
        {
            case AImode.Green:
                {
                    moveRandom();
                    break;
                }
            case AImode.Pink:
                {
                    moveClockWise();
                    break;
                }
            case AImode.Blue:
                {
                    moveAway();
                    break;
                }
            case AImode.Red:
                {
                    chaseMove();
                    break;
                }
        }

    }

    void moveRandom()
    {
        if (currentNode == targetNode)
        {
            targetNode = NodeManager.nodes[(int)Random.Range(0, NodeManager.nodes.Count)];
        }
    }

    void moveClockWise() //Using Transform[] moveAroundNode to move around
    {
        if (Vector3.Distance(currentNode.position, moveAroundNode[moveAroundIndex].position) < 0.1f)
        {
            moveAroundIndex++;
            if (moveAroundIndex == moveAroundNode.Length)
            {
                moveAroundIndex = 0;
            }
            targetNode = moveAroundNode[moveAroundIndex];

        }
    }

    
    private void moveAway() //Find  the farest  node away from the player, set it to targetnode
    {
        float maxDistance=0;
        int index=-1;
        for (int i = 0; i < NodeManager.nodes.Count; i++)
        {
            float distance = Vector3.Distance(player.currentNode.position, NodeManager.nodes[i].position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                index = i;
            }
        }
        if (NodeManager.nodes[index] == currentNode)
        {
            if (currentNode.GetComponent<Node>().up != null)
            {
                targetNode = currentNode.GetComponent<Node>().up;
            }
            if (currentNode.GetComponent<Node>().down != null)
            {
                targetNode = currentNode.GetComponent<Node>().down;
            }
            if (currentNode.GetComponent<Node>().left != null)
            {
                targetNode = currentNode.GetComponent<Node>().left;
            }
            if (currentNode.GetComponent<Node>().right != null)
            {
                targetNode = currentNode.GetComponent<Node>().right;
            }
        }
        else
        {
            targetNode = NodeManager.nodes[index];
        }
        if (targetNode == currentNode)
        {
            if (currentNode.GetComponent<Node>().up != null)
            {
                targetNode = currentNode.GetComponent<Node>().up;
            }
            if (currentNode.GetComponent<Node>().down != null)
            {
                targetNode = currentNode.GetComponent<Node>().down;
            }
            if (currentNode.GetComponent<Node>().left != null)
            {
                targetNode = currentNode.GetComponent<Node>().left;
            }
            if (currentNode.GetComponent<Node>().right != null)
            {
                targetNode = currentNode.GetComponent<Node>().right;
            }
        }

    }

    void chaseMove() //follow the player
    {
        targetNode = player.currentNode;

        if (targetNode == currentNode) //prevent the Ghost stop moving
        { 
            if (currentNode.GetComponent<Node>().up != null)
            {
                targetNode = currentNode.GetComponent<Node>().up;
            }
            if (currentNode.GetComponent<Node>().down != null)
            {
                targetNode = currentNode.GetComponent<Node>().down;
            }
            if (currentNode.GetComponent<Node>().left != null)
            {
                targetNode = currentNode.GetComponent<Node>().left;
            }
            if (currentNode.GetComponent<Node>().right != null)
            {
                targetNode = currentNode.GetComponent<Node>().right;
            }
        }

    }

    IEnumerator startMove()
    {
        yield return new WaitForSeconds(startTime);
        active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Nodes")
        {
            currentNode = other.transform;
        }
    }
}
