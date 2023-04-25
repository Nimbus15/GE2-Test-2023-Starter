using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject playerTarget;
    private Seek seekScript;
    private string playerTag;
    private bool canTakeDamage = false;
    private GameObject podGO;
    // Start is called before the first frame update
    void Start()
    {
        playerTag = "Player11";
        seekScript = GetComponent<Seek>();
        playerTarget = GameObject.FindGameObjectWithTag(playerTag);
        seekScript.target = playerTarget.transform.position;
        podGO = GameObject.FindGameObjectWithTag("Pod");

    }

    // Update is called once per frame
    void Update()
    {
        selfDestruction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canTakeDamage == true)
        {
            playerTarget.GetComponentInChildren<PodController>().givePlayerDamage();
            canTakeDamage = false;
        }
    }

    private void OnTriggerExit()
    {
        if (canTakeDamage == false)
        {
            canTakeDamage = true;
        }
    }

    private void selfDestruction()
    {
        Destroy(this, 20);
    }
}
