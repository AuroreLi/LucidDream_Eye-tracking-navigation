using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightCtrl : MonoBehaviour
{
    public List<GameObject> lights;
    public UnityEvent onOpenLight;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.AddListener(OpenLight);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.RemoveListener(OpenLight);
        }
    }


    private void OpenLight()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(true);
        }
        onOpenLight?.Invoke();

    }
}
