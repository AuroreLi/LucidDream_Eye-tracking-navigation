using DestroyIt;
using System.Collections;
using System.Collections.Generic;
using Tobii.G2OM;
using Tobii.XR;
using UnityEngine;
using UnityEngine.AI;

public class WitchMove : MonoBehaviour, IGazeFocusable
{
    public bool IsMove;
    public NavMeshAgent agent;
    public GameObject Player;
    public float DamagePerBlink = 20;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local);
            // The EyeBlinking bool is true when the eye is closed
            var isLeftEyeBlinking = eyeTrackingData.IsLeftEyeBlinking;
            var isRightEyeBlinking = eyeTrackingData.IsRightEyeBlinking;
            Debug.Log("BlinkRecv");
            if (isLeftEyeBlinking || isRightEyeBlinking)
                GetComponent<Destructible>().ApplyDamage(DamagePerBlink);
        }
       
        else
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsMove)
        {
            agent.SetDestination(Player.transform.position);
            if (Vector3.Distance(transform.position, Player.transform.position) <= 1.5f)
            {
                IsMove = false;
                PlayerController1.instance.ShowSB();
            }
        }
    }
}
