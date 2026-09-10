using UnityEngine;

// Dark Moon Greatsword generator
public class mag : MonoBehaviour
{
    GameObject handle;

    // =========================================================
    // PUBLIC MATERIALS
    // Assign these in the Unity Inspector
    // =========================================================

    public Material handleMaterial;


    void Start()
    {
        // (blade profile removed)
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

        // Additional profiles for more detailed sweep shapes
        Vector3[] profile1 =
        {
            new Vector3(-1.5f, 0f, 0f),
            new Vector3(-1.55f, 0.2f, 0f),
            new Vector3(-1.65f, 0.4f, 0f),
            new Vector3(-1.5f, 0.8f, 0f),
            new Vector3(-1.1f, 1.23f, 0f),
            new Vector3(-0.6f, 1.1f, 0f),
            new Vector3(-0.4f, 1.6f, 0f),
            new Vector3(0f, 1.7f, 0f),
            new Vector3(0f, 1.5f, 0f),
            new Vector3(-0.2f, 1.45f, 0f),
            new Vector3(-0.4f, 1.2f, 0f),
            new Vector3(-0.4f, 0.8f, 0f),
            new Vector3(-0.4f, 0.8f, 0f),
            new Vector3(-0.55f, 0.8f, 0f),
            new Vector3(-1f, 1f, 0f),
            new Vector3(-1.3f, 0.8f, 0f),
            new Vector3(-1.4f, 0.5f, 0f),
            new Vector3(-1.4f, 0.5f, 0f),
            new Vector3(-1.25f, 0.2f, 0f)
        };

        Vector3[] profile2 =
        {
            new Vector3(0f, 0.6f, 0f),
            new Vector3(-0.4f, 0.8f, 0f),
            new Vector3(-0.55f, 0.8f, 0f),
            new Vector3(-0.5f, 0.4f, 0f),
            new Vector3(-0.9f, 0.05f, 0f),
            new Vector3(-0.9f, 0.05f, 0f),
            new Vector3(-1.25f, 0.2f, 0f),
            new Vector3(-1.5f, 0f, 0f),
            new Vector3(0f, 0f, 0f)
        };

        Vector3[] profile3 =
        {
            new Vector3(0f, 0.6f, 0f),
            new Vector3(0.4f, 0.8f, 0f),
            new Vector3(0.55f, 0.8f, 0f),
            new Vector3(0.5f, 0.4f, 0f),
            new Vector3(0.9f, 0.05f, 0f),
            new Vector3(0.9f, 0.05f, 0f),
            new Vector3(1.25f, 0.2f, 0f),
            new Vector3(1.5f, 0f, 0f),
            new Vector3(0f, 0f, 0f)
        };

        Vector3[] profile4 =
        {
            new Vector3(1.5f, 0f, 0f),
            new Vector3(1.55f, 0.2f, 0f),
            new Vector3(1.65f, 0.4f, 0f),
            new Vector3(1.5f, 0.8f, 0f),
            new Vector3(1.1f, 1.23f, 0f),
            new Vector3(0.6f, 1.1f, 0f),
            new Vector3(0.4f, 1.6f, 0f),
            new Vector3(0f, 1.7f, 0f),
            new Vector3(0f, 1.5f, 0f),
            new Vector3(0.2f, 1.45f, 0f),
            new Vector3(0.4f, 1.2f, 0f),
            new Vector3(0.4f, 0.8f, 0f),
            new Vector3(0.4f, 0.8f, 0f),
            new Vector3(0.55f, 0.8f, 0f),
            new Vector3(1f, 1f, 0f),
            new Vector3(1.3f, 0.8f, 0f),
            new Vector3(1.4f, 0.5f, 0f),
            new Vector3(1.4f, 0.5f, 0f),
            new Vector3(1.25f, 0.2f, 0f)
        };

        Vector3[] profile5 =
        {
            new Vector3(0f, -0.6f, 0f),
            new Vector3(0.4f, -0.8f, 0f),
            new Vector3(0.55f, -0.8f, 0f),
            new Vector3(0.5f, -0.4f, 0f),
            new Vector3(0.9f, -0.05f, 0f),
            new Vector3(0.9f, -0.05f, 0f),
            new Vector3(1.25f, -0.2f, 0f),
            new Vector3(1.5f, 0f, 0f),
            new Vector3(0f, 0f, 0f)
        };

        Vector3[] profile6 =
        {
            new Vector3(0f, -0.6f, 0f),
            new Vector3(-0.4f, -0.8f, 0f),
            new Vector3(-0.55f, -0.8f, 0f),
            new Vector3(-0.5f, -0.4f, 0f),
            new Vector3(-0.9f, -0.05f, 0f),
            new Vector3(-0.9f, -0.05f, 0f),
            new Vector3(-1.25f, -0.2f, 0f),
            new Vector3(-1.5f, 0f, 0f),
            new Vector3(0f, 0f, 0f)
        };

        Vector3[] profile7 =
        {
            new Vector3(-1.5f, 0f, 0f),
            new Vector3(-1.55f, -0.2f, 0f),
            new Vector3(-1.65f, -0.4f, 0f),
            new Vector3(-1.5f, -0.8f, 0f),
            new Vector3(-1.1f, -1.23f, 0f),
            new Vector3(-0.6f, -1.1f, 0f),
            new Vector3(-0.4f, -1.6f, 0f),
            new Vector3(0f, -1.7f, 0f),
            new Vector3(0f, -1.5f, 0f),
            new Vector3(-0.2f, -1.45f, 0f),
            new Vector3(-0.4f, -1.2f, 0f),
            new Vector3(-0.4f, -0.8f, 0f),
            new Vector3(-0.4f, -0.8f, 0f),
            new Vector3(-0.55f, -0.8f, 0f),
            new Vector3(-1f, -1f, 0f),
            new Vector3(-1.3f, -0.8f, 0f),
            new Vector3(-1.4f, -0.5f, 0f),
            new Vector3(-1.4f, -0.5f, 0f),
            new Vector3(-1.25f, -0.2f, 0f)
        };

        Vector3[] profile8 =
        {
            new Vector3(1.5f, 0f, 0f),
            new Vector3(1.55f, -0.2f, 0f),
            new Vector3(1.65f, -0.4f, 0f),
            new Vector3(1.5f, -0.8f, 0f),
            new Vector3(1.1f, -1.23f, 0f),
            new Vector3(0.6f, -1.1f, 0f),
            new Vector3(0.4f, -1.6f, 0f),
            new Vector3(0f, -1.7f, 0f),
            new Vector3(0f, -1.5f, 0f),
            new Vector3(0.2f, -1.45f, 0f),
            new Vector3(0.4f, -1.2f, 0f),
            new Vector3(0.4f, -0.8f, 0f),
            new Vector3(0.4f, -0.8f, 0f),
            new Vector3(0.55f, -0.8f, 0f),
            new Vector3(1f, -1f, 0f),
            new Vector3(1.3f, -0.8f, 0f),
            new Vector3(1.4f, -0.5f, 0f),
            new Vector3(1.4f, -0.5f, 0f),
            new Vector3(1.25f, -0.2f, 0f)
        };



        // =========================================================
        // HANDLE PATH
        // Reuse the previous sword/handle path concept but name it for the handle
        // =========================================================

        Matrix4x4[] handlePath = new Matrix4x4[6];

        handlePath[0] =
            Matrix4x4.Scale(new Vector3(0, 0, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f));

        handlePath[1] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.01f));

        handlePath[2] =
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.0075f));

        handlePath[3] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.0075f));

        handlePath[4] =
            Matrix4x4.Scale(new Vector3(0.9f, 0.98f, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));

        handlePath[5] =
            Matrix4x4.Scale(new Vector3(0, 0, 15)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.01f));


        // =========================================================
        // SWEEP EACH PROFILE
        // Use the same handlePath for every profile and create a mesh
        // for each using MeshUtilities.Sweep.
        // =========================================================

        Vector3[][] profiles = new Vector3[][]
        {
            profile1, profile2, profile3, profile4,
            profile5, profile6, profile7, profile8
        };

        // parent object to group all profile parts
        GameObject profilesParent = new GameObject("amo");
        profilesParent.transform.parent = transform;
        profilesParent.transform.localPosition = Vector3.zero;
        profilesParent.transform.localRotation = Quaternion.identity;

        for (int i = 0; i < profiles.Length; i++)
        {
            GameObject part = new GameObject("Profile " + (i + 1));
            part.transform.parent = profilesParent.transform;
            part.transform.localPosition = Vector3.zero;
            part.transform.localRotation = Quaternion.identity;

            MeshFilter partFilter = part.AddComponent<MeshFilter>();
            MeshRenderer partRenderer = part.AddComponent<MeshRenderer>();
            partRenderer.sharedMaterial = handleMaterial;

            partFilter.mesh = MeshUtilities.Sweep(profiles[i], handlePath, true);
        }


        // (sword and joint removed)
    }

    // (loft helpers removed)
}