using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseUICtrl : MonoBehaviour
{
    public GameObject tip;
    public CanvasGroup backGround;
    public AudioSource bgSource;
    private bool openBG;
    private bool openTip;
    private float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openTip)
        {
            if (timer < 3f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                openTip = false;
                tip.SetActive(false);
            }
        }

        if (openBG)
        {
            if (backGround.alpha < 0.8f)
            {
                backGround.alpha += Time.deltaTime / 10f;
            }
            else
            {
                openBG = false;
                LoadScene();
            }
        }
    }

    public void OpenBG()
    {
        openBG = true;
        backGround.gameObject.SetActive(true);
        bgSource.Play();
    }
    public void OpenTip()
    {
        openTip = true;
        tip.SetActive(true);
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}
