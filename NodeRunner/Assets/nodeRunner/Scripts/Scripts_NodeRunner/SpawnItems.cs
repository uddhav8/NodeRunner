using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    //public
    public GameObject ThrusterSensorCombo;

    public Transform SpawnParent;

    public void spawnThrusterSensorCombo()
    {
        GameObject go = Instantiate<GameObject>(ThrusterSensorCombo, SpawnParent.position, Quaternion.identity, SpawnParent);
        //go.transform.SetParent(Parent);
    }
}
