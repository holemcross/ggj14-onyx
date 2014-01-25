using UnityEngine;
using System.Collections;

public class BGMController : MonoBehaviour {
	
	AudioSource[] audios;
	
	private int currentIndex;
	
	private int nextIndex;
	private float transitionTimer;
	private float transitionMaxTime;
	
	private float defaultTransitionTime = 3f;
	
	// Use this for initialization
	void Start () {
	
		audios = transform.GetComponentsInChildren<AudioSource>();
		// forcing loop
		for(int i=0;i<audios.Length;i++) {
			audios[i].loop = true;
			audios[i].Stop ();
		}
		
		// start song
		currentIndex = getIndex ("bgm_normal");
		
		// no transition now
		nextIndex = -1;
		transitionTimer = 0f;
		transitionMaxTime = 0f;
		
		audios[currentIndex].Play ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(nextIndex>=0) {
			transitionTimer += Time.fixedDeltaTime;
			if(transitionTimer>=transitionMaxTime) {
				audios[currentIndex].Stop ();
				currentIndex = nextIndex;
				audios[currentIndex].volume = 1f;
				nextIndex = -1;
				transitionTimer = 0f;
				transitionMaxTime = 0f;
			} else {
				// linear interpolation (is it usable with sound volume?)
				audios[currentIndex].volume = 1f - (transitionTimer/transitionMaxTime);	
				audios[nextIndex].volume = (transitionTimer/transitionMaxTime);
			}
		}
	}
	
	public void Play(string aname) {
		audios[currentIndex].Stop();
		currentIndex = getIndex(aname);
		audios[currentIndex].volume = 1f;
		audios[currentIndex].Play();
		nextIndex = -1;
		transitionTimer = 0f;
		transitionMaxTime = 0f;
	}
	
	public void Transition(string aname) {
		Transition (aname,defaultTransitionTime);
	}
	
	public void Transition(string aname, float ttime) {
		if(nextIndex>=0) return;	// transition in progress
		transitionMaxTime = ttime;
		transitionTimer = 0;
		nextIndex = getIndex(aname);
		audios[nextIndex].volume = 0f;
		audios[nextIndex].Play();
	}
	
	private int getIndex(string aname) {
		for(int i=0;i<audios.Length;i++) {
			if(audios[i].name == aname) return i;
		}
		return 0;
	}
	
	public bool IsTransitioning() {
		if(nextIndex>=0) return true;
		return false;
	}
	
}
