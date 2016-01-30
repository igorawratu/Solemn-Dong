using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LaserManager : MonoBehaviour {
    public GameObject laser;
    public int num_lasers = 1;
    public int rect_height;
    public int rect_width;
    public Vector3 orientation;
    public float speed;
    public float light_max_intensity;
    public float light_min_intensity;
    public float intensity_change_speed;
    
    private GameObject [] lasers;
    private float [] curr_positions;
    private bool dimming;

	// Use this for initialization
	void Start () {
        bool dimming = true;
        float total_space = (float)rect_height * 2 + (float)rect_width * 2;
        float spacing = total_space / num_lasers;

        curr_positions = new float[num_lasers];
        lasers = new GameObject[num_lasers];

        for (int k = 0; k < num_lasers; ++k)
        {
            GameObject curr_laser = Instantiate(laser);
            LineRenderer laser_renderer = curr_laser.GetComponent<LineRenderer>();

            Vector3 laser_local_pos = convertFloatV3(spacing * (float)k);
            Vector3 curr_laser_world_pos = convertToWorld(laser_local_pos);

            laser_renderer.SetPosition(0, gameObject.transform.position);
            laser_renderer.SetPosition(1, curr_laser_world_pos);

            lasers[k] = curr_laser;
            curr_positions[k] = spacing * (float)k;
        }

        GetComponent<Light>().intensity = light_max_intensity;
	}

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        for (int k = 0; k < num_lasers; ++k)
        {
            float new_position = (curr_positions[k] + delta * speed) % ((float)rect_height * 2 + (float)rect_width * 2);
            LineRenderer laser_renderer = lasers[k].GetComponent<LineRenderer>();

            Vector3 laser_local_pos = convertFloatV3(new_position);
            Vector3 curr_laser_world_pos = convertToWorld(laser_local_pos);

            laser_renderer.SetPosition(0, gameObject.transform.position);
            laser_renderer.SetPosition(1, curr_laser_world_pos);

            curr_positions[k] = new_position;
        }

        Light light = GetComponent<Light>();
        float intensity = light.intensity;

        if (dimming)
        {
            intensity -= delta * intensity_change_speed;

            if (intensity < light_min_intensity)
            {
                intensity = light_min_intensity;
                dimming = false;
            }

            light.intensity = intensity;
        }
        else
        {
            intensity += delta * intensity_change_speed;

            if (intensity > light_max_intensity)
            {
                intensity = light_max_intensity;
                dimming = true;
            }

            light.intensity = intensity;
        }
	}

    private Vector3 convertToWorld(Vector3 local)
    {
        Quaternion to_world_rot = Quaternion.FromToRotation(new Vector3(0, -1, 0), orientation);
        return to_world_rot * local;
    }

    private Vector3 convertFloatV3(float val)
    {
        float modded_total = val % ((float)rect_height * 2 + (float)rect_width * 2);

        float[] dims = new float[5];
        dims[0] = 0;
        dims[1] = rect_width;
        dims[2] = rect_width + rect_height;
        dims[3] = 2 * rect_width + rect_height;
        dims[4] = 2 * rect_width + 2 * rect_height;

        int segment = 0;
        float modded_segment = 0;

        for (int k = 0; k < 4; ++k)
        {
            if (modded_total >= dims[k] && modded_total < dims[k + 1])
            {
                modded_segment = modded_total - dims[k];
                segment = k;
                break;
            }
        }

        float half_width = (float)rect_width / 2;
        float half_height = (float)rect_height / 2;

        switch (segment)
        {
            case (0):
                return new Vector3(-half_width + modded_segment, -60, half_height);
            case (1):
                return new Vector3(half_width, -60, half_height - modded_segment);
            case (2):
                return new Vector3(half_width - modded_segment, -60, -half_height);
            case (3):
                return new Vector3(-half_width, -60, -half_height + modded_segment);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    void OnDestroy()
    {
        for (int k = 0; k < num_lasers; ++k)
        {
            Destroy(lasers[k]);
        }
    }
}
