using UnityEngine;
using System.Collections;

public class TestCameraLockOn : MonoBehaviour {

    public PlayerCamera camera;
    public CameraFollowable tofollow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!camera.IsLocking()) transform.GetComponent<UILabel>().text = "Lock On";
        else transform.GetComponent<UILabel>().text = "Unlock";
	}

    void OnClick()
    {
        if (!camera.IsLocking()) camera.LockOn(tofollow);
        else camera.StopLockOn();
    }

}
