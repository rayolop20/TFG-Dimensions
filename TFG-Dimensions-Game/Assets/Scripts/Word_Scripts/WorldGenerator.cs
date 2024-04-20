using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RealRender2D;

public class WorldGenerator : MonoBehaviour
{

    public GameObject plane;
    public RealRender2D rInfo;
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
               if (numberObjects < rInfo.hObjetcs.Count)
               {

                   GameObject newPlane = Instantiate(plane, Vector3.zero, Quaternion.Euler(0, 0, -90));
  
                   
                   dictPlanes.Add(rInfo.hObjetcs[i].id, newPlane);

                   //planeList.Add(newPlanesobj);
                   numberObjects++;
               }
               else if (numberObjects > rInfo.hObjetcs.Count)
               {
                    if (!dictPlanes.ContainsKey(rInfo.hObjetcs[i].id))
                    {
                        dictPlanes.Remove(i);
                        numberObjects--;
                    }
               }
           }

       }

        if (numberObjects == rInfo.hObjetcs.Count)
        {
            for (int i = 0; i < dictPlanes.Count; i++)//actualitzar la posició
            {
                float scale = getObjectScale(i);
                float position = getObjectPos2D(scale);
                UpdateFloor(dictPlanes[i], i, scale, position);
            }
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
   private void UpdateFloor(GameObject planes, int number, float scale, float position)
   {
        planes.layer = LayerMask.NameToLayer("2D");
        planes.transform.position = new Vector3(0, 0, rInfo.hObjetcs[number].initPosition.z + position);
        planes.transform.localScale = new Vector3(plane.transform.localScale.x / 10, 1, scale / 10); //scale units 1 position = 10 scale
   }
}
