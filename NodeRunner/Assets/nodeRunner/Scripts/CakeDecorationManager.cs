using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DigitalRubyShared;

public class CakeDecorationManager : MonoBehaviour
{
    // Public //
    public LayerMask m_chipakneWaleLayer;
    public LayerMask m_dragLayers;
    public float m_spherecastRadius = 0.1f;
    public Camera m_mainCamera;
    public FingersPanOrbitComponentScript m_orbit;
    public ChipkneWaleCheezein m_chipkuCheezUnderMouse;
    public SelecatableCheezein m_selectedCheez;
    public float m_initialDisWhenChipkuCheezCameUnderMouse;
    // Protected //
    // Private //
    // Access //

    void Start()
    {
        
    }

    public static bool IsPointerOverUIObject()
    {
        Vector3 touchPoint = Vector3.zero;

        if (!Touch(ref touchPoint))
        {
            return false;
        }
        else
        {

            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            //eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            eventDataCurrentPosition.position = new Vector3(touchPoint.x, touchPoint.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            //Debug.Log(results.Count);

            for (int index = 0; index < results.Count; index++)
            {
                RaycastResult curRaysastResult = results[index];
                if (curRaysastResult.gameObject.layer == 5)
                    return true;
            }
        }

        return false;
    }

    public static bool Touch(ref Vector3 touchPoint)
    {
        bool clickum = Input.touches.Length > 0 || Input.GetMouseButtonDown(0);
        touchPoint = Input.mousePosition;
        if (Input.touches.Length > 0)
        {
            touchPoint = Input.touches[0].position;
        }
        return clickum;
    }

    void Update()
    {
        if (IsPointerOverUIObject())
        {
            return;
        }
        Vector3 touchPoint = Vector3.zero;
        if(Touch(ref touchPoint))
        {
            RaycastHit hit;
            Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            bool kyaAbhiHeSelectKeyaHaiKya = false;
            // Chipkne wale cheez
            if (Physics.SphereCast(ray, m_spherecastRadius, out hit, 9999f, m_chipakneWaleLayer.value))
            {
                ChipkneWaleCheezein cheez = hit.collider.GetComponent<ChipkneWaleCheezein>();

                if (cheez == null)
                {
                    Debug.LogError(hit.collider.gameObject.name + " has not script called ChipkneWaleCheezein on it");
                }
                else
                {
                    m_chipkuCheezUnderMouse = cheez;
                    m_chipkuCheezUnderMouse.m_collider.enabled = false;
                    m_initialDisWhenChipkuCheezCameUnderMouse = hit.distance;

                    SelecatableCheezein selectMePls = hit.collider.GetComponent<SelecatableCheezein>();
                    if (selectMePls != null)
                    {
                        if (m_selectedCheez != null && m_selectedCheez != selectMePls)
                        {
                            m_selectedCheez.Select(false);
                        }
                        kyaAbhiHeSelectKeyaHaiKya = true;
                        selectMePls.Select(true);
                        m_selectedCheez = selectMePls;
                    }
                }
            }

            // selectatble wale cheez 
            if (kyaAbhiHeSelectKeyaHaiKya == false)
            {
                if (Physics.SphereCast(ray, m_spherecastRadius, out hit, 9999f))
                {
                    SelecatableCheezein selectMePls = hit.collider.GetComponent<SelecatableCheezein>();
                    if (selectMePls != null)
                    {
                        if (m_selectedCheez != null && m_selectedCheez != selectMePls)
                        {
                            m_selectedCheez.Select(false);
                        }
                        selectMePls.Select(true);
                        m_selectedCheez = selectMePls;
                    }
                    else
                    {
                        selectMePls = hit.collider.transform.parent.GetComponent<SelecatableCheezein>();

                        if (selectMePls != null)
                        {
                            if (m_selectedCheez != null && m_selectedCheez != selectMePls)
                            {
                                m_selectedCheez.Select(false);
                            }
                            selectMePls.Select(true);
                            m_selectedCheez = selectMePls;
                        }
                        else
                        {
                            if (m_selectedCheez != null)
                            {
                                m_selectedCheez.Select(false);
                                m_selectedCheez = null;
                            }
                        }
                    }
                }
                else
                {
                    if (m_selectedCheez != null)
                    {
                        m_selectedCheez.Select(false);
                        m_selectedCheez = null;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            
            if (m_chipkuCheezUnderMouse != null)
            {
                m_chipkuCheezUnderMouse.m_collider.enabled = true;
            }
            m_chipkuCheezUnderMouse = null;
        }

        /*
        m_selectedCheez = m_chipkuCheezUnderMouse;
            if(m_selectedCheez != null)
            {

            }
        if(Input.ge)
        */
        if (m_chipkuCheezUnderMouse == null)
        {
            m_orbit.enabled = true;
        }
        else 
        {
            m_orbit.enabled = false;
            UpdateCheezUnderMouseDrag();
        }
    }

    void UpdateCheezUnderMouseDrag()
    {
        Vector3 touchPoint = Vector3.zero;
        RaycastHit hit;
        if (Touch(ref touchPoint))
        {
            Ray ray = m_mainCamera.ScreenPointToRay(touchPoint);
            if (Physics.SphereCast(ray, m_spherecastRadius, out hit, 9999f, m_dragLayers.value))
            {
                m_chipkuCheezUnderMouse.transform.position = hit.point - ray.direction * m_chipkuCheezUnderMouse.m_radius;// ray.origin + ray.direction * hit.distance;
            }
            else
            {
                m_chipkuCheezUnderMouse.transform.position = ray.origin + ray.direction * m_initialDisWhenChipkuCheezCameUnderMouse;
            }
        }
    }
}
