using UnityEngine;

public class dearLeg : MonoBehaviour
{
    

    public Material sharedMaterial;

    GameObject arm1;
    GameObject arm2;

    GameObject baseJoint;
    GameObject elbowJoint;


    // ============================================================
    // START
    // ============================================================

    void Start()
    {
     

        CreateJoints();

        CreateArm1();
        CreateArm2();
    }


    // ============================================================
    // LAMP BASE
    // ============================================================

  

    // ============================================================
    // ARM PROFILE
    // ============================================================

    Vector3[] GetArmProfile()
    {
        return new Vector3[]
        {
            new Vector3(0.0f, -0.12f, 0.0f),
            new Vector3(0.014f, -0.114f, 0.0f),
            new Vector3(0.02f, -0.1f, 0.0f),

            new Vector3(0.02f, 0.1f, 0.0f),

            new Vector3(0.014f, 0.114f, 0.0f),
            new Vector3(0.0f, 0.12f, 0.0f),

            new Vector3(-0.014f, 0.114f, 0.0f),
            new Vector3(-0.02f, 0.1f, 0.0f),

            new Vector3(-0.02f, -0.1f, 0.0f),
            new Vector3(-0.014f, -0.114f, 0.0f)
        };
    }


    // ============================================================
    // ARM PATH
    // ============================================================

    Matrix4x4[] GetArmPath()
    {
        Matrix4x4[] armPath =
            new Matrix4x4[10];

        // Start cap
        armPath[0] =
            Matrix4x4.Scale(
                new Vector3(0, 0, 1)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f)
            );


        // Start bevel
        armPath[1] =
            Matrix4x4.Scale(
                new Vector3(0.9f, 0.98f, 1)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f)
            );


        // Duplicate for sharp bevel
        armPath[2] =
            Matrix4x4.Scale(
                new Vector3(0.9f, 0.98f, 1)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f)
            );


        // Start of straight section
        armPath[3] =
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.0075f)
            );


        // End of straight section
        armPath[4] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.0075f)
            );


        // End bevel
        armPath[5] =
            Matrix4x4.Scale(
                new Vector3(0.9f, 0.98f, 1)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f)
            );


        // Duplicate for sharp bevel
        armPath[6] =
            Matrix4x4.Scale(
                new Vector3(0.9f, 0.98f, 1)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f)
            );


        // Cap
        armPath[7] =
            Matrix4x4.Scale(
                new Vector3(0, 0, 1)
            ) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f)
            );


        // Extra transforms to keep the sweep stable
        armPath[8] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0)
            );

        armPath[9] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0)
            );

        return armPath;
    }


    // ============================================================
    // CREATE ARM 1
    // ============================================================

    void CreateArm1()
    {
        arm1 = new GameObject("Lamp Arm 1");

        MeshFilter filter =
            arm1.AddComponent<MeshFilter>();

        MeshRenderer renderer =
            arm1.AddComponent<MeshRenderer>();
        if (sharedMaterial != null)
            renderer.sharedMaterial = sharedMaterial;

        filter.mesh =
            MeshUtilities.Sweep(
                GetArmProfile(),
                GetArmPath(),
                false
            );

        // IMPORTANT:
        // Arm 1 belongs to Base Joint
        arm1.transform.parent =
            baseJoint.transform;

        arm1.transform.localPosition =
            new Vector3(0, 0.1f, 0);

        arm1.transform.localRotation =
            Quaternion.identity;
    }


    // ============================================================
    // CREATE ARM 2
    // ============================================================

    void CreateArm2()
    {
        arm2 = new GameObject("Lamp Arm 2");

        MeshFilter filter =
            arm2.AddComponent<MeshFilter>();

        MeshRenderer renderer =
            arm2.AddComponent<MeshRenderer>();
        if (sharedMaterial != null)
            renderer.sharedMaterial = sharedMaterial;

        filter.mesh =
            MeshUtilities.Sweep(
                GetArmProfile(),
                GetArmPath(),
                false
            );

        // IMPORTANT:
        // Arm 2 belongs to Elbow Joint
        arm2.transform.parent =
            elbowJoint.transform;

        arm2.transform.localPosition =
            new Vector3(0, 0.1f, 0);

        arm2.transform.localRotation =
            Quaternion.identity;
    }


    // ============================================================
    // CREATE JOINTS
    // ============================================================

    void CreateJoints()
    {

        // --------------------------------------------------------
        // BASE JOINT
        // --------------------------------------------------------

        baseJoint = new GameObject("Base Joint");
        baseJoint.transform.parent = transform;
        baseJoint.transform.localPosition = Vector3.zero;
        baseJoint.transform.localRotation = Quaternion.identity;

        // --------------------------------------------------------
        // ELBOW JOINT
        // --------------------------------------------------------
        // --------------------------------------------------------

        elbowJoint =
            new GameObject("Elbow Joint");

        elbowJoint.transform.parent =
            baseJoint != null ? baseJoint.transform : transform;

        elbowJoint.transform.localPosition =
            new Vector3(0, 0.2f, 0);

        elbowJoint.transform.localRotation =
            Quaternion.Euler(
                0,
                0,
                45
            );


        // --------------------------------------------------------
        // SHADE JOINT
        // --------------------------------------------------------

       
    }


    // (shade removed)
}
