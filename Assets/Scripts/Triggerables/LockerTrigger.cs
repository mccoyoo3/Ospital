using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerTrigger : MonoBehaviour
{
    private Animator lockerAnim;

    private void Start()
    {
        lockerAnim = gameObject.transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            lockerAnim.SetBool("opening", false);
        }
    }
}
