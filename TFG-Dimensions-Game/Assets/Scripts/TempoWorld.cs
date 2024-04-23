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

            for (int i = 0; i < rInfo.hObjetcs.Count; i++)
            {
                if (numberObjects < rInfo.hObjetcs.Count && !dictPlanes.ContainsKey(i))
                {
                    GameObject newPlane = Instantiate(plane, Vector3.zero, Quaternion.Euler(0, 0, -90));
                    dictPlanes.Add(rInfo.hObjetcs[i].id, newPlane);

                    //planeList.Add(newPlanesobj);
                    numberObjects++;
                }
            }
        }// revisar

        if (numberObjects > rInfo.hObjetcs.Count)
        {
            foreach (HitObjects value in rInfo.removedObjects)
            {
                if (dictPlanes.ContainsKey(value.id))
                {
                    Destroy(dictPlanes[value.id].gameObject);
                    dictPlanes.Remove(value.id);
                    numberObjects--;
                }
            }
            //for (int j = 0; j < rInfo.removedObjects.Count; j++)
            //{
            //    if (dictPlanes.ContainsKey(rInfo.removedObjects[j].id))
            //    {
            //        Destroy(dictPlanes[rInfo.removedObjects[j].id].gameObject);
            //        dictPlanes.Remove(j);
            //        numberObjects--;
            //    }
            //}

            rInfo.removedObjects.Clear();
        }

        if (numberObjects == rInfo.hObjetcs.Count)
        {
            foreach (KeyValuePair<int, GameObject> item in dictPlanes)
            {
                int index = item.Key; // Clave del diccionario
                GameObject plane = item.Value;
                float scale = getObjectScale(index);
                float position = getObjectPos2D(scale);
                UpdateFloor(plane, index, scale, position);
            }
        }



    }

    private float getObjectScale(int numobj)
    {
        float scale = rInfo.hObjetcs[numobj].endPosition.z - rInfo.hObjetcs[numobj].initPosition.z;
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
        planes.transform.position = new Vector3(0, 0, rInfo.hObjetcs[number].initPosition.z + position);
        planes.transform.localScale = new Vector3(plane.transform.localScale.x / 10, 1, scale / 10); //scale units 1 position = 10 scale
    }
}
