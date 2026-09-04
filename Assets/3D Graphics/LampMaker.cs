using UnityEngine;

public class LampMaker : MonoBehaviour
{
    public GameObject lampBase;
    public GameObject arm1;
    public GameObject arm2;
    public GameObject lampShade;
    public GameObject baseJoint;
    public GameObject elbowJoint;
    public GameObject shadeJoint;

    void Start()
    {
        // shared material
        Shader shader = Shader.Find("Standard") ?? Shader.Find("Sprites/Default") ?? Shader.Find("UI/Default") ?? Shader.Find("Hidden/InternalErrorShader");
        Material mat = shader != null ? new Material(shader) : null;

        // Lamp base
        lampBase = new GameObject();
        lampBase.name = "Lamp Base";
        MeshRenderer meshRenderer = lampBase.AddComponent<MeshRenderer>();
        if (mat != null) meshRenderer.sharedMaterial = mat;
        MeshFilter meshFilter = lampBase.AddComponent<MeshFilter>();
        meshFilter.mesh = MeshUtilities.Cylinder(16, 0.02f, 0.01f);
        lampBase.transform.parent = transform;
        lampBase.transform.localPosition = new Vector3(0, 0.02f, 0);

        // Joints
        baseJoint = new GameObject();
        baseJoint.name = "Base Joint";
        baseJoint.transform.parent = lampBase.transform;
        baseJoint.transform.localPosition = new Vector3(0, 0, 0);

        elbowJoint = new GameObject();
        elbowJoint.name = "Elbow Joint";
        elbowJoint.transform.parent = baseJoint.transform;
        elbowJoint.transform.localPosition = new Vector3(0, 0.2f, 0);
        elbowJoint.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 45));

        shadeJoint = new GameObject();
        shadeJoint.name = "Shade Joint";
        shadeJoint.transform.parent = elbowJoint.transform;
        shadeJoint.transform.localPosition = new Vector3(0, 0.2f, 0);
        shadeJoint.transform.localRotation = Quaternion.identity;

        // Arm profile
        Vector3[] armProfile = new Vector3[] {
           new Vector3( 0.00f, -0.45f,  0.00f),
            new Vector3( 0.055f, -0.43f,  0.00f),
            new Vector3( 0.075f, -0.35f,  0.00f),
            new Vector3( 0.075f,  0.35f,  0.00f),
            new Vector3( 0.055f,  0.43f,  0.00f),
            new Vector3( 0.00f,  0.45f,  0.00f),
            new Vector3(-0.055f,  0.43f,  0.00f),
            new Vector3(-0.075f,  0.35f,  0.00f),
            new Vector3(-0.075f, -0.35f, 0.00f),
            new Vector3(-0.055f, -0.43f, 0.00f)
        };

        // Create the path with duplicated intermediate transforms for a sharp bevel
        Matrix4x4[] armPath = new Matrix4x4[10];
        armPath[0] = Matrix4x4.Scale(new Vector3(0, 0, 1)) * Matrix4x4.Translate(new Vector3(0, 0, -0.01f));
        armPath[1] = Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 1)) * Matrix4x4.Translate(new Vector3(0, 0, -0.01f));
        armPath[2] = armPath[1];
        armPath[3] = Matrix4x4.Translate(new Vector3(0, 0, -0.0075f));
        armPath[4] = armPath[3];
        armPath[5] = Matrix4x4.Translate(new Vector3(0, 0, 0.0075f));
        armPath[6] = armPath[5];
        armPath[7] = Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 1)) * Matrix4x4.Translate(new Vector3(0, 0, 0.01f));
        armPath[8] = armPath[7];
        armPath[9] = Matrix4x4.Scale(new Vector3(0, 0, 1)) * Matrix4x4.Translate(new Vector3(0, 0, 0.01f));

        // Create first arm
        arm1 = new GameObject();
        arm1.name = "Lamp Arm1";
        MeshRenderer arm1Renderer = arm1.AddComponent<MeshRenderer>();
        if (mat != null) arm1Renderer.sharedMaterial = mat;
        MeshFilter arm1Filter = arm1.AddComponent<MeshFilter>();
        arm1Filter.mesh = MeshUtilities.Sweep(armProfile, armPath, false);
        arm1.transform.parent = baseJoint.transform;
        arm1.transform.localPosition = new Vector3(0, 0.1f, 0);
        arm1.transform.localRotation = Quaternion.identity;

        // Create second arm (upper arm)
        arm2 = new GameObject();
        arm2.name = "Lamp Arm2";
        MeshRenderer arm2Renderer = arm2.AddComponent<MeshRenderer>();
        if (mat != null) arm2Renderer.sharedMaterial = mat;
        MeshFilter arm2Filter = arm2.AddComponent<MeshFilter>();
        arm2Filter.mesh = MeshUtilities.Sweep(armProfile, armPath, false);
        arm2.transform.parent = elbowJoint.transform;
        arm2.transform.localPosition = new Vector3(0, 0.1f, 0);
        arm2.transform.localRotation = Quaternion.identity;

        // Lamp shade profile (simple example)
        Vector3[] shadeProfile = new Vector3[] {
            new Vector3(0.0f, 0.05f, 0.0f),
            new Vector3(0.08f, 0.05f, 0.0f),
            new Vector3(0.095f, 0.03f, 0.0f),
            new Vector3(0.1f, 0.0f, 0.0f),
            new Vector3(0.095f, -0.03f, 0.0f),
            new Vector3(0.08f, -0.05f, 0.0f),
            new Vector3(0.0f, -0.05f, 0.0f)
        };

        Matrix4x4[] shadePath = MeshUtilities.MakeCirclePath(0f, 16);
        lampShade = new GameObject();
        lampShade.name = "Lamp Shade";
        MeshRenderer shadeRenderer = lampShade.AddComponent<MeshRenderer>();
        if (mat != null) shadeRenderer.sharedMaterial = mat;
        MeshFilter shadeFilter = lampShade.AddComponent<MeshFilter>();
        shadeFilter.mesh = MeshUtilities.Sweep(shadeProfile, shadePath, true);
        lampShade.transform.parent = shadeJoint.transform;
        lampShade.transform.localPosition = Vector3.zero;
        lampShade.transform.localRotation = Quaternion.identity;
    }
}
