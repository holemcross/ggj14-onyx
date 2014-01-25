using UnityEngine;
using System.Collections;

public class PlayerCameraPanner : MonoBehaviour {

    public PlayerCamera camera;
    public bool rightPan = true;

    private bool isHovering = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isHovering)
        {
            if (rightPan) camera.PanRight();
            else camera.PanLeft();
        }
	}

    void OnHover(bool isOver)
    {
        isHovering = isOver;
    }

}
