using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecatableCheezein : MonoBehaviour
{
    // Public //
    [Header("Selectable Variables")]
    public GameObject m_selectionMesh;
    public bool m_canChangeColor;
    public bool m_canRotate;
    public MeshRenderer m_mainMesh;
    public Material m_mainMaterial;
    // Protected //
    // Private //
    // Access //

    public virtual void Start()
    {
        m_mainMaterial = m_mainMesh.material;
    }

    public void Select(bool set)
    {
        m_selectionMesh.SetActive(set);
    }
}
