using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class to control the player movement
 * Using the current Node to find the up,down,left,right node. 
 * And using moveDir to check the move direction, then choose the Node
 *  Input.GetKeyDown() to change the move direction.
 * 
 * When the player is moving near to the target node, the target node will become current node
 * Then the target node will change to next same moveDir Node according to Node.cs class
 * **/
public class PlayerControl : MonoBehaviour
{
    
    [SerializeField]
    private float speed=2.5f;
    [SerializeField]
    private Transform startNode;
    [SerializeField]
    public Transform currentNode;
    [SerializeField]
    public Transform nextNode;
    public MoveDir currentDir;
    public enum MoveDir {up,down,left,right}

    [SerializeField]
    GamePlayManager gamePlayManager;

    private AudioSource musicPlay;
    [SerializeField]
    private AudioClip[] music;
    [SerializeField]
    private bool godMode=false;
    public bool GodMode { get { return godMode; } set { godMode = value; } }
    private float distanceNext=-1f;
    private float distanceCurr=-1f;


    // Start is called before the first frame update
    void Start()
    {
        currentDir = MoveDir.up;
        musicPlay = GetComponent<AudioSource>();
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
            if (!musicPlay.isPlaying)
            {
                musicPlay.Stop();
                musicPlay.loop = false;
                musicPlay.clip = music[1];
                musicPlay.Play();
            }
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
                gamePlayManager.Score += 100;
                musicPlay.Stop();
                musicPlay.loop = false;
                musicPlay.clip = music[0];
                musicPlay.Play();

            other.gameObject.SetActive(false);
        }
        if (other.transform.tag == "BigBeans")
        {
                gamePlayManager.Score += 500;
                musicPlay.Stop();
                musicPlay.loop = false;
                musicPlay.clip = music[0];
                musicPlay.Play();

            other.gameObject.SetActive(false);
        }
        if (other.transform.tag == "Ghosts")
        {
            if (godMode == false)
            {
                gamePlayManager.Health--;
                transform.position = startNode.position;
                currentNode = startNode;
                nextNode = startNode;
                currentDir = MoveDir.up;
                StartCoroutine(Reborn());
            }
        }
    }

    private IEnumerator Reborn()
    {
        godMode = true;
        for (int i = 0; i < 10; i++)
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            transform.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        godMode = false;
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
