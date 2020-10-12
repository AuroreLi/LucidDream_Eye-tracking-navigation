using System.Collections;
using System.Collections.Generic;
using Tobii.XR;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // Get eye tracking data in local space
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local);

        // The EyeBlinking bool is true when the eye is closed
        var isLeftEyeBlinking = eyeTrackingData.IsLeftEyeBlinking;
        var isRightEyeBlinking = eyeTrackingData.IsRightEyeBlinking;

        // Using gaze direction in local space makes it easier to apply a local rotation
        // to your virtual eye balls.
        var eyesDirection = eyeTrackingData.GazeRay.Direction;
    }
}
