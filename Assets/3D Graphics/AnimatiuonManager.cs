using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Renamed and cleaned up: provides a simple way to interpolate a Transform through a list of positions
public class AnimationManager : MonoBehaviour
{
    // Transform to animate. If not set, falls back to this GameObject's transform.
    public Transform Target;

    // Positions to move through (in local space).
    public List<Vector3> positions = new List<Vector3>
    {
        new Vector3(0, 0, 0f),
        new Vector3(1, 0f, 0f),
        new Vector3(0f, 1f, 0f)
    };
    

    // Duration (seconds) for each segment between positions. Length should be positions.Count - 1.
    // If shorter, remaining segments will use the last value or 1f by default.
    public List<float> durations = new List<float> { 1f, 1f };

    // Loop the animation when finished.
    public bool loop = true;
    // Discrete turn applied after each segment completes
    public bool turnOnSegment = false;
    public float turnAngle = 90f;
    public Vector3 turnAxis = Vector3.up;

    void Start()
    {
        if (Target == null) Target = transform;
        StartCoroutine(AnimateThroughPositions());
    }

    IEnumerator AnimateThroughPositions()
    {
        if (positions == null || positions.Count == 0) yield break;

        // Ensure durations list is valid
        if (durations == null) durations = new List<float>();
        while (durations.Count < Mathf.Max(0, positions.Count - 1))
        {
            // If no duration specified, default to 1 second per segment
            durations.Add(durations.Count > 0 ? durations[durations.Count - 1] : 1f);
        }

        do
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                Vector3 a = positions[i];
                Vector3 b = positions[i + 1];
                float duration = Mathf.Max(0.0001f, durations[i]);
                float t = 0f;
                while (t < duration)
                {
                    float percent = t / duration;
                    Target.localPosition = Vector3.Lerp(a, b, percent);
                    t += Time.deltaTime;
                    yield return null;
                }
                Target.localPosition = b;
            }
        } while (loop);

       
    }
}
