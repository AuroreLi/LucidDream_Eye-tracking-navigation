using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Tobii.XR;
using VRTK;
using System.IO;
using UnityEngine.UI;
public class MyBootstrapper : MonoBehaviour
{
    void Awake()
    {
        var settings = new TobiiXR_Settings();
        TobiiXR.Start(settings);
    }
}
public class Move : MonoBehaviour
{
    public float moveSpeed = 1;
    public float minSpeed = 0.1f;
    public float maxSpeed = 1;
    public Transform head;

    private CharacterController character;
    public bool move;
    private float offsetSpeed;

    public UnityEvent onMove;
    public UnityEvent onStopMove;
    /// <summary>
    /// angle between eye-tracking direction and camera forward direction
    /// </summary>
    public float angle;
    /// <summary>
    /// Move Controll Mode :  hmd/EyeTracking
    /// </summary>
    public ControllMode controllMode;
    /// <summary>
    /// HandCtrl Script of Left hand
    /// </summary>
    public HandCtrl leftHandCtrl;
    /// <summary>
    /// HandCtrl Script of Right hand
    /// </summary>
    public HandCtrl rightHandCtrl;
    /// <summary>
    /// ControllMode Tip Text Of Left Hand
    /// </summary>
    public Text leftHandModeText;
    /// <summary>
    /// ControllMode Tip Text Of Right Hand
    /// </summary>
    public Text rightHandModeText;
    /// <summary>
    /// List to save recorded angle data
    /// </summary>
    List<string> angleData = new List<string>();
    /// <summary>
    /// whether is recoding data
    /// </summary>
    bool IsRecordingData;
    /// <summary>
    /// recode txt save path
    /// </summary>
    public string txtPath;
    /// <summary>
    /// record angle frequency
    /// </summary>
    public float recordInterval = 1f;
    float timer;
    void Start()
    {
        character = GetComponent<CharacterController>();
        leftHandCtrl.onTouchLeftPress.AddListener(OnSwitch2HmdMode);
        leftHandCtrl.onTouchRightPress.AddListener(OnSwitch2EyeTrackingMode);
        rightHandCtrl.onTouchLeftPress.AddListener(OnSwitch2HmdMode);
        rightHandCtrl.onTouchRightPress.AddListener(OnSwitch2EyeTrackingMode);
    }
    public void OnMove()
    {
        move = !move;
        if (move)
        {
            onMove?.Invoke();
        }
        else
        {
            onStopMove?.Invoke();
        }
    }
    public void AddSpeed()
    {
        moveSpeed += Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed, minSpeed, maxSpeed);
    }
    public void ReduceSpeed()
    {
        moveSpeed -= Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed, minSpeed, maxSpeed);
    }
    public void SetOffsetSpeed(float _speed)
    {
        offsetSpeed = _speed;
    }
    void OnSwitch2EyeTrackingMode()
    {
        ShowModePanel(true);
        CancelInvoke();
        controllMode = ControllMode.EyeTracking;
        leftHandModeText.text = "EyeTracking";
        rightHandModeText.text = "EyeTracking";
        leftHandModeText.transform.parent.parent.localPosition = new Vector3(0.25f, 0, 0);
        rightHandModeText.transform.parent.parent.localPosition = new Vector3(0.25f, 0, 0);
        Invoke("HideModePanel", 5);
    }
    void OnSwitch2HmdMode()
    {
        ShowModePanel(true);
        CancelInvoke();
        controllMode = ControllMode.Hmd;
        leftHandModeText.text = "hmd";
        rightHandModeText.text = "hmd";
        leftHandModeText.transform.parent.parent.localPosition = new Vector3(-0.25f, 0, 0);
        rightHandModeText.transform.parent.parent.localPosition = new Vector3(-0.25f, 0, 0);
        Invoke("HideModePanel", 5);
    }
    void HideModePanel()
    {
        ShowModePanel(false);
    }
    /// <summary>
    /// Show Controll Mode Tip Panel
    /// </summary>
    /// <param name="_isShown"></param>
    void ShowModePanel(bool _isShown)
    {
        leftHandModeText.transform.parent.parent.gameObject.SetActive(_isShown);
        rightHandModeText.transform.parent.parent.gameObject.SetActive(_isShown);
    }
    /// <summary>
    /// save recorded angle data to local txt file
    /// </summary>
    void OnSaveTxt()
    {
        txtPath = Application.streamingAssetsPath + "/AngleRecord" + System.DateTime.Now.ToLongTimeString().Replace(':', '_') + ".txt";
        string DataString = "";
        foreach (var data in angleData)
        {
            DataString += data + "\n";
        }
        File.WriteAllText(txtPath, DataString);
        //Debug.Log("保存文件成功");
    }
    private void OnApplicationQuit()
    {
        OnSaveTxt();
    }
    void Update()
    {
        if (!Camera.main)
        {
            return;
        }
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        if (move)
        {
            IsRecordingData = true;
            if (eyeTrackingData.GazeRay.IsValid)
            {
                if (controllMode.Equals(ControllMode.EyeTracking))
                {
                    var rayDirection = eyeTrackingData.GazeRay.Direction;
                    character.SimpleMove(rayDirection * (moveSpeed + offsetSpeed));
                    //Debug.Log("EyeTracking");
                }
                else
                {
                    character.SimpleMove(head.forward * (moveSpeed + offsetSpeed));
                    //Debug.Log("hmd");
                }
            }
            
         
        }
        else
        {
            IsRecordingData = false;
            character.SimpleMove(head.forward * offsetSpeed);
        }
        //when the signed angle from head forward diretion to eyeTracking direction is larger than 0,当夹角大于0，判断眼睛看的方向在camera ray右边，然后通过Quaternion算夹角度数。
        bool IsRight = Vector3.SignedAngle(head.forward, eyeTrackingData.GazeRay.Direction.normalized, Vector3.up) > 0;
        angle = (IsRight ? 1 : -1) * Quaternion.Angle(head.rotation, Quaternion.LookRotation(eyeTrackingData.GazeRay.Direction, Vector3.up));
        //Debug.Log("Angle " + angle);
        
        if (IsRecordingData)
        {   //
            timer += Time.deltaTime;
            if (timer > recordInterval&& Mathf.Abs(angle)<90)
            {
                timer = 0;
                //save current angle data to Data List
                angleData.Add(angle.ToString("F3"));
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            IsRecordingData = true;
           // Debug.Log("开始记录");
            //angleData.Clear();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Debug.Log("停止记录");
            IsRecordingData = false;
            OnSaveTxt();
        }
    }
}
/// <summary>
/// 控制方式
/// </summary>
public enum ControllMode
{
    EyeTracking,
    Hmd
}