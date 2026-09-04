
using UnityEngine;

// =========================================================
// 3D CHARACTER GENERATOR
// Body + Apple-Style Round Head + 2-Part Arms + Legs + Joints
// Uses MeshUtilities.Sweep()
// =========================================================

public class CharacterGenerator : MonoBehaviour
{
    GameObject body;
    GameObject head;

    GameObject leftUpperArm;
    GameObject leftLowerArm;

    GameObject rightUpperArm;
    GameObject rightLowerArm;

    GameObject leftLeg;
    GameObject rightLeg;

    GameObject leftShoulderJoint;
    GameObject rightShoulderJoint;

    GameObject leftElbowJoint;
    GameObject rightElbowJoint;

    GameObject leftHipJoint;
    GameObject rightHipJoint;

    GameObject chestArmor;
    GameObject leftShoulderBlade;
    GameObject rightShoulderBlade;


    // =========================================================
    // PUBLIC MATERIALS
    // =========================================================

    public Material bodyMaterial;
    public Material skinMaterial;
    public Material armourMaterial;


    void Start()
    {
        // =========================================================
        // BODY PROFILE
        // =========================================================

        Vector3[] bodyProfile = new Vector3[]
        {
            new Vector3(-1.2f, -2f, 0f),
            new Vector3( 1.2f, -2f, 0f),
            new Vector3( 1.4f,  1.5f, 0f),
            new Vector3( 0.9f,  2f, 0f),
            new Vector3(-0.9f,  2f, 0f),
            new Vector3(-1.4f,  1.5f, 0f)
        };


        // =========================================================
        // APPLE-STYLE HEAD PROFILE
        // EXACT SAME POINTS AS YOUR APPLE CODE
        // =========================================================

        Vector3[] headProfile = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(1.2f, 1f, 0f),
            new Vector3(1f, 2f, 0f),
            new Vector3(0f, 2.3f, 0f)
        };


        // =========================================================
        // ARM PROFILE
        // =========================================================

        Vector3[] armProfile = new Vector3[]
        {
            new Vector3(-0.35f, 0f,   0f),
            new Vector3( 0.35f, 0f,   0f),
            new Vector3( 0.4f,  1.8f, 0f),
            new Vector3(-0.4f,  1.8f, 0f)
        };


        // =========================================================
        // LEG PROFILE
        // =========================================================

        Vector3[] legProfile = new Vector3[]
        {
            new Vector3(-0.45f, 0f,   0f),
            new Vector3( 0.45f, 0f,   0f),
            new Vector3( 0.45f, 1.5f, 0f),
            new Vector3(-0.45f, 1.5f, 0f)
        };


        // =========================================================
        // BODY / ARM / LEG PATH
        // =========================================================

        Matrix4x4[] path = new Matrix4x4[6];

