using System.Collections;
using System.Collections.Generic;
using Tobii.XR;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WalkingPlace : MonoBehaviour
{
    public Transform head;
    private CharacterController _characterController;
    [SerializeField] private GameObject _lefthand;
    [SerializeField] private GameObject _righthand;

    private Vector3 _previousPosLeft;
    private Vector3 _previousPosRight;
    private Vector3 _direction;

    Vector3 _gravity = new Vector3(0, 9.8f, 0);

    public float _speed = 10;

    void Start()
    {
        //Setting the pos at the start of the game  
        SetPreviousPos();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /* 
         * Calculating the velocity of the player hand movement 
         * Left hand velocity = left hand current pose - left hand precious pose 
         * Right hand velocity = right current pose - right hand precious pose 
         * total velocity =+ magnitude of Left hand velocity + Magnitude of hand velocity 
         *  
         * Move the player based on the total velocity 
         */
        Vector3 _leftVelocity = _lefthand.transform.position - _previousPosLeft;
        Vector3 _rightVelocity = _righthand.transform.position - _previousPosRight;
        float _totalVelocity = _rightVelocity.magnitude * .8f + _leftVelocity.magnitude * .8f;

        if (_totalVelocity > .05f)
        {
            var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
            if (eyeTrackingData.GazeRay.IsValid)
            {
                _direction = eyeTrackingData.GazeRay.Direction;
            }
            else
            {
                //Getting the direction that player is facing  
                _direction = head.forward;//TransformDirection(new Vector3(0, 0, 1));
            }
            //Moving the player using character controller    
            _characterController.Move(_speed * Time.deltaTime * Vector3.ProjectOnPlane(_direction, Vector3.up));
        }
        //Applying Gravity to the player  
        _characterController.Move(-_gravity * Time.deltaTime);
        SetPreviousPos();
    }
    private void SetPreviousPos()
    {
        _previousPosLeft = _lefthand.transform.position;
        _previousPosRight = _righthand.transform.position;
    }
}

