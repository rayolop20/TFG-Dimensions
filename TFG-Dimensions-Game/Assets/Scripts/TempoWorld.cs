using System;
using System.Collections.Generic;
using UnityEngine;

public class TempoWorld : MonoBehaviour
{

    public GameObject plane;
    public TempRay rInfo;
    Dictionary<int, GameObject> dictPlanes = new Dictionary<int, GameObject>();
    //List<GamePlanes> planeList = new ();

    int numberObjects;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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

                    //planeList.Add(newPlanesobj);
                    numberObjects++;
                }
            }
            for (int i = 0; i < rInfo.hObjetcs.Count; i++)
            {

            }
        }// revisar

        if (numberObjects > rInfo.hObjetcs.Count)
        {
            foreach (int value in rInfo.removedObjects)
            {
                if (dictPlanes.ContainsKey(value))
                {
                    Destroy(dictPlanes[value].gameObject);
                    dictPlanes.Remove(value);
                    numberObjects--;
                }
            }
            rInfo.removedObjects.Clear();
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
        planes.transform.position = new Vector3(rInfo.hObjetcs[number].initPosition.x + position, 0, 0);
        planes.transform.localScale = new Vector3(scale / 10, 1, plane.transform.localScale.z/10); //scale units 1 position = 10 scale
    }
}
