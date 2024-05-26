using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlaneCubeIntersection : MonoBehaviour
{
    public GameObject objeto; // Objeto cuyo Mesh vamos a intersectar
    public Vector3 planeNormal; // Normal del plano
    public float planeDistance; // Distancia del plano desde el origen
    List<Vector3> intersectionVertices = new List<Vector3>();
    void Update()
    {
        intersectionVertices.Clear();
        planeNormal = -Camera.main.transform.forward;
        MeshFilter meshFilter = objeto.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("El objeto no tiene un MeshFilter.");
            return;
        }

        Mesh mesh = meshFilter.mesh;
        Plane plane = new Plane(planeNormal, 0);



        intersectionVertices = IntersectMeshWithPlane(mesh, plane, objeto.transform);

    }

    List<Vector3> IntersectMeshWithPlane(Mesh mesh, Plane plane, Transform t)
    {

        List<Vector3> intersectionVertices = new List<Vector3>();

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = t.transform.TransformPoint(vertices[triangles[i]]) ;
            Vector3 v1 = t.transform.TransformPoint(vertices[triangles[i + 1]]) ;
            Vector3 v2 = t.transform.TransformPoint(vertices[triangles[i + 2]]) ;

            Vector3 intersection1, intersection2;
            if (TriangleIntersectsPlane(plane, v0, v1, v2, out intersection1, out intersection2))
            {
                if (!intersectionVertices.Contains(intersection1))
                {
                    intersectionVertices.Add(intersection1);
                }
                if (!intersectionVertices.Contains(intersection2))
                {
                    intersectionVertices.Add(intersection2);
                }
            }
        }

        return intersectionVertices;
    }
    bool TriangleIntersectsPlane(Plane plane, Vector3 v0, Vector3 v1, Vector3 v2, out Vector3 intersection1, out Vector3 intersection2)
    {
        intersection1 = Vector3.zero;
        intersection2 = Vector3.zero;

        bool side0 = plane.GetSide(v0);
        bool side1 = plane.GetSide(v1);
        bool side2 = plane.GetSide(v2);

        if (side0 == side1 && side1 == side2)
        {
            // Todos los vértices están en el mismo lado del plano, no hay intersección
            return false;
        }

        List<Vector3> points = new List<Vector3>();

        if (side0 != side1)
        {
            points.Add(LinePlaneIntersection(plane, v0, v1));
        }
        if (side1 != side2)
        {
            points.Add(LinePlaneIntersection(plane, v1, v2));
        }
        if (side2 != side0)
        {
            points.Add(LinePlaneIntersection(plane, v2, v0));
        }

        if (points.Count == 2)
        {
            intersection1 = points[0];
            intersection2 = points[1];
            return true;
        }

        return false;
    }

    Vector3 LinePlaneIntersection(Plane plane, Vector3 point1, Vector3 point2)
    {
        Vector3 direction = point2 - point1;
        float distance;
        Ray ray = new Ray(point1, direction);

        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private void OnDrawGizmos()
    {
        foreach (var v in intersectionVertices)
        {

            Gizmos.DrawSphere(v, 0.05f);
        }

    }


}