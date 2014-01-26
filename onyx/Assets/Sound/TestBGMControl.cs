using UnityEngine;
using System.Collections;

public class TestBGMControl : MonoBehaviour {

    public BGMController bgm;

    private int cycle = 0;
    private string[] name = { "bgm_normal", "bgm_excite", "bgm_excite_more", "bgm_excite_most" };

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        cycle++;
        if (cycle == name.Length) cycle = 0;
        bgm.Transition(name[cycle]);
    }

}
