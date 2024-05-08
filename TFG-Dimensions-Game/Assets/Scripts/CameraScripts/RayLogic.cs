using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;


public class HitObjects
{
    public Vector3 initPosition;
    public Vector3 endPosition;
    public GameObject goGeneralVariables;
    public int id;
}

public class RayLogic : MonoBehaviour
{
    public Transform target;
    public float orbitRadius = 10f; //distancia del centre

    private float rayLenght = 10f;
    RaycastHit[] RayEsquerra;
    RaycastHit[] RayDreta;
    public Transform rotationPlayer;

    private Dictionary<int, Tuple<Vector3, Vector3, GameObject>> newPositions = new();
    private Dictionary<int, Tuple<Vector3, Vector3, GameObject>> lastPositions = new();
    public Dictionary<int, HitObjects> hObjetcs = new();

    Vector3 endPositionDreta;
    Vector3 endPositionEsquerra;

    private void Start()
    {

    }
    void Update()
    {
        newPositions.Clear();

        PositionCreationRay();
        RaycastHit[] raySwap = RayEsquerra;
        if (RayEsquerra.Length < RayDreta.Length)
        {
            RayEsquerra = RayEsquerra.OrderByDescending(hit => hit.distance).ToArray();
            RayDreta = RayDreta.OrderBy(hit => hit.distance).ToArray();
            raySwap = RayDreta;
        }

        //calcular numero de hits del rayo
        for (int i = 0; i < raySwap.Length; i++)
        {
            if (raySwap[i].collider.gameObject.tag != "Player")
            {
                if ((i < RayEsquerra.Length && i < RayDreta.Length) && (RayEsquerra[i].collider.gameObject.GetInstanceID() == RayDreta[i].collider.gameObject.GetInstanceID()))
                {
                    newPositions.Add(raySwap[i].collider.gameObject.GetInstanceID(),
                            Tuple.Create(RayDreta[i].point, RayEsquerra[i].point, RayEsquerra[i].collider.gameObject));
                }
                else if (RayEsquerra.Length > RayDreta.Length)
                {
                    newPositions.Add(raySwap[i].collider.gameObject.GetInstanceID(),
                            Tuple.Create(endPositionDreta, RayEsquerra[i].point, RayEsquerra[i].collider.gameObject));
                }
                else if (RayEsquerra.Length < RayDreta.Length)
                {
                    newPositions.Add(raySwap[i].collider.gameObject.GetInstanceID(),
                            Tuple.Create(RayDreta[i].point, endPositionEsquerra, RayDreta[i].collider.gameObject));
                }
                else
                {
                    RayEsquerra = RayEsquerra.OrderByDescending(hit => hit.distance).ToArray();
                    RayDreta = RayDreta.OrderBy(hit => hit.distance).ToArray();
                }
                


            }
            else
            {
                rotationPlayer = raySwap[i].collider.gameObject.transform;
            }


        }



        //calcular i adegir objectes i posicions noves

        foreach (KeyValuePair<int, HitObjects> planeValue in hObjetcs)
        {
            if (hObjetcs.Count > newPositions.Count && !newPositions.ContainsKey(planeValue.Key)) //Eliminar elements
            {
                hObjetcs.Remove(planeValue.Key);
                lastPositions.Remove(planeValue.Key);
                return;

            }
            else if ((hObjetcs[planeValue.Key].initPosition != newPositions[planeValue.Key].Item1 && hObjetcs[planeValue.Key].initPosition != newPositions[planeValue.Key].Item2) || 
                (hObjetcs[planeValue.Key].endPosition != newPositions[planeValue.Key].Item1 && hObjetcs[planeValue.Key].endPosition != newPositions[planeValue.Key].Item2)) //mirar sio posicions son iguals segons key
            {
              actualizePositions(planeValue.Key);
            }

            hObjetcs[planeValue.Key].goGeneralVariables = newPositions[planeValue.Key].Item3; // actualitzar escala


        }
        foreach (KeyValuePair<int, Tuple<Vector3, Vector3, GameObject>> positions in newPositions)
        {
            if (lastPositions.Count < newPositions.Count && !lastPositions.ContainsKey(positions.Key)) // comparar si tamany es diferent i el objectes esta creat o no 
            {
                HitObjects colidedObject = new HitObjects();
                colidedObject.initPosition = positions.Value.Item1;
                colidedObject.endPosition = positions.Value.Item2;
                colidedObject.goGeneralVariables = positions.Value.Item3;
                hObjetcs.Add(positions.Key, colidedObject);
            }
        }



        foreach (var kvp in newPositions) //igualar diccionaris
        {
            if (!lastPositions.ContainsKey(kvp.Key))
            {
                lastPositions.Add(kvp.Key, kvp.Value);
            }
            else if (lastPositions.ContainsKey(kvp.Key))
            {
                lastPositions[kvp.Key] = kvp.Value;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Vector3 rayPosition = target.TransformPoint(-Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
        Vector3 direction = (target.position - rayPosition).normalized;
 
        Debug.DrawRay(rayPosition, direction * 10f, Color.red);

        Vector3 rayPosition2 = target.TransformPoint(Vector3.right * orbitRadius); // blau (dreta a esquerra)
        Vector3 direction2 = (target.position - rayPosition2).normalized;
        Debug.DrawRay(new Vector3(rayPosition2.x, rayPosition2.y, rayPosition2.z), direction2 * 10f, Color.blue);
    }

    private void actualizePositions(int objectId)//
    {
        hObjetcs[objectId].initPosition = newPositions[objectId].Item1;
        hObjetcs[objectId].endPosition = newPositions[objectId].Item2;
    }


    private void PositionCreationRay()
    {

        Vector3 rayPositionEsquerra = target.TransformPoint(-Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
        // Calcular la dirección del rayo hacia el objetivo
        Vector3 directionEsquerra = (target.position - rayPositionEsquerra).normalized;
        RayEsquerra = Physics.RaycastAll(rayPositionEsquerra, directionEsquerra, rayLenght); // esquerra
        endPositionEsquerra = rayPositionEsquerra;

        Vector3 rayPositionDreta = target.TransformPoint(Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
        // Calcular la dirección del rayo hacia el objetivo
        Vector3 directionDreta = (target.position - rayPositionDreta).normalized;
        RayDreta = Physics.RaycastAll(rayPositionDreta, directionDreta, rayLenght); // dreta
        endPositionDreta = rayPositionDreta;

        RayEsquerra = RayEsquerra.OrderBy(hit => hit.distance).ToArray();
        RayDreta = RayDreta.OrderByDescending(hit => hit.distance).ToArray();
        
    }

}
