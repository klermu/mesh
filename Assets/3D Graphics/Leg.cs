using UnityEngine;

public class Leg : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        Vector3[] leg = new Vector3[] {
            new Vector3(-0.4f, 0.0f, 0.0f),
            new Vector3(-0.4f, 0.4f, 0.0f),
            new Vector3(-0.39f, 0.8f, 0.0f),
            new Vector3(-0.33f, 1.4f, 0.0f),
            new Vector3(-0.26f, 1.6f, 0.0f),
            new Vector3(-0.2f, 1.7f, 0.0f),
            new Vector3(-0.0f, 1.85f, 0.0f),
            new Vector3(0.2f, 1.85f, 0.0f),
            new Vector3(0.4f, 1.79f, 0.0f),
            new Vector3(0.6f, 1.5f, 0.0f),
            new Vector3(0.6f, 1.3f, 0.0f),
            new Vector3(0.55f, 1.0f, 0.0f),
            new Vector3(0.33f, 0.5f, 0.0f),
            new Vector3(0.3f, 0.0f, 0.0f),
            new Vector3(0.25f, -0.2f, 0.0f),
            new Vector3(0.22f, -0.3f, 0.0f),
            new Vector3(0.1f, -0.43f, 0.0f),
            new Vector3(0.0f, -0.47f, 0.0f),
            new Vector3(-0.2f, -0.47f, 0.0f),
            new Vector3(-0.35f, -0.3f, 0.0f),
            new Vector3(-0.4f, 0.0f, 0.0f)
        };


        Matrix4x4[] armPath = new Matrix4x4[6];
        armPath[0] = Matrix4x4.Scale(new Vector3(0, 0, 1)) *
                     Matrix4x4.Translate(new Vector3(0, 0, -0.01f));

        armPath[1] = Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 1)) *
             Matrix4x4.Translate(new Vector3(0, 0, -0.01f));

        armPath[2] = Matrix4x4.Translate(new Vector3(0, 0, -0.0075f));

        armPath[3] = Matrix4x4.Translate(new Vector3(0, 0, 0.0075f));

        armPath[4] = Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 1)) *
             Matrix4x4.Translate(new Vector3(0, 0, 0.01f));

        armPath[5] = Matrix4x4.Scale(new Vector3(0, 0, 1)) *
                     Matrix4x4.Translate(new Vector3(0, 0, 0.01f));

        arm1 = new GameObject();
        arm1.name = "Lamp Arm1";

        meshRenderer = arm1.AddComponent<MeshRenderer>();

        meshFilter = arm1.AddComponent<MeshFilter>();
        meshFilter.mesh = MeshUtilities.Sweep(leg, armPath, false);


    }

}
