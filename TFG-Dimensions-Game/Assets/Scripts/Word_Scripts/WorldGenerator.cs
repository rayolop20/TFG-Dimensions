using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class WorldGenerator : MonoBehaviour
{
    public GameObject plane;
    public RayOrder rOrdered;
    public Transform pInfo;
    Dictionary<int, GameObject> dictPlanes = new Dictionary<int, GameObject>();

    int numberObjects;

    void Start()
    {

    }

    void Update()
    {

        if (numberObjects != rOrdered.notRepItems.Count) // crerar numeror de objectes segons cada 2 punts
        {
            foreach (KeyValuePair<int, HitObjects> g in rOrdered.notRepItems)
            {
                if (numberObjects < rOrdered.notRepItems.Count && !dictPlanes.ContainsKey(g.Key))
                {
                    GameObject newPlane = Instantiate(plane, Vector3.zero, plane.transform.rotation);
                    dictPlanes.Add(g.Key, newPlane);
                    numberObjects++;
                }
            }
        }

        if (numberObjects > rOrdered.notRepItems.Count)
        {
            foreach (KeyValuePair<int, GameObject> removed in dictPlanes)
            {
                if (!rOrdered.notRepItems.ContainsKey(removed.Key))
                {
                    Destroy(dictPlanes[removed.Key].gameObject);
                    dictPlanes.Remove(removed.Key);
                    numberObjects--;
                    return;
                }
            }
        }

        if (numberObjects == rOrdered.notRepItems.Count)
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
        Vector3 distanceBewteenPoints = rOrdered.notRepItems[numobj].endPosition - rOrdered.notRepItems[numobj].initPosition;
        Vector3 centralPoint = distanceBewteenPoints / 2;
        return centralPoint;
    }
    private float GetScale(int numobj)
    {
       //Formula Euclidiana
        float Scale = Mathf.Sqrt(Mathf.Pow(rOrdered.notRepItems[numobj].endPosition.x - rOrdered.notRepItems[numobj].initPosition.x, 2) + Mathf.Pow(rOrdered.notRepItems[numobj].endPosition.z - rOrdered.notRepItems[numobj].initPosition.z, 2));
        return Scale;
    }
    private void UpdateFloor(GameObject planes, int number, float scale, Vector3 position)
    {
        planes.layer = LayerMask.NameToLayer("2D"); 
        planes.transform.position = new Vector3(rOrdered.notRepItems[number].initPosition.x + position.x, rOrdered.notRepItems[number].goGeneralVariables.transform.position.y, rOrdered.notRepItems[number].initPosition.z + position.z);
        planes.transform.localScale = new Vector3(scale / 10, 1, rOrdered.notRepItems[number].goGeneralVariables.transform.localScale.y / 10); //scale units 1 position = 10 scale
       // planes.transform.rotation = new Quaternion(planes.transform.rotation.x, planes.transform.rotation.y, rInfo.rotationPlayer.transform.rotation.z, planes.transform.rotation.w);
        planes.transform.eulerAngles = new Vector3(planes.transform.eulerAngles.x , 0, pInfo.transform.eulerAngles.y); //scale units 1 position = 10 scale  // rInfo.hObjetcs[number].goGeneralVariables.transform.eulerAngles.z
    }
}
