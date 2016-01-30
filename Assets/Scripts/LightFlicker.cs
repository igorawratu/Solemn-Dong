using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
    public int flicker_rate_upper;
    public int flicker_rate_lower;

    public int flicker_count_upper;
    public int flicker_count_lower;

    public float flicker_speed;

    private float current_flicker_timer;
    private int flicker_time_limit;

	// Use this for initialization
	void Start () {
        if (flicker_rate_upper <= flicker_rate_lower)
        {
            flicker_rate_upper = flicker_rate_lower + 5;
        }

        if (flicker_count_upper <= flicker_count_lower)
        {
            flicker_count_upper = flicker_count_lower + 5;
        }

        if (flicker_speed <= 0f)
        {
            flicker_speed = 0.1f;
        }

        System.Random rng = new System.Random();

        current_flicker_timer = 0f;
        flicker_time_limit = rng.Next(flicker_rate_lower, flicker_rate_upper + 1);
	}
	
	// Update is called once per frame
	void Update () {
        current_flicker_timer += Time.deltaTime;
        if (current_flicker_timer < flicker_time_limit)
        {
            return;
        }

        System.Random rng = new System.Random();

        current_flicker_timer = 0f;
        flicker_time_limit = rng.Next(flicker_rate_lower, flicker_rate_upper + 1);

        int num_flickers = rng.Next(flicker_count_lower, flicker_count_upper);

        StartCoroutine("flicker", num_flickers);
	}

    private IEnumerator flicker(int num_flickers)
    {
        for (int i = 0; i < num_flickers; ++i)
        {
            Light light = GetComponent<Light>();
            light.intensity = 1;
            yield return new WaitForSeconds(flicker_speed);
            light.intensity = 6;
            yield return new WaitForSeconds(flicker_speed);
        }
    }
}
