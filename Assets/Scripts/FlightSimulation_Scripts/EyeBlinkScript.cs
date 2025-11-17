using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinkScript : MonoBehaviour
{
    public float blinkEyeRate;
    private Animator anim; 
    private float previousBlinkEyeRate;
    private float blinkEyeTime;

    // Update is called once per frame
    void Update()
    {
        anim = gameObject.GetComponent<Animator>(); 
        if (Time.time > blinkEyeTime)
        {
            previousBlinkEyeRate = blinkEyeRate;
            blinkEyeTime = Time.time + blinkEyeRate;
            //set a trigger named "blink" in ur animator window and then set that trigger the arrow connectiing eyeIdle to eyeBlink               
            anim.SetTrigger("blink");
            while (previousBlinkEyeRate == blinkEyeRate)
            {
                // Random Rate from 4 secs to 10secs
                blinkEyeRate = Random.Range(4f, 10f);
            }
        }
    }
}
