using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorEvent : MonoBehaviour
{
    public int sceneIndex=1;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.AddListener(LoadScene);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.RemoveListener(LoadScene);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
