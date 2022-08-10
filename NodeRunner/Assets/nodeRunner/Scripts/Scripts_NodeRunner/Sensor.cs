using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    // Public //
    public static bool AllActive = false;
    public Collider m_collider;
    public Thruster m_thruster;
    public Transform m_range;
    public LayerMask m_layerMask;
    public float m_pushFactor = 1f;
    // Protected //
    // Private //
    // Access //

    public void SetAllActive(bool set)
    {
        AllActive = set;
    }
    private void FixedUpdate()
    {
        if(!AllActive)
        {
            return;
        }
        if(m_collider != null)
            m_collider.enabled = false;
        Vector3 direction = m_range.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, distance, m_layerMask.value))
        {
            float pushFactor = 1f - (hit.distance / distance);
            m_thruster.PushPushPush(m_pushFactor * pushFactor);
        }
    }
}
