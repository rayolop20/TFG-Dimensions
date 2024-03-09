using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealRender2D : MonoBehaviour
{
    struct HitObjects
    {
        public string name;
        public Vector3 initPosition;
        public Vector3 endPosition;
    }

    public float rayLength = 10f; // Longitud del rayo
    public LayerMask layerMask; // Máscara de capas para la detección de colisiones
    public GameObject plane;
    public GameObject p;

    int instant = 0;

    int hitsCount = 0;
    Vector3 LastHitPosition;
    List<Vector3> positions = new();
    List<HitObjects> hObjetcs = new();
    private void Start()
    {
        // Comprobar si el rayo colisiona con algo
        Physics.queriesHitBackfaces = true;
    }
    void Update()
    {
        positions.Clear();
        LastHitPosition = transform.position;

        for (int i = 0; i < hitsCount + 1; i++)
        {
            Ray ray = new Ray(LastHitPosition, transform.forward);

            RaycastHit hitInfo;


            if (Physics.Raycast(ray, out hitInfo, rayLength))
            {
                hitsCount++;
                positions.Add(hitInfo.point);

                LastHitPosition = hitInfo.point + new Vector3(0, 0, 0.01f);

                Debug.Log(positions[i]);
                // Dibujar el rayo en la escena hasta el punto de colisión
                Debug.DrawRay(LastHitPosition, transform.forward * hitInfo.distance, Color.red);

            }

            if (i % 2 == 0 && i != 0)
            {
                HitObjects colidedObject = new HitObjects();

                colidedObject.initPosition = positions[i - 1];
                colidedObject.endPosition = positions[i - 2];

                hObjetcs.Add(colidedObject);
            }

        }

        foreach (HitObjects item in hObjetcs)
        {
            GameObject nPlane = new GameObject(); 
            nPlane = Instantiate(plane, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
       // //calcular escala i posicio del pla
       //    
       // float scale = (positions[1].z - positions[0].z);
       // float position = scale / 2;
       //
       //
       //
       // p.layer = LayerMask.NameToLayer("2D");
       // p.transform.position = new Vector3(0, 0, positions[0].z + position);
       // p.transform.localScale = new Vector3(plane.transform.localScale.x / 10, 1, scale / 10);
       // instant++;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }

}
