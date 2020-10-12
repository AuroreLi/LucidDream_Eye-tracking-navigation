using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowCtrl : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.AddListener(PlayAnim);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.RemoveListener(PlayAnim);
        }
    }

    private void PlayAnim()
    {
        anim.SetBool("Play",true);
    }
}
