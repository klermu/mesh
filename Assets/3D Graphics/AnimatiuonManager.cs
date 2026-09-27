using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Renamed and cleaned up: provides a simple way to interpolate a Transform through a list of positions
public class AnimationManager : MonoBehaviour
{
    // Transform to animate. If not set, falls back to this GameObject's transform.
    public Transform Target;

    // A pose pairs a position with a rotation so each position in the list has an
    // associated rotation visible in the inspector.
    [System.Serializable]
    public class Pose
    {
        public Vector3 position = Vector3.zero;
        public Vector3 rotation = Vector3.zero; // Euler degrees
        // Duration (seconds) to travel from this pose to the next pose. Shown under rotation in inspector.
        public float duration = 1f;
    }

    // Positions (with rotation) to move through (in local space).
    public List<Pose> poses = new List<Pose>
    {
        new Pose { position = new Vector3(0,0,0), rotation = Vector3.zero },
        new Pose { position = new Vector3(1,0,0), rotation = Vector3.zero },
        new Pose { position = new Vector3(0,1,0), rotation = Vector3.zero }
    };


    // Note: each Pose now contains its own duration to the next pose (poses[i].duration)

    // Loop the animation when finished.
    public bool loop = true;
    // If true, rotation will be interpolated smoothly over each segment's duration
    public bool turnOverDuration = false;
    // Note: rotations are stored per-pose in poses[i].rotation

    void Start()
    {
        if (Target == null) Target = transform;
        // compute and fix rotations per pose so they're available in the inspector/runtime
        EnsurePositionRotations();
        // ensure Target starts with the rotation for the first pose (if available)
        if (poses != null && poses.Count > 0)
            Target.localRotation = Quaternion.Euler(poses[0].rotation);
        StartCoroutine(AnimateThroughPositions());
    }

    // Populate the public 'rotations' list so there is one rotation per position.
    // If the user already provided rotations with the correct count, keep them.
    // Otherwise fill every position with the Target's current local rotation.
    void EnsurePositionRotations()
    {
        if (Target == null) Target = transform;
        if (poses == null || poses.Count == 0) return;

        int needed = poses.Count;
        for (int i = 0; i < needed; i++)
        {
            if (poses[i] == null) poses[i] = new Pose();
        }
    }

    // Returns the final rotation (local) of the Target after each segment finishes.
    // This uses the per-position 'rotations' list. Call EnsurePositionRotations first
    // to guarantee rotations has one entry per position.
    public List<Quaternion> GetSegmentFinalRotations()
    {
        var result = new List<Quaternion>();
        if (Target == null) Target = transform;
        if (poses == null || poses.Count < 2) return result;

        EnsurePositionRotations();
        int segmentCount = poses.Count - 1;
        for (int i = 0; i < segmentCount; i++)
        {
            result.Add(Quaternion.Euler(poses[i + 1].rotation));
        }

        return result;
    }

    IEnumerator AnimateThroughPositions()
    {
        if (poses == null || poses.Count == 0) yield break;

        // No global durations list any more. Each pose carries its own duration to the next pose.

        do
        {
            for (int i = 0; i < poses.Count - 1; i++)
            {
                Vector3 a = poses[i].position;
                Vector3 b = poses[i + 1].position;
                float duration = Mathf.Max(0.0001f, poses[i].duration);
                // rotation for this segment comes from the poses list (per-position)
                Quaternion rotA = Quaternion.Euler(poses[i].rotation);
                Quaternion rotB = Quaternion.Euler(poses[i + 1].rotation);

                // ensure rotation stays at the start-of-segment rotation while moving
                Target.localRotation = rotA;

                float t = 0f;
                while (t < duration)
                {
                    float percent = t / duration;
                    Target.localPosition = Vector3.Lerp(a, b, percent);

                    if (turnOverDuration)
                    {
                        Target.localRotation = Quaternion.Slerp(rotA, rotB, percent);
                    }

                    t += Time.deltaTime;
                    yield return null;
                }

                // ensure final position
                Target.localPosition = b;

                // ensure final rotation at segment end
                Target.localRotation = rotB;
            }
        } while (loop);

    }
}