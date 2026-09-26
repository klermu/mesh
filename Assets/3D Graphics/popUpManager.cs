using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple manager to pop up a list of GameObjects at configured times (relative to sequence start)
public class PopUpManager : MonoBehaviour
{
    [Tooltip("Objects to show/hide by the manager.")]
    public List<GameObject> targets = new List<GameObject>();

    [Tooltip("Time (seconds) after the sequence starts when the corresponding object will be shown.")]
    public List<float> showAt = new List<float>();

    [Tooltip("Duration (seconds) each object stays visible. If shorter than showAt list, last value is reused.")]
    public List<float> durations = new List<float>();

    [Tooltip("If true the sequence starts automatically on Awake.")]
    public bool startOnAwake = true;

    [Tooltip("Loop the sequence when it finishes.")]
    public bool loop = false;

    Coroutine sequenceCoroutine;

    void Awake()
    {
        // Ensure all targets start disabled (optional behavior) — leave as-is if you want existing state
        foreach (var go in targets)
            if (go != null) go.SetActive(false);

        if (startOnAwake)
            StartSequence();
    }

    // Start the popup sequence. If already running it will be restarted.
    public void StartSequence()
    {
        if (sequenceCoroutine != null)
            StopCoroutine(sequenceCoroutine);
        sequenceCoroutine = StartCoroutine(RunSequence());
    }

    // Stop the sequence and hide all targets
    public void StopSequence()
    {
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
        HideAll();
    }

    // Immediately hide all managed targets
    public void HideAll()
    {
        foreach (var go in targets)
            if (go != null) go.SetActive(false);
    }

    IEnumerator RunSequence()
    {
        if (targets == null || targets.Count == 0) yield break;

        // Normalize list sizes: showAt should have at least targets.Count entries
        while (showAt.Count < targets.Count)
            showAt.Add(0f);

        // Ensure durations has at least one value (default 1s)
        if (durations == null) durations = new List<float>();
        if (durations.Count == 0) durations.Add(1f);

        do
        {
            float startTime = Time.time;

            // For each target schedule its show/hide relative to startTime
            for (int i = 0; i < targets.Count; i++)
            {
                var go = targets[i];
                float showTime = Mathf.Max(0f, showAt[i]);
                float duration = durations[Mathf.Min(i, durations.Count - 1)];

                // Wait until it's time to show this target
                float t = startTime + showTime - Time.time;
                if (t > 0f)
                    yield return new WaitForSeconds(t);

                if (go != null) go.SetActive(true);

                // Hide after duration
                if (duration > 0f)
                {
                    yield return new WaitForSeconds(duration);
                    if (go != null) go.SetActive(false);
                }
            }

            // Sequence finished
            if (!loop) break;
        } while (loop);

        sequenceCoroutine = null;
    }
}
