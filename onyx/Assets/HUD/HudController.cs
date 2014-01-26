using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

    public GameResources resource;

    public UILabel text_population_all;
    public UILabel text_population_us;
    public UILabel text_population_them;
    public UILabel text_mana;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		int pop_our = resource.pop_p1;
		int pop_them = resource.pop_p2;
		int mana = resource.p1_mana;
		
		if(Network.isClient) {
			pop_our = resource.pop_p2;
			pop_them = resource.pop_p1;
			mana = resource.p2_mana;
		}

        if (text_population_all) text_population_all.text = "Global Population: " + resource.pop_all;
        if (text_population_us) text_population_us.text = "Our Follower: " + pop_our;
        if (text_population_them) text_population_us.text = "Enemy Follower: " + pop_them;
        if (text_mana) text_mana.text = "Our Holy Power: " + mana + " - " + resource.p1_mana;

	}
}
