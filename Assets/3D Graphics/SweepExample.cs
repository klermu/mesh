using UnityEngine;

// Example demonstrating two uses of MeshUtilities.Sweep:
// 1) An open sweep (a simple extruded rounded rectangle)
// 2) A closed sweep (revolve-style surface) using MakeCirclePath
public class SweepExample : MonoBehaviour
{
    // multiplier to widen the chest shape along X axis
    public float widthMultiplier = 15f;

    GameObject arm1;
    GameObject arm2;

    GameObject baseJoint;
    GameObject lampShade;
    GameObject lampJoint;
    GameObject elbowJoint;
    GameObject shadeJoint;
    GameObject lampBase;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    Mesh mesh;

    void Start()
    {
        // New chest profile points
        Vector3[] right =
{
             new Vector3(0f, 0f, 0f),
            new Vector3(2f, 0f, 0f),
            new Vector3(2.2f, -0.1f, 0f),
            new Vector3(2.2f, -0.5f, 0f),
            new Vector3(2f, -0.6f, 0f),
            new Vector3(1.5f, -0.5f, 0f),
            new Vector3(0.25f, -0.5f, 0f),
            new Vector3(0.25f, -3f, 0f),
            new Vector3(0f, -3f, 0f)
        };

        // Build symmetric profile by mirroring the right side across X
        var pts = new System.Collections.Generic.List<Vector3>();

        // Left side
        for (int i = right.Length - 2; i >= 1; i--)
        {
            pts.Add(new Vector3(-right[i].x, right[i].y, 0f));
        }

        // Top center point
        pts.Add(new Vector3(right[0].x, right[0].y, 0f));

        // Right side
        for (int i = 1; i < right.Length; i++)
        {
            pts.Add(new Vector3(right[i].x, right[i].y, 0f));
        }

        // Close loop
        if (pts.Count > 0)
            pts.Add(pts[0]);

        Vector3[] chest = pts.ToArray();

        Matrix4x4[] armPath = new Matrix4x4[6];

        armPath[0] = Matrix4x4.Scale(new Vector3(0, 0, 15)) *
                     Matrix4x4.Translate(new Vector3(0, 0, -0.01f));

        armPath[1] = Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
                     Matrix4x4.Translate(new Vector3(0, 0, -0.01f));

        armPath[2] = Matrix4x4.Translate(new Vector3(0, 0, -0.0075f));

        armPath[3] = Matrix4x4.Translate(new Vector3(0, 0, 0.0075f));

        armPath[4] = Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
                     Matrix4x4.Translate(new Vector3(0, 0, 0.01f));

        armPath[5] = Matrix4x4.Scale(new Vector3(0, 0, 15)) *
                     Matrix4x4.Translate(new Vector3(0, 0, 0.01f));

        arm1 = new GameObject();
        arm1.name = "Lamp Arm1";

        meshRenderer = arm1.AddComponent<MeshRenderer>();

        meshFilter = arm1.AddComponent<MeshFilter>();
        meshFilter.mesh = MeshUtilities.Sweep(chest, armPath, false);
    }
}
