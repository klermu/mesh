using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dark Moon Greatsword generator
public class sword : MonoBehaviour
{
    GameObject swordblade;
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

        swordblade = new GameObject();
        swordblade.name = "Sword Blade";

        MeshRenderer swordRenderer =
            swordblade.AddComponent<MeshRenderer>();

        // Apply public sword material
        swordRenderer.sharedMaterial = swordMaterial;

        MeshFilter swordFilter =
            swordblade.AddComponent<MeshFilter>();

        swordFilter.mesh =
            MeshUtilities.Sweep(
                swordProfile,
                swordPath,
                true);

        // Sword is controlled by the joint
        swordblade.transform.parent =
            elbowJoint.transform;

       

        
    }

}





