using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private Transform currentNode;

    [SerializeField]
    private int currentPathNumber;

    [SerializeField]
    private List<Transform> path= new List<Transform>();

    [SerializeField]
    private Transform targetNode;

    private PathFingding newFind;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private float startTime;
    [SerializeField]
    private bool active = false;

    [SerializeField]
    private int moveAroundIndex;
    [SerializeField]
    private Transform[] moveAroundNode;

    [SerializeField]
    private PlayerControl player;

    private enum AImode {Green,Pink,Blue,Red }
    [SerializeField]
    private AImode _AIMode;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startMove());
        newFind= GetComponent<PathFingding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            if (currentNode == null)
            {
                if (Vector3.Distance(transform.position, startPos.position) < 0.2f)
                {
                    currentNode = startPos;
                }
                transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
            }
            else
            {
                if (path.Count > 0)
                {
                    checkCurrentPath();
                    checkPosition();
                    moving();
                    AImanage();
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
        if (currentPathNumber < path.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentPathNumber].position, speed * Time.deltaTime);
        }
    }

    void AImanage()
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

    void moveClockWise()
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

    
    private void moveAway()
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
            targetNode = NodeManager.nodes[(int)Random.Range(0, NodeManager.nodes.Count)];
        }
        else
        {
            targetNode = NodeManager.nodes[index];
        }
        if (targetNode == currentNode)
        {
            targetNode = NodeManager.nodes[(int)Random.Range(0, NodeManager.nodes.Count)];
        }

    }

    void chaseMove()
    {
        targetNode = player.currentNode;
        if (targetNode == currentNode)
        {
            targetNode = NodeManager.nodes[(int)Random.Range(0, NodeManager.nodes.Count)];
        }
    }

    IEnumerator startMove()
    {
        yield return new WaitForSeconds(startTime);
        active = true;
    }
}
