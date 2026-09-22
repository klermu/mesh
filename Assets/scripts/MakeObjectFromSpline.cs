using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.LowLevelPhysics2D.PhysicsWorld;

public class MakeObjectFromSpline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure a MeshRenderer is present
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Ensure the renderer has a material so it can render in the scene
        if (meshRenderer.sharedMaterial == null)
        {
            var std = Shader.Find("Standard");
            if (std != null)
                meshRenderer.sharedMaterial = new Material(std);
        }

        // Ensure a MeshFilter is present
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();


        var spline = gameObject.GetComponent<SplineContainer>();
        // Create the mesh safely and assign if successful
        
        var path = MeshUtilities.MakePathFromSpline(spline, 100);


        Vector3[] SplineProfile =
            MeshUtilities.MakeCircleProfile(
                0.1f,
                3
            );


        Mesh mesh = null;
        try
        {
            mesh = MeshUtilities.Sweep(SplineProfile, path, false);
        }
        catch (System.Exception e)
        {
            Debug.LogError("MeshUtilities.Cube threw an exception: " + e);
        }

        if (mesh != null)
            meshFilter.sharedMesh = mesh;
        else
            Debug.LogError("Failed to create mesh in cylinder.Start");

    }
}
