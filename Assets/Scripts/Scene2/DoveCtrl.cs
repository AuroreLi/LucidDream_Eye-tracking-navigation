using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoveCtrl : MonoBehaviour
{
    public AudioSource source;
    public UnityEvent onDoveClick;
    public int sceneIndex = 2;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.AddListener(OnDoveClick);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            other.transform.parent.GetComponent<HandCtrl>().onTriggerClick.RemoveListener(OnDoveClick);
        }
    }

    private void OnDoveClick()
    {
        onDoveClick?.Invoke();
        StopSource();
    }
    public void PlaySource()
    {
        source.Play();
    }
    public void StopSource()
    {
        source.Stop();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
