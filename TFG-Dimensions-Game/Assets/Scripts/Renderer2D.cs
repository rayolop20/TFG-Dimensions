using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer2D : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayLength = 10f;
    RaycastHit[] hits;
    List<RaycastHit> allHits = new();
    private List<GameObject> actualObjects = new List<GameObject>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        hits = Physics.RaycastAll(transform.position, transform.forward, rayLength);


        foreach (GameObject obj in actualObjects)
        {
            bool  hitActive = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == obj)
                {
                    hitActive = true;
                    break;
                }
            }

            if (!hitActive)
            {
                obj.layer = LayerMask.NameToLayer("Default");
            }
        }
        
        
        actualObjects.Clear(); //clear last frame list
        foreach (RaycastHit hit in hits)
        {
            actualObjects.Add(hit.collider.gameObject);
            hit.collider.gameObject.layer = LayerMask.NameToLayer("2D");
        }

        // Actualizar la lista de objetos tocados en el fotograma actual


        //RaycastHit[] actualHits = Physics.RaycastAll(transform.position, transform.forward, rayLength);
        //
        //foreach (RaycastHit hit in hits)
        //{
        //    hit.collider.gameObject.layer = LayerMask.NameToLayer("2D");
        //}
        //


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }
}
