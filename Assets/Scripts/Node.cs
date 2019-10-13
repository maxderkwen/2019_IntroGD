using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField]
    public Transform up;
    [SerializeField]
    public Transform down;
    [SerializeField]
    public Transform left;
    [SerializeField]
    public Transform right;

    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public Transform parent;
    // Start is called before the first frame update
    void Awake()
    {
        GetAroundNode();
    }

    private void GetAroundNode()
    {
        transform.GetComponent<SphereCollider>().enabled = false;
        if (Physics.Raycast(transform.position, Vector3.up*5, out hit))
        {
            if (hit.transform != null && hit.transform.tag == "Nodes")
            {
                up = hit.transform;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.down * 5, out hit))
        {
            if (hit.transform != null && hit.transform.tag == "Nodes")
            {
                down = hit.transform;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.left * 5, out hit))
        {
            if (hit.transform != null && hit.transform.tag == "Nodes")
            {
                left = hit.transform;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right * 5, out hit))
        {
            if (hit.transform != null && hit.transform.tag == "Nodes")
            {
                right = hit.transform;
            }
        }
        transform.GetComponent<SphereCollider>().enabled = true;
    }


}


