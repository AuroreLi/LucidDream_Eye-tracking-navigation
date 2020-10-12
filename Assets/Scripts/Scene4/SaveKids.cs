using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveKids : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;

    private Valve.VR.EVRButtonId TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;//手柄扳机键

    private Valve.VR.EVRButtonId ApplicationMenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;//手柄菜单键

    private Valve.VR.EVRButtonId TochPadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;//手柄触摸板键

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObject.index);
        }
    }
    void Start()
    {
        //trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        if (controller.GetPress(TochPadButton))
        {
            Debug.Log("按住触摸板键");
        }
        if (controller.GetPressUp(TochPadButton))
        {
            Debug.Log("抬起触摸板键");
        }
        if (controller.GetPressDown(TriggerButton))
        {
            if (Vector3.Distance(transform.position,Player1.transform.position)<=1)
            {
                Player1.SetActive(false);
                PlayerController1.instance.ShowSaveText();
            }

            if (Vector3.Distance(transform.position,Player2.transform.position)<=1)
            {
                Player2.SetActive(false);
                PlayerController1.instance.ShowEnemy2();
            }

            if (Vector3.Distance(transform.position,Player3.transform.position) <= 1)
            {
                Player3.SetActive(false);
                Destroy(Player3);
                SceneManager.LoadScene("Scene3");
            }
            //Debug.Log("按下触摸板键");
        }
        //if (controller.GetPress(TriggerButton))
        //{
        //    Debug.Log("扣动扳机键");
        //}

        //if (controller.GetPressUp(TriggerButton))
        //{
        //    Debug.Log("松开扳机键");
        //}
        //后面其他手柄按键均可按照上面的方法添加使用
    }
}


