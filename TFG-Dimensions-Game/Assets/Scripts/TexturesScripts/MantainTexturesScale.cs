using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class MaintainTextureScale : MonoBehaviour
{
    private Vector2 originalTiling;

    void Start()
    {
        // Obtener el componente MeshRenderer
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        // Guardar la tiling original de la textura
        if (renderer != null && renderer.material != null)
        {
            originalTiling = renderer.material.mainTextureScale; //guardar textura tiling
        }
    }

    void Update()
    {
        // Obtener el componente MeshRenderer
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        // Ajustar el tiling de la textura en base a la escala del objeto
        if (renderer != null && renderer.material != null)
        {
            Vector3 objectScale = transform.localScale;
            //Multiplicar escala del objecte per la textura per mantenir escala texture 
            renderer.material.mainTextureScale = new Vector2(originalTiling.x * objectScale.x, originalTiling.y * objectScale.y);

            
        }

    }
}