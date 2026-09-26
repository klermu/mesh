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
            new Vector3(0.5f, 0f, 0f),
            new Vector3(0.5f, 6f, 0f),
            new Vector3(0f, 8.5f, 0f),
            new Vector3(-0.5f, 6f, 0f),
            new Vector3(-0.5f, 0f, 0f)
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
            new Vector3(0, 5, 0);

        handle.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
        


        // =========================================================
        // JOINT — CONNECTS SWORD AND HANDLE
        // =========================================================

        elbowJoint = new GameObject();
        elbowJoint.name = "Sword Joint";

        elbowJoint.transform.parent = transform;

        elbowJoint.transform.localPosition =
            new Vector3(0, 0, 0);

        elbowJoint.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        



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
        sword.transform.localPosition = new Vector3(0, 5, 0);


        sword.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));

        
    }

    // Spin settings
    public Vector3 spinAxis = new Vector3(0f, 1f, 0f);
    public float spinSpeed = 90f; // degrees per second

    // Remember local position so the sword can be kept in place while rotating
    Vector3 swordLocalPosition;
    Vector3 handleLocalPosition;

    void Awake()
    {
        // ensure fields have sensible defaults
        if (spinAxis == Vector3.zero) spinAxis = new Vector3(0f, 1f, 0f);
    }

    void LateUpdate()
    {
        if (sword == null) return;

        // Keep sword and handle at their original local positions and spin them in place
        if (swordLocalPosition != Vector3.zero)
            sword.transform.localPosition = swordLocalPosition;
        if (handleLocalPosition != Vector3.zero && handle != null)
            handle.transform.localPosition = handleLocalPosition;

        // Rotate both around their local axes so they spin in place
        sword.transform.Rotate(spinAxis.normalized * spinSpeed * Time.deltaTime, Space.Self);
        if (handle != null)
            handle.transform.Rotate(spinAxis.normalized * spinSpeed * Time.deltaTime, Space.Self);
    }
}





