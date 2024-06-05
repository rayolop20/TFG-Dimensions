using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LastRayorder : MonoBehaviour
{
    public RayLogic[] rInfo;
    [HideInInspector] public Dictionary<int, HitObjects> notRepItems = new();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rInfo.Count(); i++)
        {
            foreach (KeyValuePair<int, HitObjects> g in rInfo[i].hObjetcs)
            {
                if (!notRepItems.ContainsKey(g.Key))
                {
                    notRepItems.Add(g.Key, g.Value);
                }
            }
            for (int j = 0; j < rInfo[i].removeObjects.Count(); j++)
            {
                if (notRepItems.ContainsKey(rInfo[i].removeObjects[j]))
                {
                    notRepItems.Remove(rInfo[i].removeObjects[j]);
                    rInfo[i].removeObjects.Remove(rInfo[i].removeObjects[j]);
                }
            }
        }


    }
}