        path[0] =
            Matrix4x4.Scale(new Vector3(0, 0, 1)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.5f)
            );

        path[1] =
            Matrix4x4.Scale(new Vector3(0.95f, 0.95f, 1)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.3f)
            );

        path[2] =
            Matrix4x4.Translate(
                new Vector3(0, 0, -0.1f)
            );

        path[3] =
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.1f)
            );

        path[4] =
            Matrix4x4.Scale(new Vector3(0.95f, 0.95f, 1)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.3f)
            );

        path[5] =
            Matrix4x4.Scale(new Vector3(0, 0, 1)) *
            Matrix4x4.Translate(
                new Vector3(0, 0, 0.5f)
            );


        // =========================================================
        // HEAD PATH
        // SAME METHOD AS YOUR APPLE SCRIPT
        // =========================================================

        int headDivisions = 32;

        Matrix4x4[] headPath =
            new Matrix4x4[headDivisions];

        for (int i = 0; i < headDivisions; i++)
        {
            float angle =
                2.0f * Mathf.PI * i / headDivisions;

            headPath[i] =
                Matrix4x4.TRS(
                    Vector3.zero,
                    Quaternion.Euler(
                        0,
                        angle * Mathf.Rad2Deg,
                        0
                    ),
                    Vector3.one
                );
        }


        // =========================================================
        // BODY
        // =========================================================

        body = CreatePart(
            "Body",
            bodyProfile,
            path,
            bodyMaterial,
            new Vector3(0f, 4f, 0f)
        );


        // =========================================================
        // HEAD
        // APPLE-STYLE ROUND HEAD
        // =========================================================

        head = CreatePart(
            "Head",
            headProfile,
            headPath,
            skinMaterial,
            new Vector3(0f, 5.8f, 0f)
        );


        // =========================================================
        // LEFT SHOULDER JOINT
        // =========================================================

        leftShoulderJoint = CreateJoint(
            "Left Shoulder Joint",
            new Vector3(-1.5f, 2f, 0f),
            body.transform
        );


        // =========================================================
        // RIGHT SHOULDER JOINT
        // =========================================================

        rightShoulderJoint = CreateJoint(
            "Right Shoulder Joint",
            new Vector3(1.5f, 2f, 0f),
            body.transform
        );


        // =========================================================
        // LEFT UPPER ARM
        // =========================================================

        leftUpperArm = CreatePart(
            "Left Upper Arm",
            armProfile,
            path,
            bodyMaterial,
            new Vector3(-1.5f, 3.2f, 0f)
        );

        leftUpperArm.transform.parent =
            leftShoulderJoint.transform;

        leftUpperArm.transform.localPosition =
            new Vector3(0f, -2f, 0f);


        // =========================================================
        // RIGHT UPPER ARM
        // =========================================================

        rightUpperArm = CreatePart(
            "Right Upper Arm",
            armProfile,
            path,
            bodyMaterial,
            new Vector3(1.5f, 3.2f, 0f)
        );

        rightUpperArm.transform.parent =
            rightShoulderJoint.transform;

        rightUpperArm.transform.localPosition =
            new Vector3(0f, -2f, 0f);


        // =========================================================
        // LEFT ELBOW JOINT
        // =========================================================

        leftElbowJoint = CreateJoint(
            "Left Elbow Joint",
            new Vector3(0f, -3.6f, 0f),
            leftShoulderJoint.transform
        );


        // =========================================================
        // RIGHT ELBOW JOINT
        // =========================================================

        rightElbowJoint = CreateJoint(
            "Right Elbow Joint",
            new Vector3(0f, -3.6f, 0f),
            rightShoulderJoint.transform
        );


        // =========================================================
        // LEFT LOWER ARM
        // =========================================================

        leftLowerArm = CreatePart(
            "Left Lower Arm",
            armProfile,
            path,
            skinMaterial,
            Vector3.zero
        );

        leftLowerArm.transform.parent =
            leftElbowJoint.transform;

        leftLowerArm.transform.localPosition =
            new Vector3(0f, -0.15f, 0f);


        // =========================================================
        // RIGHT LOWER ARM
        // =========================================================

        rightLowerArm = CreatePart(
            "Right Lower Arm",
            armProfile,
            path,
            skinMaterial,
            Vector3.zero
        );

        rightLowerArm.transform.parent =
            rightElbowJoint.transform;

        rightLowerArm.transform.localPosition =
            new Vector3(0f, -0.15f, 0f);


        // =========================================================
        // LEFT HIP JOINT
        // =========================================================

        leftHipJoint = CreateJoint(
            "Left Hip Joint",
            new Vector3(-0.65f, 2f, 0f),
            body.transform
        );


        // =========================================================
        // RIGHT HIP JOINT
        // =========================================================

        rightHipJoint = CreateJoint(
            "Right Hip Joint",
            new Vector3(0.65f, 2f, 0f),
            body.transform
        );


        // =========================================================
        // LEFT LEG
        // =========================================================

        leftLeg = CreatePart(
            "Left Leg",
            legProfile,
            path,
            bodyMaterial,
            Vector3.zero
        );

        leftLeg.transform.parent =
            leftHipJoint.transform;

        leftLeg.transform.localPosition =
            new Vector3(0f, -5.3f, 0f);


        // =========================================================
        // RIGHT LEG
        // =========================================================

        rightLeg = CreatePart(
            "Right Leg",
            legProfile,
            path,
            bodyMaterial,
            Vector3.zero
        );

        rightLeg.transform.parent =
            rightHipJoint.transform;

        rightLeg.transform.localPosition =
            new Vector3(0f, -5.3f, 0f);
    }


    // =========================================================
    // CREATE BODY PART
    // =========================================================

    GameObject CreatePart(
        string partName,
        Vector3[] profile,
        Matrix4x4[] path,
        Material material,
        Vector3 position)
    {
        GameObject part =
            new GameObject();

        part.name =
            partName;


        // =====================================================
        // MESH FILTER
        // =====================================================

        MeshFilter meshFilter =
            part.AddComponent<MeshFilter>();


        // =====================================================
        // MESH RENDERER
        // =====================================================

        MeshRenderer meshRenderer =
            part.AddComponent<MeshRenderer>();

        meshRenderer.sharedMaterial =
            material;


        // =====================================================
        // CREATE SWEEP MESH
        // =====================================================

        meshFilter.mesh =
            MeshUtilities.Sweep(
                profile,
                path,
                true
            );


        // =====================================================
        // TRANSFORM
        // =====================================================

        part.transform.parent =
            transform;

        part.transform.localPosition =
            position;

        part.transform.localRotation =
            Quaternion.identity;


        return part;
    }


    // =========================================================
    // CREATE JOINT
    // =========================================================

    GameObject CreateJoint(
        string jointName,
        Vector3 position,
        Transform parent)
    {
        GameObject joint =
            new GameObject();

        joint.name =
            jointName;

        joint.transform.parent =
            parent;

        joint.transform.localPosition =
            position;

        joint.transform.localRotation =
            Quaternion.identity;


        return joint;
    }
}

