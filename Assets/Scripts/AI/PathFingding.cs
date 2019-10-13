using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * This class is a traditional A-star algorithm Pathfinding class
 * using the Nodes in the game play scene as the grid to find the road.
 *
 * OpenSet to record the nodes which are ready to calculated.
 * CloseSet to record the nodes finished calculated.
 * 
 * 
 * **/
public class PathFingding : MonoBehaviour
{
    //record the final result path, used by other component.
    public List<Transform> FinalPath=new List<Transform>();
    public void FindNodePath(Transform _startNode, Transform _endNode)
    {
        Transform startNode = _startNode;
        Transform targetNode = _endNode;

        List<Transform> openSet = new List<Transform>();
        HashSet<Transform> closedSet = new HashSet<Transform>();
        openSet.Add(startNode);

        while (openSet.Count > 0) //make sure it has the start Point
        {
            Transform node = openSet[0];
            for (int i = 1; i < openSet.Count; i++) //find the lowest cost in openSet
            {
                if (openSet[i].GetComponent<Node>().fCost < node.GetComponent<Node>().fCost || openSet[i].GetComponent<Node>().fCost == node.GetComponent<Node>().fCost)
                {
                    if (openSet[i].GetComponent<Node>().hCost < node.GetComponent<Node>().hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node); //while finish calculated, and choose it as next node, remove from openSet add to closeSet
            closedSet.Add(node); 

            if (node == targetNode) //if it is  the  final node
            {
                RetracePath(startNode, targetNode); //Retrace, to make it usable for other game object
                continue;
            }

            foreach (Transform neighbour in NeighbourNode(node)) //find all the nearby node cost.
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
    } //To find the nearby node


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
