using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class FollowFlightPath : MonoBehaviour
{
    public PathCreator flightPath;
    public PathCreator landingPath;
    [SerializeField] float speed = 0.0f;
    float accel = 2.0f;
    float distanceTravelled = 0.0f;
    bool followPath = true;
    bool turbulence_enabled = false;
    bool land_plane = false;
    bool slow_down = false;

    Vector3 plane_landing_spot = new Vector3(-444.10f, 122.77f, -294.75f);
    Vector3 plane_stop_spot = new Vector3(-444.11f, 122.75f, -164.01f);

    Vector3 plane_free_flight = new Vector3(-443.83f, 250.50f, 398.49f);

    float stopping_distance = 0.0f;
    float landing_speed = 0.0f;
    Rigidbody plane_Rigidbody;

    GameObject[] scared_npcs;

    public static FollowFlightPath instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        plane_Rigidbody = GetComponent<Rigidbody>();
        stopping_distance = Vector3.Distance(plane_landing_spot, plane_stop_spot);

        scared_npcs = GameObject.FindGameObjectsWithTag("Fear_NPC");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (followPath)
        {
            distanceTravelled += speed * Time.deltaTime;
            speed += accel * Time.deltaTime;
            transform.position = flightPath.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = flightPath.path.GetRotationAtDistance(distanceTravelled);
            //var U_Turn_Segment = flightPath.bezierPath.GetPointsInSegment(2);
            //var anchor2 = U_Turn_Segment[3];
            //Debug.Log(transform.position);
            if (Vector3.Distance(transform.position, plane_free_flight) <= 1.0f)
            {
                followPath = false;
                distanceTravelled = 0.0f;
            }
        }
        if (land_plane)
        {
            distanceTravelled   += speed * Time.deltaTime;
            speed               = Math.Clamp(speed - (accel * Time.deltaTime), 2.0f, 300.0f);
            transform.position  = landingPath.path.GetPointAtDistance(distanceTravelled);
            transform.rotation  = landingPath.path.GetRotationAtDistance(distanceTravelled);
            if (slow_down)
            {
                var time_stamp  = 1 - (Vector3.Distance(transform.position, plane_landing_spot) / stopping_distance);
                speed           = Mathf.Lerp(0, landing_speed, time_stamp);
                if(speed <= 2.5f)
                {
                    speed = 0.0f;
                    land_plane = false;
                }
                return;
            }

            //var U_Turn_Segment  = landingPath.bezierPath.GetPointsInSegment(1);
            //var anchor2         = U_Turn_Segment[3];
            //Debug.Log(anchor2);
            //Debug.Log(transform.position);
            var distance = Vector3.Distance(transform.position, plane_landing_spot);
            if (distance <= 1.0f)
            {
                slow_down = true;
                landing_speed = speed;
            }
        }        
       
    }

    public void EnableTurbulence()
    {
        turbulence_enabled = true;
        foreach (var npc in scared_npcs)
        {
            npc.GetComponent<Animator>().SetBool("turbulence", turbulence_enabled);
        }
    }

    public void DisableTurbulence()
    {
        turbulence_enabled = false;
        foreach (var npc in scared_npcs)
        {
            npc.GetComponent<Animator>().SetBool("turbulence", turbulence_enabled);
        }
    }

    public bool getTurbulence()
    {
        return turbulence_enabled;
    }

    public void landPlane()
    {
        followPath = false;
        distanceTravelled = 0.0f;
        speed += 20.0f;
        land_plane = !land_plane;
        
    }
}
