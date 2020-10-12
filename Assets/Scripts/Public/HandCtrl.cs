using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCtrl : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private Valve.VR.EVRButtonId TriggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;//手柄扳机键
    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObject.index);
        }
    }
    public UnityEvent onTriggerClick;
    public UnityEvent onTouchUpPress;
    public UnityEvent onTouchDownPress;
    public UnityEvent onTouchLeftPress;
    public UnityEvent onTouchRightPress;
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (controller.GetPressDown(TriggerButton))
        {
            onTriggerClick?.Invoke();
        }
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 padAxis = device.GetAxis();
            //根据角度来判断上下左右四个范围 

            if (padAxis.y>0.5f)
            {
                onTouchUpPress?.Invoke();
               // Debug.Log("上");
            }
            if (padAxis.y < -0.5f)
            {
                onTouchDownPress?.Invoke();
               // Debug.Log("下");
            }
            if (padAxis.x < -0.5f)
            {
                onTouchLeftPress?.Invoke();
               // Debug.Log("左");
            }
            if (padAxis.x > 0.5f)
            {
                onTouchRightPress?.Invoke();
               // Debug.Log("右");
            }
        }

    }
  
}
