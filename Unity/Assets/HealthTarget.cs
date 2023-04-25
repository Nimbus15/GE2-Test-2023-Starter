using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTarget : MonoBehaviour
{
    public GameObject playerTarget;
    private string playerTag;
    private bool canHeal= true;
    private GameObject podGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canHeal == true)
        {
            podGO.GetComponent<PodController>().givePlayerHealth();
            canHeal = false;
        }
    }

    private void OnTriggerExit()
    {
        if (canHeal == false)
        {
            canHeal = true;
        }
    }
}
