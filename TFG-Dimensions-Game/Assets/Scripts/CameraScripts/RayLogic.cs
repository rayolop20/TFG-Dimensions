using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;


public class HitObjects
{
    public Vector3 initPosition;
    public Vector3 endPosition;
    public Vector3 goScale;
}

public class RayLogic : MonoBehaviour
{

    public Transform target;
    private float orbitRadius = 10f; //distancia del centre

    private float rayLenght = 20f;
    RaycastHit[] RayEsquerra;
    RaycastHit[] RayDreta;

    private List<Vector3> newPositions = new();
    private List<Vector3> lastPositions = new();
    public List<GameObject> goInfo = new();
    public Dictionary<int, HitObjects> hObjetcs = new();
    [HideInInspector] public List<int> removedObjects = new();
    private int ObjectNumberId;
    public Vector3 origin = new Vector3(10, 0, 0);



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
        RayCalculations();
       
        Ray rayScale = new Ray(transform.position, transform.right);
        RaycastHit[] ScaleY = Physics.RaycastAll(rayScale);
        ScaleY = ScaleY.OrderBy(hit => hit.distance).ToArray();
        foreach (RaycastHit obj in ScaleY)
        {
               goInfo.Add(obj.collider.gameObject);
        }


        PositionCreationRay();

        //calcular numero de hits del rayo
        for (int i = 0; i < RayEsquerra.Count(); i++)
        {
            Vector3 roundedPointEsquerra = new Vector3((float)Math.Round(RayEsquerra[i].point.x, 2),
                                                  (float)Math.Round(RayEsquerra[i].point.y, 2),
                                                  (float)Math.Round(RayEsquerra[i].point.z, 2));

            Vector3 roundedPointDreta = new Vector3((float)Math.Round(RayDreta[i].point.x, 2),
                                                  (float)Math.Round(RayDreta[i].point.y, 2),
                                                  (float)Math.Round(RayDreta[i].point.z, 2));

            newPositions.Add(roundedPointEsquerra);
            newPositions.Add(roundedPointDreta);
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
                 //   colidedObject.goScale = goInfo[volta].transform.localScale;
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
         Vector3 rayPosition = target.TransformPoint(-Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
         Vector3 direction = (target.position - rayPosition).normalized;
         Debug.DrawRay(rayPosition, direction * 20f, Color.red);
        
         Vector3 rayPosition2 = target.TransformPoint(Vector3.right * orbitRadius); // blau (dreta a esquerra)
         Vector3 direction2 = (target.position - rayPosition2).normalized;
         Debug.DrawRay(new Vector3(rayPosition2.x, rayPosition2.y + 1, rayPosition2.z), direction2 * 20f, Color.blue);
    }

    private void actualizePositions(int objectId, int listNum)
    {
        hObjetcs[objectId].initPosition = newPositions[listNum];
        hObjetcs[objectId].endPosition = newPositions[listNum + 1];
    //    hObjetcs[objectId].endPosition = newPositions[listNum]; // falta acutalitzar
    }

    private void RayCalculations() {
        RayEsquerra = Physics.RaycastAll(gameObject.transform.position - origin, transform.right, rayLenght); // esquerra
        RayDreta = Physics.RaycastAll(gameObject.transform.position + origin, -transform.right, rayLenght); // dreta
        RayEsquerra = RayEsquerra.OrderBy(hit => hit.distance).ToArray();
        RayDreta = RayDreta.OrderBy(hit => hit.distance).ToArray();
        Array.Reverse(RayDreta);

    }

    private void PositionCreationRay()
    {

        Vector3 rayPositionEsquerra = target.TransformPoint(-Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
        // Calcular la dirección del rayo hacia el objetivo
        Vector3 directionEsquerra = (target.position - rayPositionEsquerra).normalized;
        RayEsquerra = Physics.RaycastAll(rayPositionEsquerra, directionEsquerra, rayLenght); // esquerra


        Vector3 rayPositionDreta = target.TransformPoint(Vector3.right * orbitRadius); // vermell (esquerra cap a dreta)
        // Calcular la dirección del rayo hacia el objetivo
        Vector3 directionDreta = (target.position - rayPositionDreta).normalized;
        RayDreta = Physics.RaycastAll(rayPositionDreta, directionDreta, rayLenght); // dreta


        RayEsquerra = RayEsquerra.OrderBy(hit => hit.distance).ToArray();
        RayDreta = RayDreta.OrderBy(hit => hit.distance).ToArray();
        Array.Reverse(RayDreta);
    }
}
