using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMaterial : MonoBehaviour
{
   public Material Trasaparent; // El nuevo material que deseas aplicar
   public Material Grass; // El nuevo material que deseas aplicar
   
   private Renderer objectRenderer;

   
   void Start()
   {
       
   }
   
   private void Update()
   {

      
   }

    public void SwapMat(GameObject plane, string tag) {

        objectRenderer = plane.gameObject.GetComponent<Renderer>();

        if (tag == "Ground")
        {
            objectRenderer.material = Grass;
        }
        
        if (tag == "Coin" || tag == "NoneExit")
        {
            objectRenderer.material = Trasaparent;
        }
    }
}
