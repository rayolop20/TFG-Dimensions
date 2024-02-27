using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer2D : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayLength = 10f;
    RaycastHit[] hits;
    List<RaycastHit> allHits = new();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit[] actualHits = Physics.RaycastAll(transform.position, transform.forward, rayLength);

        if (hits != actualHits)
        {
            hits = actualHits;
            foreach (RaycastHit hit in hits)
            {
                hit.collider.gameObject.layer = LayerMask.NameToLayer("2D");

                allHits.Add(hit);
                Debug.Log("Entro");
            }
        }

       


        //for (int i = 0; i < hits.Length; i++)
        //{
        //    if (allHits[i] != hits[i] )
        //    {
        //
        //    }
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }
}
