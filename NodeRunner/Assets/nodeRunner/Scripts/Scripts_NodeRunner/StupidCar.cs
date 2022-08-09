using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidCar : MonoBehaviour
{
    // Public //
    public Rigidbody m_rigidBody;
    public Transform m_centerOfGravity;
    // Protected //
    // Private //
    // Access //

    private void Awake()
    {
        //m_rigidBody.centerOfMass = m_centerOfGravity.localPosition;
    }

    public void ApplyForce(Vector3 at, Vector3 force)
    {
        m_rigidBody.AddForceAtPosition(force, at);
        m_rigidBody.AddForce(force);
    } 
}
