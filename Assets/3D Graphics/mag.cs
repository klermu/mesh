using UnityEngine;

public class mag : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        // Ensure a MeshRenderer is present
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Try to find the Standard shader; fall back gracefully if it's missing
        

        // Ensure a MeshFilter is present and assign mesh if available
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        var mesh = MeshUtilities.Cylinder(8, 1, 2);
        if (mesh != null)
            meshFilter.mesh = mesh;
        else
            Debug.LogError("MeshUtilities.Cylinder returned null mesh.");
    }
}
