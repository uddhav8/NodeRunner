using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    // Public //
    public Collider m_collider;
    [Range(0f, 1000f)]
    public float m_thrustFactor = 100f;
    public float m_thrustMultiplier = 100f;
    [Range(-1f, 1000f)]
    public float m_runTimePerCall = 1f;
    public bool m_triggerOnStart;
    public Vector3 m_thrustAxis = Vector3.forward;

    public ParticleSystem ExhaustFlame;
    // Protected //
    // Private //
    StupidCar m_car;
    float m_triggered = 0;
    public static bool AllActive = false;
    // Access //

    public void SetAllActive(bool set)
    {
        AllActive = set;
        Sensor.AllActive = set;
    }

    private void OnEnable()
    {
        m_car = GetComponentInParent<StupidCar>();
        if(m_car == null)
        {
            Debug.LogError(name + " no Stupid car in parent.");
            return;
        }
    }



    private void Start()
    {
        if(m_triggerOnStart)
        {
            ActivateOnce();
        }
    }

    public void ActivateOnce()
    {
        m_triggered = m_runTimePerCall;
    }

    private void FixedUpdate()
    {
        if(m_car == null || !AllActive)
        {
            return;
        }

        if(m_collider != null)
            m_collider.enabled = false;
        m_triggered -= Time.fixedDeltaTime;

        if(m_triggered <= 0f)
        {
            return;
        }

        Vector3 thrustVector = transform.TransformVector(m_thrustAxis);
        m_car.ApplyForce(transform.position, m_thrustFactor * thrustVector * m_thrustMultiplier);
    }

    public void PushPushPush(float factor)
    {
        Vector3 thrustVector = transform.TransformVector(m_thrustAxis);
        m_car.ApplyForce(transform.position, thrustVector * factor);
    }

    //public void startMainThruster()
    //{
    //    //m_mainThruster.GetComponent<Thruster>().m_triggerOnStart = true;
    //    m_triggerOnStart = true;
    //}

    /*public void startMainThrusterOnPlay()
    {
        PushPushPush(10000);    
    }*/
}
