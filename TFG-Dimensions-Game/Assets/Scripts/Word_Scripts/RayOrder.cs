using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class RayOrder : MonoBehaviour
{
    public RayLogic rInfo;
    [HideInInspector] public Dictionary<int, HitObjects> notRepItems = new();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rInfo.hObjetcs.Count; i++)
        {
            foreach (KeyValuePair<int, HitObjects> g in rInfo.hObjetcs)
            {
                if (!notRepItems.ContainsKey(g.Key))
                {
                    notRepItems.Add(g.Key, g.Value);
                }
            }

        }
        for (int j = 0; j < rInfo.removeObjects.Count(); j++)
        {
            if (notRepItems.ContainsKey(rInfo.removeObjects[j]))
            {
                notRepItems.Remove(rInfo.removeObjects[j]);
                rInfo.removeObjects.Remove(rInfo.removeObjects[j]);
            }
        }

    }
}
