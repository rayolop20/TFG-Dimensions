using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer2D : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayLength = 10f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, rayLength);
        foreach (RaycastHit hit in hits)
        {
            hit.collider.gameObject.layer = LayerMask.NameToLayer("2D");
            Debug.Log("El rayo ha golpeado a: " + hit.collider.gameObject.name);
        }
        else
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }
}
