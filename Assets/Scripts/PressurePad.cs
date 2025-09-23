using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private float _requiredDistance;

    private MeshRenderer meshRenderer;
    private Color _padColor;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        _padColor = meshRenderer.material.color;

    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Moving_Box"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance < _requiredDistance)
            {

                // Rigidbody box = other.GetComponent<Rigidbody>();
                // if (box != null)
                // {
                //     box.isKinematic = true;
                // }

                if (meshRenderer != null)
                {
                    meshRenderer.material.color = Color.green;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moving_Box"))
        {
            if (meshRenderer != null)
            {
                meshRenderer.material.color = _padColor;
            }
        }
    }
}
