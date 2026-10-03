using UnityEngine;

// Spawns a set of base squares and cylinders at different positions on Start.
public class BaseOfStadium : MonoBehaviour
{
    [Tooltip("How many pairs to create")]
    public int count = 5;

    [Tooltip("Horizontal spacing between each pair")]
    public float spacing = 1.5f;

    [Tooltip("Half-size (extent) for the square base; final size = extent * 2")]
    public float squareExtent = 0.5f;

    // (cylinder removed)

    [Tooltip("Optional material applied to generated objects")]
    public Material material;

    void Start()
    {
        if (count <= 0) return;

        for (int i = 0; i < count; i++)
        {
            // Create square base
            GameObject square = new GameObject($"BaseSquare_{i}");
            square.transform.SetParent(transform, false);
            square.transform.localPosition = new Vector3(i * spacing, 0f, 0f);

            MeshFilter sf = square.AddComponent<MeshFilter>();
            MeshRenderer sr = square.AddComponent<MeshRenderer>();
            try
            {
                sf.sharedMesh = MeshUtilities.Cube(squareExtent);
            }
            catch (System.Exception e)
            {
                Debug.LogError("BaseOfStadium: failed to create cube mesh: " + e.Message);
            }
            if (material != null) sr.sharedMaterial = material;

            // (cylinder creation removed)
        }
    }
}
