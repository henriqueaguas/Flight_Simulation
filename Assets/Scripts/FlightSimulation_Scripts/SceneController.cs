using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public float timeValue;

    [SerializeField] private GameObject _clouds;
    private bool _turbulenceEnabled = false;
    private bool _turbulenceDisabled = false;
    private bool _landingEnabled = false;
    private bool _endOfTurbulence = false;
    
    // Update is called once per frame
    void Update()
    {
        if(timeValue < 0)
        {
            timeValue = 0;
        }
        else
        {
            timeValue -= Time.deltaTime;
            if(timeValue < 120 && !_turbulenceEnabled)
            {
                _turbulenceEnabled = true;
                FollowFlightPath.instance.EnableTurbulence();
            }
            else if (timeValue < 60 && !_endOfTurbulence)
            {
                _endOfTurbulence = true;
                FollowFlightPath.instance.DisableTurbulence();
            }
            else if(timeValue < 30 && !_turbulenceDisabled)
            {
                _turbulenceDisabled = true;
                _clouds.SetActive(true);
                _clouds.GetComponent<ParticleSystem>().Play();
            }
            else if(timeValue < 20 && !_landingEnabled)
            {
                _landingEnabled = true;
                FollowFlightPath.instance.landPlane();
            }
        }
        //DisplayTime();

    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);

        Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
