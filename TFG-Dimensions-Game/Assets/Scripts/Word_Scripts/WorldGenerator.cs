using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class WorldGenerator : MonoBehaviour
{
    public GameObject plane;
    public RayLogic rInfo;
    Dictionary<int, GameObject> dictPlanes = new Dictionary<int, GameObject>();

    int numberObjects;

    void Start()
    {

    }

    void Update()
    {

        if (numberObjects != rInfo.hObjetcs.Count) // crerar numeror de objectes segons cada 2 punts
        {
            foreach (KeyValuePair<int, HitObjects> g in rInfo.hObjetcs)
            {
                if (numberObjects < rInfo.hObjetcs.Count && !dictPlanes.ContainsKey(g.Key))
                {
                    GameObject newPlane = Instantiate(plane, Vector3.zero, Quaternion.Euler(-90, 0, 0));
                    dictPlanes.Add(g.Key, newPlane);
                    numberObjects++;
                }
            }
        }

        if (numberObjects > rInfo.hObjetcs.Count)
        {
            foreach (KeyValuePair<int, GameObject> removed in dictPlanes)
            {
                if (!rInfo.hObjetcs.ContainsKey(removed.Key))
                {
                    Destroy(dictPlanes[removed.Key].gameObject);
                    dictPlanes.Remove(removed.Key);
                    numberObjects--;
                    return;
                }
            }
        }

        if (numberObjects == rInfo.hObjetcs.Count)
        {
            foreach (KeyValuePair<int, GameObject> item in dictPlanes)
            {
                int index = item.Key; // Clave del diccionario
                GameObject plane = item.Value; //valor de dintre
                Vector3 position = getObjectPos2D(index);
                //float scale = rInfo.hObjetcs[index].endPosition.z - rInfo.hObjetcs[index].initPosition.z;
                float scale = GetScale(index);
                UpdateFloor(plane, index, scale, position);
            }
        }
    }

    private Vector3 getObjectPos2D(int numobj)
    {
        Vector3 distanceBewteenPoints = rInfo.hObjetcs[numobj].endPosition - rInfo.hObjetcs[numobj].initPosition;
        Vector3 centralPoint = distanceBewteenPoints / 2;
        return centralPoint;
    }
    private float GetScale(int numobj)
    {
       //Formula Euclidiana
        float Scale = Mathf.Sqrt(Mathf.Pow(rInfo.hObjetcs[numobj].endPosition.x - rInfo.hObjetcs[numobj].initPosition.x, 2) + Mathf.Pow(rInfo.hObjetcs[numobj].endPosition.z - rInfo.hObjetcs[numobj].initPosition.z, 2));
        return Scale;
    }
    private void UpdateFloor(GameObject planes, int number, float scale, Vector3 position)
    {
        planes.layer = LayerMask.NameToLayer("2D");
        planes.transform.position = new Vector3(rInfo.hObjetcs[number].initPosition.x + position.x, rInfo.hObjetcs[number].goGeneralVariables.transform.position.y, rInfo.hObjetcs[number].initPosition.z + position.z);
        planes.transform.localScale = new Vector3(scale / 10, 1, rInfo.hObjetcs[number].goGeneralVariables.transform.localScale.y / 10); //scale units 1 position = 10 scale
        planes.transform.eulerAngles = new Vector3(planes.transform.eulerAngles.x , 0,rInfo.rotationPlayer.transform.eulerAngles.y); //scale units 1 position = 10 scale  // rInfo.hObjetcs[number].goGeneralVariables.transform.eulerAngles.z
    }
}
