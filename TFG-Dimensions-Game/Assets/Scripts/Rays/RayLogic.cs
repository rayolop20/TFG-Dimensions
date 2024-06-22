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
    private Dictionary<int, Tuple<Vector3, Vector3, GameObject>> newPositions = new();
    private Dictionary<int, Tuple<Vector3, Vector3, GameObject>> lastPositions = new();
    public Dictionary<int, HitObjects> hObjetcs = new();
    [HideInInspector] public List<int> removeObjects = new();

    List<RaysCreation> createdRays = new List<RaysCreation>();


    private void Start()
    {


        RaysCreation[] hijos = GetComponentsInChildren<RaysCreation>();

        // Agrega cada uno a la lista
        foreach (RaysCreation hijo in hijos)
        {
            createdRays.Add(hijo);
        }
    }

    void Update()
    {
        newPositions.Clear();

        //////////////////////////////////////////////////////
        //calcular numero de hits del rayo

        foreach (RaysCreation ray in createdRays)
        {
            if (ray.rayEsquerra != null)
            {
                for (int i = 0; i < ray.rayEsquerra.Length; i++)
                {
                    if (ray.rayEsquerra[i].collider.gameObject.tag != "Player")
                    {

                        float dotProductRayEsquerra = Vector3.Dot(ray.directionEsquerra.normalized, ray.rayEsquerra[i].normal);
                        float dotProductRayDreta = Vector3.Dot(ray.directionDreta.normalized, ray.rayDreta[i].normal);
                        if (i < ray.rayEsquerra.Length && i < ray.rayDreta.Length && ray.rayEsquerra[i].point != ray.rayDreta[i].point && !newPositions.ContainsKey(ray.rayEsquerra[i].collider.gameObject.GetInstanceID()))
                        {
                            newPositions.Add(ray.rayEsquerra[i].collider.gameObject.GetInstanceID(),
                                    Tuple.Create(ray.rayDreta[i].point, ray.rayEsquerra[i].point, ray.rayEsquerra[i].collider.gameObject));
                        }
                        else if (dotProductRayEsquerra > 0 && !newPositions.ContainsKey(ray.rayEsquerra[i].collider.gameObject.GetInstanceID()))
                        {
                            newPositions.Add(ray.rayEsquerra[i].collider.gameObject.GetInstanceID(),
                                    Tuple.Create(ray.rayDreta[i].point, ray.endPositionEsquerra, ray.rayDreta[i].collider.gameObject));
                        }
                        else if (dotProductRayDreta > 0 && !newPositions.ContainsKey(ray.rayEsquerra[i].collider.gameObject.GetInstanceID()))
                        {
                            newPositions.Add(ray.rayDreta[i].collider.gameObject.GetInstanceID(),
                                    Tuple.Create(ray.endPositionDreta, ray.rayEsquerra[i].point, ray.rayEsquerra[i].collider.gameObject));
                        }

                    }
                }
            }

        }




        //calcular i adegir objectes i posicions noves

        foreach (KeyValuePair<int, HitObjects> planeValue in hObjetcs)
        {
            if (!newPositions.ContainsKey(planeValue.Key)) //Eliminar elements
            {
                removeObjects.Add(planeValue.Key);
                hObjetcs.Remove(planeValue.Key);
                lastPositions.Remove(planeValue.Key);
                return;

            }
            if (hObjetcs.ContainsKey(planeValue.Key) && newPositions.ContainsKey(planeValue.Key))
            {
                if ((hObjetcs[planeValue.Key].initPosition != newPositions[planeValue.Key].Item1 && hObjetcs[planeValue.Key].initPosition != newPositions[planeValue.Key].Item2) ||
                (hObjetcs[planeValue.Key].endPosition != newPositions[planeValue.Key].Item1 && hObjetcs[planeValue.Key].endPosition != newPositions[planeValue.Key].Item2)) //mirar sio posicions son iguals segons key
                {
                    actualizePositions(planeValue.Key);
                }
                hObjetcs[planeValue.Key].goGeneralVariables = newPositions[planeValue.Key].Item3; // actualitzar escala
            }


          


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

        if (lastPositions.Count > newPositions.Count)
        {
            Debug.Log("Bug");
        }

        foreach (var kvp in newPositions) //igualar diccionaris
        {
            if (!lastPositions.ContainsKey(kvp.Key))
            {
                lastPositions.Add(kvp.Key, kvp.Value);
                if (lastPositions.Count > newPositions.Count)
                {
                    Debug.Log("Bug");
                }
            }
            else if (lastPositions.ContainsKey(kvp.Key))
            {
                lastPositions[kvp.Key] = kvp.Value;
            }
           //else if (lastPositions.Count > newPositions.Count && !newPositions.ContainsKey())
           //{
           //
           //}

        }
        if (lastPositions.Count > newPositions.Count)
        {
            Debug.Log("Bug");
        }

    }



    private void actualizePositions(int objectId)//
    {
        hObjetcs[objectId].initPosition = newPositions[objectId].Item1;
        hObjetcs[objectId].endPosition = newPositions[objectId].Item2;
    }


}
