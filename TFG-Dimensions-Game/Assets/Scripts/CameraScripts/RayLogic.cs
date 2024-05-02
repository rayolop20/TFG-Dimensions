using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HitObjects
{
    public Vector3 initPosition;
    public Vector3 endPosition;
    public Vector3 goScale;
}

public class RayLogic : MonoBehaviour
{
    public float rayLength = 10f; // Longitud del rayo
    int hitsCount = 0;
    Vector3 LastHitPosition;
    private List<Vector3> newPositions = new();
    private List<Vector3> lastPositions = new();
    public List<GameObject> goInfo = new();
    public Dictionary<int, HitObjects> hObjetcs = new();
    [HideInInspector] public List<int> removedObjects = new();
    private int ObjectNumberId;



    private void Start()
    {
        ObjectNumberId = 0;
        // Comprobar si el rayo colisiona con algo
        Physics.queriesHitBackfaces = true;
    }
    void Update()
    {
        newPositions.Clear();
        goInfo.Clear();
        hitsCount = 0;
        LastHitPosition = transform.position;

        Ray rayScale = new Ray(transform.position, transform.right);
        RaycastHit[] ScaleY = Physics.RaycastAll(rayScale);
        ScaleY = ScaleY.OrderBy(hit => hit.distance).ToArray();
        foreach (RaycastHit obj in ScaleY)
        {
               goInfo.Add(obj.collider.gameObject);
        }

        //calcular numero de hits del rayo
        for (int i = 0; i <= hitsCount; i++)
        {

            Ray ray = new Ray(LastHitPosition, transform.right);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, rayLength))
            {
                hitsCount++; // suma quan fa hit
                Vector3 roundedPoint = new Vector3((float)Math.Round(hitInfo.point.x, 2),
                                                   (float)Math.Round(hitInfo.point.y, 2),
                                                   (float)Math.Round(hitInfo.point.z, 2));

                newPositions.Add(roundedPoint);

                LastHitPosition = hitInfo.point + new Vector3(0.01f, 0, 0); // + 0.01 per no tornar a colisionar
                Debug.DrawRay(LastHitPosition, transform.right * hitInfo.distance, Color.red);
            }
        }


        //calcular i adegir objectes i posicions noves

        if (newPositions.Count % 2 != 1 && lastPositions.Count % 2 != 1)
        {

            int vuelta_num = 0;
            foreach (KeyValuePair<int, HitObjects> planeValue in hObjetcs)
            {
                int _key = planeValue.Key;


                if (hObjetcs.Count > (newPositions.Count / 2) && !newPositions.Contains(planeValue.Value.initPosition))
                {
                    removedObjects.Add(_key);
                    hObjetcs.Remove(_key);
                    lastPositions = new List<Vector3>(newPositions);

                    Debug.Log("entro");
                    return;

                }
                else if (lastPositions.Count == newPositions.Count && (!newPositions.Contains(planeValue.Value.initPosition) || !newPositions.Contains(planeValue.Value.endPosition)) && (newPositions.Count / 2) == hObjetcs.Count)
                {
                    actualizePositions(_key, vuelta_num);
                }
                vuelta_num = vuelta_num + 2;
            }
            int volta =0; ;
            for (int i = 0; i < newPositions.Count; i += 2)
            {
                if (lastPositions.Count < newPositions.Count && !lastPositions.Contains(newPositions[i]) && !hObjetcs.ContainsKey(ObjectNumberId) && i % 2 == 0) // comparar si tamany es diferent i el objectes esta creat o no 
                {
                    HitObjects colidedObject = new HitObjects();
                    colidedObject.initPosition = newPositions[i];
                    colidedObject.endPosition = newPositions[i + 1];
                    colidedObject.goScale = goInfo[volta].transform.localScale;
                    hObjetcs.Add(ObjectNumberId, colidedObject);
                    lastPositions.Add(newPositions[i]);
                    lastPositions.Add(newPositions[i + 1]);
                    ObjectNumberId++;
                    
                }
                volta++;
            }


        }
        lastPositions = new List<Vector3>(newPositions);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * rayLength);
    }

    private void actualizePositions(int objectId, int listNum)
    {
        hObjetcs[objectId].initPosition = newPositions[listNum];
        hObjetcs[objectId].endPosition = newPositions[listNum + 1];
    //    hObjetcs[objectId].endPosition = newPositions[listNum]; // falta acutalitzar
    }
}
