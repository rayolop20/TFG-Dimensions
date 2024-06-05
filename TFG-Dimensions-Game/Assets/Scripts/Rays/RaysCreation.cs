using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaysCreation : MonoBehaviour
{
    public Transform target;
    public float orbitRadius; //distancia del centre

    public float rayLenght = 40f;
    public RaycastHit[] rayEsquerra;
    public RaycastHit[] rayDreta;

    [HideInInspector] public Vector3 directionEsquerra;
    [HideInInspector] public Vector3 directionDreta;

    [HideInInspector] public Vector3 endPositionDreta;
    [HideInInspector] public Vector3 endPositionEsquerra;
    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitBackfaces = true;
    }

    // Update is called once per frame
    void Update()
    {
        PositionCreationRay();
    }

    private void PositionCreationRay()
    {

        Vector3 rayPositionEsquerra = target.TransformPoint(-Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
        // Calcular la dirección del rayo hacia el objetivo
        directionEsquerra = (target.position - rayPositionEsquerra).normalized;

        rayEsquerra = Physics.RaycastAll(rayPositionEsquerra, directionEsquerra, rayLenght); // esquerra
        endPositionEsquerra = rayPositionEsquerra;

        Vector3 rayPositionDreta = target.TransformPoint(Vector3.right * orbitRadius); // vermell (dreta cap a Esquerra)
        // Calcular la dirección del rayo hacia el objetivo
        directionDreta = (target.position - rayPositionDreta).normalized;
        rayDreta = Physics.RaycastAll(rayPositionDreta, directionDreta, rayLenght); // dreta
        endPositionDreta = rayPositionDreta;

        

        rayEsquerra = rayEsquerra.OrderBy(hit => hit.distance).ToArray();
        rayDreta = rayDreta.OrderByDescending(hit => hit.distance).ToArray();

    }

    private void OnDrawGizmos()
    {
        Vector3 rayPosition = target.TransformPoint(-Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
        Vector3 direction = (target.position - rayPosition).normalized;

        Debug.DrawRay(rayPosition, direction * rayLenght, Color.red);

        Vector3 rayPosition2 = target.TransformPoint(Vector3.right * orbitRadius); // blau (dreta a esquerra)
        Vector3 direction2 = (target.position - rayPosition2).normalized;
        Debug.DrawRay(new Vector3(rayPosition2.x, rayPosition2.y, rayPosition2.z), direction2 * rayLenght, Color.blue);
    }


}
