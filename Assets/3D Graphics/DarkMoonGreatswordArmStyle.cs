using UnityEngine;

// Dark Moon Greatsword generator
public class DarkMoonGreatswordArmStyle : MonoBehaviour
{
    GameObject sword;
    GameObject handle;
    GameObject elbowJoint;

    // =========================================================
    // PUBLIC MATERIALS
    // Assign these in the Unity Inspector
    // =========================================================

    public Material swordMaterial;
    public Material handleMaterial;


    void Start()
    {
        // =========================================================
        // SWORD — BLADE PROFILE
        // =========================================================

        Vector3[] swordProfile = new Vector3[]
        {
            new Vector3(-1f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(0.8f, 1f, 0f),
            new Vector3(0.7f, 7f, 0f),
            new Vector3(0f, 8.5f, 0f),
            new Vector3(-0.7f, 7f, 0f),
            new Vector3(-0.8f, 1f, 0f)
        };


        // =========================================================
        // HANDLE — GRIP PROFILE
        // =========================================================

        Vector3[] handleProfile = new Vector3[]
        {
            new Vector3(-1f, 0f, 0f),
            new Vector3(-2.2f, -0.1f, 0f),
            new Vector3(-2.2f, -0.5f, 0f),
            new Vector3(-2f, -0.6f, 0f),
            new Vector3(-1.5f, -0.5f, 0f),
            new Vector3(-0.25f, -0.5f, 0f),
            new Vector3(-0.25f, -3f, 0f),
            new Vector3(0f, -3f, 0f),
            new Vector3(0.25f, -3f, 0f),
            new Vector3(0.25f, -0.5f, 0f),
            new Vector3(1.5f, -0.5f, 0f),
            new Vector3(2f, -0.6f, 0f),
            new Vector3(2.2f, -0.5f, 0f),
            new Vector3(2.2f, -0.1f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 0f, 0f)
        };


        // =========================================================
        // SWORD / HANDLE PATH
        // =========================================================

        Matrix4x4[] swordPath = new Matrix4x4[6];

        swordPath[0] =
            Matrix4x4.Scale(new Vector3(0, 0, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f));

        swordPath[1] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f));

        swordPath[2] =
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.0075f));

        swordPath[3] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.0075f));

        swordPath[4] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));

        swordPath[5] =
            Matrix4x4.Scale(new Vector3(0, 0, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));


        // =========================================================
        // HANDLE
        // =========================================================

        handle = new GameObject();
        handle.name = "Handle";

        MeshRenderer handleRenderer =
            handle.AddComponent<MeshRenderer>();

        // Apply public handle material
        handleRenderer.sharedMaterial = handleMaterial;

        MeshFilter handleFilter =
            handle.AddComponent<MeshFilter>();

        handleFilter.mesh =
            MeshUtilities.Sweep(
                handleProfile,
                swordPath,
                true);

        handle.transform.parent = transform;

        handle.transform.localPosition =
            new Vector3(0, 0, 0);

        handle.transform.localRotation =
            Quaternion.identity;


        // =========================================================
        // JOINT — CONNECTS SWORD AND HANDLE
        // =========================================================

        elbowJoint = new GameObject();
        elbowJoint.name = "Sword Joint";

        elbowJoint.transform.parent = transform;

        elbowJoint.transform.localPosition =
            new Vector3(0.5f, 2f, 0);

        elbowJoint.transform.localRotation =
            Quaternion.identity;


        // =========================================================
        // SWORD
        // =========================================================

        sword = new GameObject();
        sword.name = "Sword";

        MeshRenderer swordRenderer =
            sword.AddComponent<MeshRenderer>();

        // Apply public sword material
        swordRenderer.sharedMaterial = swordMaterial;

        MeshFilter swordFilter =
            sword.AddComponent<MeshFilter>();

        swordFilter.mesh =
            MeshUtilities.Sweep(
                swordProfile,
                swordPath,
                true);

        // Sword is controlled by the joint
        sword.transform.parent =
            elbowJoint.transform;

        // Move back so the joint is at the connection point
        sword.transform.localPosition =
            new Vector3(-0.5f, -2f, 0);

        sword.transform.localRotation =
            Quaternion.identity;
    }
}