using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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
                float scale = getObjectScale(index);
                //float scale = rInfo.hObjetcs[index].endPosition.z - rInfo.hObjetcs[index].initPosition.z;
                float position = getObjectPos2D(scale);
                UpdateFloor(plane, index, scale, position);
            }
        }
    }

    private float getObjectScale(int numobj)
    {
        float scale = rInfo.hObjetcs[numobj].endPosition.x - rInfo.hObjetcs[numobj].initPosition.x;
        return scale;
    }
    private float getObjectPos2D(float scale)
    {
        float position = scale / 2;
        return position;
    }
    private void UpdateFloor(GameObject planes, int number, float scale, float position)
    {
        planes.layer = LayerMask.NameToLayer("2D");
        planes.transform.position = new Vector3(rInfo.hObjetcs[number].initPosition.x + position, rInfo.hObjetcs[number].initPosition.y, rInfo.hObjetcs[number].initPosition.z);
        planes.transform.localScale = new Vector3(scale / 10, 1, plane.transform.localScale.z / 10);//new Vector3(scale / 10, 1, rInfo.hObjetcs[number].goScale.y / 10); //scale units 1 position = 10 scale
    }
}
