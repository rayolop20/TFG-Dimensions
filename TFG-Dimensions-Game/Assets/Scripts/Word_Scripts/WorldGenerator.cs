using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class WorldGenerator : MonoBehaviour
{
    public GameObject plane;
    //public RayOrder rOrdered;
    public RayLogic rayLogic;
    public Transform pInfo;
    Dictionary<int, GameObject> dictPlanes = new Dictionary<int, GameObject>();
    public SwapMaterial swapMaterial;

    int numberObjects;

    void Start()
    {
        swapMaterial = GetComponent<SwapMaterial>();
    }

    void Update()
    {

        if (numberObjects != rayLogic.hObjetcs.Count) // crerar numeror de objectes segons cada 2 punts
        {
            foreach (KeyValuePair<int, HitObjects> g in rayLogic.hObjetcs)
            {
                if (numberObjects < rayLogic.hObjetcs.Count && !dictPlanes.ContainsKey(g.Key))
                {
                    GameObject newPlane = Instantiate(plane, Vector3.zero, plane.transform.rotation);
                    dictPlanes.Add(g.Key, newPlane);
                    numberObjects++;
                }
            }
        }

        if (numberObjects > rayLogic.hObjetcs.Count)
        {
            foreach (KeyValuePair<int, GameObject> removed in dictPlanes)
            {
                if (!rayLogic.hObjetcs.ContainsKey(removed.Key))
                {
                    Destroy(dictPlanes[removed.Key].gameObject);
                    dictPlanes.Remove(removed.Key);
                    numberObjects--;
                    return;
                }
            }
        }

        if (numberObjects == rayLogic.hObjetcs.Count)
        {
            foreach (KeyValuePair<int, GameObject> item in dictPlanes)
            {
                int index = item.Key; // Clave del diccionario
                GameObject plane = item.Value; //valor de dintre
                plane.tag = rayLogic.hObjetcs[index].goGeneralVariables.tag;
                swapMaterial.SwapMat(plane, plane.tag);

                Vector3 position = getObjectPos2D(index);
                //float scale = rInfo.hObjetcs[index].endPosition.z - rInfo.hObjetcs[index].initPosition.z;
                float scale = GetScale(index);
                UpdateFloor(plane, index, scale, position);
            }
        }
    }

    private Vector3 getObjectPos2D(int numobj)
    {
        Vector3 centralPoint = Vector3.zero;
        if (rayLogic.hObjetcs.ContainsKey(numobj))
        {
            Vector3 distanceBewteenPoints = rayLogic.hObjetcs[numobj].endPosition - rayLogic.hObjetcs[numobj].initPosition;
            centralPoint = distanceBewteenPoints / 2;
        }

        return centralPoint;
    }
    private float GetScale(int numobj)
    {
        //Formula Euclidiana
        float Scale = Mathf.Sqrt(Mathf.Pow(rayLogic.hObjetcs[numobj].endPosition.x - rayLogic.hObjetcs[numobj].initPosition.x, 2) + Mathf.Pow(rayLogic.hObjetcs[numobj].endPosition.z - rayLogic.hObjetcs[numobj].initPosition.z, 2));
        return Scale;
    }
    private void UpdateFloor(GameObject planes, int number, float scale, Vector3 position)
    {
        planes.layer = LayerMask.NameToLayer("2D");
        planes.transform.position = new Vector3(rayLogic.hObjetcs[number].initPosition.x + position.x, rayLogic.hObjetcs[number].goGeneralVariables.transform.position.y, rayLogic.hObjetcs[number].initPosition.z + position.z);
        planes.transform.localScale = new Vector3(scale / 10, 1, rayLogic.hObjetcs[number].goGeneralVariables.transform.localScale.y / 10); //scale units 1 position = 10 scale
                                                                                                                                               // planes.transform.rotation = new Quaternion(planes.transform.rotation.x, planes.transform.rotation.y, rInfo.rotationPlayer.transform.rotation.z, planes.transform.rotation.w);
        planes.transform.eulerAngles = new Vector3(planes.transform.eulerAngles.x, 0, pInfo.transform.eulerAngles.y); //scale units 1 position = 10 scale  // rInfo.hObjetcs[number].goGeneralVariables.transform.eulerAngles.z
    }
}
