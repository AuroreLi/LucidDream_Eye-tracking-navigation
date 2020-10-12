using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController1 : MonoBehaviour
{
    public static PlayerController1 instance;
    public GameObject ShowCatch;
    public GameObject ShowSave;
    public AudioSource audio;

    public GameObject StartShow;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy10;
    public GameObject enemy11;
    public GameObject enemy12;
    public GameObject enemy13;
    public GameObject SaveAnother1;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CloseStartShow",3);
    }

    public void CloseStartShow()
    {
        StartShow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowEnemy2()
    {
        SaveAnother1.SetActive(true);
        enemy2.GetComponent<WitchMove>().IsMove = true;
        enemy3.GetComponent<WitchMove>().IsMove = true;
        Invoke("CloseOther2", 2);
    }

    public void CloseOther2()
    {
        SaveAnother1.SetActive(false);
    }


    public void ShowSaveText()
    {
        ShowSave.SetActive(true);
        Invoke("CloseSave", 9);
        enemy1.GetComponent<WitchMove>().IsMove = true;
        enemy4.GetComponent<WitchMove>().IsMove = true;
        enemy5.GetComponent<WitchMove>().IsMove = true;
        enemy6.GetComponent<WitchMove>().IsMove = true;
        enemy7.GetComponent<WitchMove>().IsMove = true;
        enemy8.GetComponent<WitchMove>().IsMove = true;
        enemy9.GetComponent<WitchMove>().IsMove = true;
        enemy10.GetComponent<WitchMove>().IsMove = true;
        enemy11.GetComponent<WitchMove>().IsMove = true;
    }

    public void CloseSave()
    {
        ShowSave.SetActive(false);
    }


    public void ShowSB()
    {
        ShowCatch.SetActive(true);
        //PlayTheAudio
        audio.Play();
        Invoke("Closesb",2);
    }

    public void Closesb()
    {
        ShowCatch.SetActive(false);
        //Load scene
        SceneManager.LoadScene("4");
    }


}
