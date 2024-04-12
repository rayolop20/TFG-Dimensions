using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RealRender2D;

public class WorldGenerator : MonoBehaviour
{

    public GameObject plane;
    public RealRender2D rInfo;
    List<GameObject> planeList = new ();

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
                GameObject newPlane = Instantiate(plane, Vector3.zero, Quaternion.Euler(0, 0, -90));
                numberObjects++;
                planeList.Add(newPlane);
            }
        }
        for (int i = 0; i < planeList.Count; i++)//actualitzar la posició
        {
            float scale = getObjectScale(i);
            float position = getObjectPos2D(scale);
            UpdateFloor(i, scale, position);
        }

    }

    private float getObjectScale (int numobj)
    {
        float scale = rInfo.hObjetcs[numobj].endPosition.z - rInfo.hObjetcs[numobj].initPosition.z;
        return scale;
    }
    private float getObjectPos2D(float scale)
    {
        float position = scale / 2;
        return position;
    }
    private void UpdateFloor(int number, float scale, float position)
    {
        planeList[number].layer = LayerMask.NameToLayer("2D");
        planeList[number].transform.position = new Vector3(0, 0, rInfo.hObjetcs[number].initPosition.z + position);
        planeList[number].transform.localScale = new Vector3(plane.transform.localScale.x / 10, 1, scale / 10); //scale units 1 position = 10 scale
    }
}
