using UnityEngine;
using System;
using System.Collections;

public class SceneManager : MonoBehaviour {
    public GameObject [] levels;

    public int change_scene_lower;
    public int change_scene_upper;

    public GameObject Eyelid;

    public float blink_time;

    private float time_in_scene = 0;
    private int scene_change_time = 0;

    private GameObject active_level;
    private int active_level_idx;

    private Animator eyelid_animator;

    private int[] priorities;

	// Use this for initialization
	void Start () {
        if (change_scene_lower >= change_scene_upper)
        {
            change_scene_upper = change_scene_lower + 30;
        }

        System.Random rng = new System.Random();
        scene_change_time = rng.Next(change_scene_lower, change_scene_upper + 1);

        active_level = levels.Length > 0 ? Instantiate(levels[0]) : null;
        active_level_idx = 0;

        eyelid_animator = Eyelid.GetComponent<Animator>();
        priorities = new int[levels.Length];
        priorities[0] = 0;

        for (int k = 1; k < priorities.Length; ++k)
        {
            priorities[k] = 1;
        }
	}

    // Update is called once per frame
    void Update()
    {
        time_in_scene += Time.deltaTime;
        if (time_in_scene < (float)scene_change_time)
        {
            return;
        }

        System.Random rng = new System.Random();

        time_in_scene = 0;
        scene_change_time = rng.Next(change_scene_lower, change_scene_upper + 1);

        int num_levels = levels.Length;

        bool scene_changed = num_levels > 1;

        if(!scene_changed)
        {
            return;
        }

        StartCoroutine(playBlinkAnimation());
	}

    IEnumerator playBlinkAnimation()
    {
        eyelid_animator.SetBool("Blink", true);
        Globals.fade_out = true;
        yield return new WaitForSeconds(blink_time);
        eyelid_animator.SetBool("Blink", false);
        Globals.fade_out = false;

        System.Random rng = new System.Random();

        int max_priority = 0;
        for (int k = 0; k < priorities.Length; ++k)
        {
            max_priority += priorities[k];
        }
        
        int selected = 0;
        int random_priority = rng.Next(0, max_priority);

        int curr_acc = 0;
        for (int k = 0; k < priorities.Length; ++k)
        {
            if (random_priority >= curr_acc && random_priority < curr_acc + priorities[k])
            {
                selected = k;
                break;
            }
            curr_acc += priorities[k];
        }

        for (int k = 0; k < priorities.Length; ++k)
        {
            if (k == selected)
            {
                priorities[k] = 0;
            }
            else
            {
                priorities[k] = priorities[k] == 0 ? 1 : priorities[k] * 2;
            }
        }
        
        Destroy(active_level);
        active_level = Instantiate(levels[selected]);
        active_level_idx = selected;
    }
}