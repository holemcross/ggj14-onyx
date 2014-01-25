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

        if (text_population_all) text_population_all.text = "Global Population: " + resource.pop_all;
        if (text_population_us) text_population_us.text = "Our Follower: " + resource.pop_p1;
        if (text_population_them) text_population_us.text = "Enemy Follower: " + resource.pop_p2;
        if (text_mana) text_mana.text = "Our Holy Power: " + resource.p1_mana;

	}
}
