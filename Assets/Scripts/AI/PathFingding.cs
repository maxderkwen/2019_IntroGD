using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFingding : MonoBehaviour
{

    public List<Transform> OpenList= new List<Transform>();
    public List<Transform> ClosedList = new List<Transform>();

    public List<Transform> FinalPath=new List<Transform>();




    public void FindNodePath(Transform _startNode, Transform _endNode)
    {
        Transform startNode = _startNode;
        Transform targetNode = _endNode;

        List<Transform> openSet = new List<Transform>();
        HashSet<Transform> closedSet = new HashSet<Transform>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Transform node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].GetComponent<Node>().fCost < node.GetComponent<Node>().fCost || openSet[i].GetComponent<Node>().fCost == node.GetComponent<Node>().fCost)
                {
                    if (openSet[i].GetComponent<Node>().hCost < node.GetComponent<Node>().hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                continue;
            }

            foreach (Transform neighbour in NeighbourNode(node))
            {
                if ( closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.GetComponent<Node>().gCost + calculateManhattenDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.GetComponent<Node>().gCost || !openSet.Contains(neighbour))
                {
                    neighbour.GetComponent<Node>().gCost = newCostToNeighbour;
                    neighbour.GetComponent<Node>().hCost = calculateManhattenDistance(neighbour, targetNode);
                    neighbour.GetComponent<Node>().parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        //yield return null;
    }

    private List<Transform> tempList = new List<Transform>();
    private Node tempNode;
    List<Transform> NeighbourNode(Transform node)
    {
        tempList.Clear();
        tempNode = node.GetComponent<Node>();
        if (tempNode.up != null)
        {
            tempList.Add(tempNode.up);
        }
        if (tempNode.down != null)
        {
                tempList.Add(tempNode.down);
        }
        if (tempNode.left != null)
        {
                tempList.Add(tempNode.left);
        }
        if (tempNode.right != null)
        {
                tempList.Add(tempNode.right);
        }
        return tempList;
    }


    int calculateManhattenDistance(Transform node1,Transform node2)
    {
        if (node1 != null && node2 != null)
        {
            int x = (int)Mathf.Abs(node1.position.x - node2.position.x);//x1-x2
            int y = (int)Mathf.Abs(node1.position.y - node2.position.y);//y1-y2
            return x + y;//Return the sum
        }
        return -1;
    }

    void RetracePath(Transform _startNode, Transform _endNode)
    {
        List<Transform> path = new List<Transform>();
        Transform currentNode = _endNode;
        FinalPath.Clear();
        while (currentNode != _startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.GetComponent<Node>().parent;
        }
        path.Reverse();
        FinalPath = path;
    }

}
