using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides a simple way to move this Target Transform toward a list of objects (Followers)
public class AnimationManager : MonoBehaviour
{
    // Transform to animate. If not set, falls back to this GameObject's transform.
    public Transform Target;

    // Objects in the scene that Target will move toward (assigned in the inspector).
    public List<Transform> Followers = new List<Transform>();

    // Duration (seconds) for each follower's move to the Target. Length should be Followers.Count.
    // If shorter, remaining entries will use the last value or 1f by default.
    public List<float> durations = new List<float> { 1f };

    // Loop the animation when finished.
    public bool loop = true;
    // (impact feedback removed)

    void Start()
    {
        if (Target == null) Target = transform;
        StartCoroutine(AnimateFollowers());
    }

    IEnumerator AnimateFollowers()
    {
        if (Followers == null || Followers.Count == 0) yield break;

        // Ensure durations list is valid
        if (durations == null) durations = new List<float>();
        while (durations.Count < Mathf.Max(0, Followers.Count))
        {
            durations.Add(durations.Count > 0 ? durations[durations.Count - 1] : 1f);
        }

        do
        {
            for (int i = 0; i < Followers.Count; i++)
            {
                Transform targetObj = Followers[i];
                if (targetObj == null) continue;

                Vector3 startPos = Target.position;
                Quaternion startRot = Target.rotation;

                // Capture destination position at start of the move
                Vector3 destPos = targetObj.position;

                float duration = Mathf.Max(0.0001f, durations[i]);
                float t = 0f;
                while (t < duration)
                {
                    float percent = t / duration;

                    // move Target toward the follower (destination) in world space
                    Target.position = Vector3.Lerp(startPos, destPos, percent);

                    // smoothly rotate Target to face the current destination position
                    Vector3 dir = (destPos - Target.position);
                    if (dir.sqrMagnitude > 0.000001f)
                    {
                        Quaternion lookRot = Quaternion.LookRotation(dir.normalized, Vector3.up);
                        Target.rotation = Quaternion.Slerp(startRot, lookRot, percent);
                    }

                    t += Time.deltaTime;
                    yield return null;
                }

                // ensure final alignment
                Target.position = destPos;
                Vector3 finalDir = (targetObj.position - Target.position);
                if (finalDir.sqrMagnitude > 0.000001f)
                {
                    Target.rotation = Quaternion.LookRotation(finalDir.normalized, Vector3.up);
                }
            }
        } while (loop);
    }
}
