using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Viking : MonoBehaviour
{   
    public float angle = 0;
    private float lerpTimer = 0;
    public float speed = 5f;
    /*
    [HideInInspector]
    public float Addedforce = 0f;
    [HideInInspector]
    public float WheelForce = 0f;
    [HideInInspector]
    public float gravity = 0.0f;
    [HideInInspector]
    public float angleGravity = 0.0f;
    [HideInInspector]*/
    private float durationValue = 30.0f;

    //public bool bForce = false;
    private bool bChangedDirection = false;
    Quaternion defaultRotation;

    // public AudioSource audioSource;

    private void Awake()
    {
        defaultRotation = transform.localRotation;
        speed = 6;
       
    }

    public void Reset()
    {
        lerpTimer = 0;
        transform.rotation = new Quaternion(defaultRotation.x, defaultRotation.y, defaultRotation.z, defaultRotation.w);
    }
   

    private void FixedUpdate()
    {
        lerpTimer += Time.deltaTime * (speed) / durationValue;

        transform.rotation = PendulumRotation();
        /*
      if (speed > 0.0f) speed -= Time.deltaTime;
      else speed = 0;

      if (angle > 0.0f) angle -= Time.deltaTime;
      else angle = 0;

      lerpTimer += Time.deltaTime * (speed + WheelForce) / durationValue;


      transform.rotation = PendulumRotation();

      if (speed > 0.0f) speed -= Time.deltaTime * gravity;
      else speed = 0;

      if (angle > 0.0f) angle -= Time.deltaTime * angleGravity;
      else angle = 0;


      WheelForce = Addedforce;
      Addedforce = 0;

      if (WheelForce != 0)
      {
          speed += WheelForce;
          WheelForce = 0;}*/

        /*
        if ((int)(transform.rotation.z) == 0)
        {
            bChangedDirection = true;
        }

        if (Mathf.Abs(transform.rotation.z) >= 0.25f && bChangedDirection)
        {
            // SoundManager.Instance.PlaySE(SoundList.Sound_viking,this.transform.position);
            audioSource.clip = SoundList.Sound_viking;
            audioSource.Play();
            bChangedDirection = false;
            Debug.Log("바이킹 소리");
        }*/
    }




    Quaternion PendulumRotation()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle), Quaternion.Euler(Vector3.back * angle), ((Mathf.Sin(lerpTimer) + 1.0f) * 0.5f));
    }

}
