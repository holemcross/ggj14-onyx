using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    private float currentX = 0;
    public float desiredX = 0;

    private float maxCameraSpeed = 50f;
    private float maxCameraDistCap = 10f;

    private float panSpeed = 50f;

    public CameraFollowable lockingTo;
    private bool snapToLock = false;

	// Use this for initialization
	void Start () {
        lockingTo = null;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (lockingTo!=null)
        {
            desiredX = lockingTo.getCurrentX();
        }

        currentX = transform.position.x;

        float diffDist = desiredX - currentX;
        float absDiffDist = Mathf.Abs(diffDist);
        if (absDiffDist > 0.01)
        {
            // move your ass, camera man


            if (lockingTo != null && snapToLock)
            {
                transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);
            }
            else
            {
                if (absDiffDist > maxCameraDistCap)
                {
                    diffDist = diffDist / absDiffDist * maxCameraDistCap;
                }

                float percentDist = diffDist / maxCameraDistCap;

                if (diffDist > 0 && percentDist < 0.1f) percentDist = 0.1f;
                else if (diffDist < 0 && percentDist > -0.1f) percentDist = -0.1f;

                float movespeed = percentDist * maxCameraSpeed * Time.fixedDeltaTime;
                if (Mathf.Abs(movespeed) > absDiffDist) movespeed = diffDist;

                transform.position += new Vector3(movespeed, 0, 0);

                if (lockingTo != null && !snapToLock)
                {
                    if (absDiffDist - Mathf.Abs(movespeed) < 0.03f)
                    {
                        snapToLock = true;
                    }
                }

            }

        }
	}

    public void PanRight()
    {
        StopLockOn();
        desiredX += panSpeed * Time.fixedDeltaTime;
		//Debug.Log ("panning right");
    }

    public void PanLeft()
    {
        StopLockOn();
        desiredX -= panSpeed * Time.fixedDeltaTime;
		//Debug.Log ("panning left");
    }

    public void LockOn(CameraFollowable c)
    {
        // add locked on target, update desiredX every update
        lockingTo = c;
        snapToLock = false;
    }

    public void StopLockOn()
    {
        // remove locked on target, set desiredX to currentX
        lockingTo = null;
        desiredX = currentX;
    }

    public bool IsLocking()
    {
        return (lockingTo != null);
    }

}
