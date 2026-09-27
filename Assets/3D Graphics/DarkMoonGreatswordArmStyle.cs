using System.Collections;
using System.Collections.Generic;
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
            new Vector3(-0.2f, 0f, 0f),
            new Vector3(0.2f, 0f, 0f),
            new Vector3(0.2f, 1.5f, 0f),
            new Vector3(0f, 4f, 0f),
            new Vector3(-0.2f, 1.5f, 0f)
        };


        // =========================================================
        // HANDLE — GRIP PROFILE
        // =========================================================

        Vector3[] handleProfile = new Vector3[]
        {
            new Vector3(-0.5f, 0f, 0f),
            new Vector3(-1.1f, -0.05f, 0f),
            new Vector3(-1.1f, -0.25f, 0f),
            new Vector3(-1.0f, -0.3f, 0f),
            new Vector3(-0.75f, -0.25f, 0f),
            new Vector3(-0.125f, -0.25f, 0f),
            new Vector3(-0.125f, -1.5f, 0f),
            new Vector3(0f, -1.5f, 0f),
            new Vector3(0.125f, -1.5f, 0f),
            new Vector3(0.125f, -0.25f, 0f),
            new Vector3(0.75f, -0.25f, 0f),
            new Vector3(1.0f, -0.3f, 0f),
            new Vector3(1.1f, -0.25f, 0f),
            new Vector3(1.1f, -0.05f, 0f),
            new Vector3(0.5f, 0f, 0f),
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

    // Rotation to followers (optional)
    public List<Transform> Followers = new List<Transform>();
    public float rotateToFollowerDuration = 0.25f;
    int currentFollowerIndex = -1;

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

    // Public API: rotate the sword to face the next follower in the list.
    // Wraps to the first follower when reaching the end.
    public void RotateToNextFollower()
    {
        if (Followers == null || Followers.Count == 0) return;

        currentFollowerIndex = (currentFollowerIndex + 1) % Followers.Count;
        Transform next = Followers[currentFollowerIndex];
        if (next == null) return;

        StopAllCoroutines();
        StartCoroutine(RotateElbowToFollower(next, rotateToFollowerDuration));
    }

    IEnumerator RotateElbowToFollower(Transform target, float duration)
    {
        if (elbowJoint == null || sword == null) yield break;

        // Direction from sword (world pos) to the target
        Vector3 dir = target.position - sword.transform.position;
        if (dir.sqrMagnitude < 0.000001f) yield break;

        // Desired world rotation so the sword's forward points at target
        Quaternion desiredSwordWorldRot = Quaternion.LookRotation(dir.normalized, Vector3.up);

        // Convert desired sword world rotation into the elbow's rotation by removing the sword's local rotation
        Quaternion swordLocalRot = sword.transform.localRotation;
        Quaternion desiredElbowRot = desiredSwordWorldRot * Quaternion.Inverse(swordLocalRot);

        Quaternion startRot = elbowJoint.transform.rotation;
        float t = 0f;
        while (t < duration)
        {
            float p = t / Mathf.Max(0.0001f, duration);
            elbowJoint.transform.rotation = Quaternion.Slerp(startRot, desiredElbowRot, p);
            t += Time.deltaTime;
            yield return null;
        }

        elbowJoint.transform.rotation = desiredElbowRot;
    }
}





