using UnityEngine;
using System;
using System.Collections;

public class SceneManager : MonoBehaviour {
    public GameObject [] cubicals;
    public GameObject [] toilets;
    public GameObject [] effects;

    public int change_scene_lower;
    public int change_scene_upper;

    public GameObject Eyelid;

    public float blink_time;

    private float time_in_scene = 0;
    private int scene_change_time = 0;

    private GameObject active_cubical;
    private GameObject active_toilet;
    private GameObject active_effect;

	// Use this for initialization
	void Start () {
        if (change_scene_lower >= change_scene_upper)
        {
            change_scene_upper = change_scene_lower + 30;
        }

        System.Random rng = new System.Random();
        scene_change_time = rng.Next(change_scene_lower, change_scene_upper + 1);

        active_cubical = cubicals.Length > 0 ? Instantiate(cubicals[0]) : null;
        active_toilet = toilets.Length > 0 ? Instantiate(toilets[0]) : null;
        active_effect = effects.Length > 0 ? Instantiate(effects[0]) : null;
	}
	
	// Update is called once per frame
	void Update () {
        time_in_scene += Time.deltaTime;
        if (time_in_scene < (float)scene_change_time)
        {
            return;
        }

        System.Random rng = new System.Random();

        time_in_scene = 0;
        scene_change_time = rng.Next(change_scene_lower, change_scene_upper + 1);

        int num_cubicals = cubicals.Length;
        int num_toilets = toilets.Length;
        int num_effects = effects.Length;

        bool scene_changed = num_cubicals > 1 && num_toilets > 1 && num_effects > 1;

        if(!scene_changed)
        {
            //return;
        }

        StartCoroutine(playBlinkAnimation());
	}

    IEnumerator playBlinkAnimation()
    {
        Animator eyelid_animator = Eyelid.GetComponent<Animator>();

        eyelid_animator.SetBool("Blink", true);
        yield return new WaitForSeconds(blink_time);
        eyelid_animator.SetBool("Blink", false);

        System.Random rng = new System.Random();

        int num_cubicals = cubicals.Length;
        int num_toilets = toilets.Length;
        int num_effects = effects.Length;

        if (num_cubicals > 0)
        {
            int selected_cubical = rng.Next(0, num_cubicals);

            Destroy(active_cubical);
            active_cubical = Instantiate(cubicals[selected_cubical]);
        }

        if (num_toilets > 0)
        {
            int selected_toilet = rng.Next(0, num_toilets);
            Debug.Log(selected_toilet);

            Destroy(active_toilet);
            active_toilet = Instantiate(toilets[selected_toilet]);
        }

        if (num_effects > 0)
        {
            int selected_effect = rng.Next(0, num_effects);

            Destroy(active_effect);
            active_effect = Instantiate(effects[selected_effect]);
        }
    }
}