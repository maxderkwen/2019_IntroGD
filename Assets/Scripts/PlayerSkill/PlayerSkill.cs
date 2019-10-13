using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private PlayerControl playerControl;
    [SerializeField]
    private int skillGod = 0;
    [SerializeField]
    private Text godUI;

    [SerializeField]
    public Transform portalGate;
    private Transform recordNode;

    [SerializeField]
    private int skillTrans = 0;
    [SerializeField]
    private Text transUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (skillGod > 0)
            {
                skillGod--;
                StartCoroutine(callGodMode());
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            callPortal();
        }
    }
    private void FixedUpdate()
    {
        checkSkillsUI();
    }
    private IEnumerator callGodMode()
    {
        playerControl.GodMode = true;
        for (int i = 0; i < 10; i++)
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            transform.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        playerControl.GodMode = false;
    }


    private bool portalSet = false;
    private void callPortal()
    {
        print(portalGate.gameObject.activeInHierarchy);
        if (portalSet == false )
        {
            if (skillTrans > 0)
            {
                skillTrans--;
                portalSet = true;
                portalGate.gameObject.SetActive(true);
                recordNode = transform.GetComponent<PlayerControl>().currentNode;
                portalGate.position = transform.GetComponent<PlayerControl>().currentNode.position;
            }
        }else if( portalSet==true)
        {
            
            transform.position = portalGate.position;
            transform.GetComponent<PlayerControl>().nextNode = recordNode;
            transform.GetComponent<PlayerControl>().currentNode = recordNode;
            portalSet = false;
            portalGate.gameObject.SetActive(false);
        }

    }

    private void checkSkillsUI()
    {
        godUI.text = "GodMode: x"+skillGod;
        transUI.text = "Portal: x" + skillTrans;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "BigBeans")
        {
            int getSkill=(int)Random.Range(0, 2);
            if (getSkill == 0)
            {
                skillGod++;
            }
            else
            {
                skillTrans++;
            }
        }
    }
}
