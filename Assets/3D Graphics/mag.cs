using UnityEngine;

public class mag : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        // Create a shared material for both cylinders
        Shader shader = Shader.Find("Standard") ?? Shader.Find("Sprites/Default") ?? Shader.Find("UI/Default") ?? Shader.Find("Hidden/InternalErrorShader");
        Material sharedMat = shader != null ? new Material(shader) : null;

        for (int c = 0; c < 2; c++)
        {
            // Create cylinder GameObject
            GameObject cyl = new GameObject($"Cylinder{c + 1}");
            cyl.transform.parent = this.transform;
            cyl.transform.localPosition = new Vector3(c == 0 ? -1.5f : 1.5f, 0f, 0f);

            MeshFilter mf = cyl.AddComponent<MeshFilter>();
            MeshRenderer mr = cyl.AddComponent<MeshRenderer>();

            if (sharedMat != null)
                mr.sharedMaterial = sharedMat;

            Mesh mesh = MeshUtilities.Cylinder(8, 1f, 0.1f);
            if (mesh != null)
                mf.mesh = mesh;
            else
                Debug.LogError("MeshUtilities.Cylinder returned null mesh.", this);
        }
    }
}
