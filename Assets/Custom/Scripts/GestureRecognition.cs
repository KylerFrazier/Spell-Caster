using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class GestureRecognition : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftGestureChecks;
    [SerializeField] private GameObject rightGestureChecks;
    [SerializeField] private GameObject prefabTest;

    private List<TriggerCheckpoint> leftCheckpoints  = new List<TriggerCheckpoint>();
    private List<TriggerCheckpoint> rightCheckpoints = new List<TriggerCheckpoint>();

    private bool active = false;

    
    void Awake() {
        foreach (Transform checkpoint in leftGestureChecks. transform) {
            leftCheckpoints .Add(checkpoint.GetComponent<TriggerCheckpoint>());
        }
        foreach (Transform checkpoint in rightGestureChecks.transform) {
            rightCheckpoints.Add(checkpoint.GetComponent<TriggerCheckpoint>());
        }
        System.Diagnostics.Debug.Assert(
            leftCheckpoints.Count == rightCheckpoints.Count,
            " The number of lefthand checkpoints should equal the number of righthand checkpoints."
        );
    }

    void Update() {
        if (leftCheckpoints[0].isActive() && rightCheckpoints[0].isActive()) {
            if (!active) {
                active = true;
                Instantiate(
                    prefabTest,
                    (leftHand.transform.position + rightHand.transform.position)/2,
                    Quaternion.identity
                );
            }
        }
        else if (active)
            active = false;
    }
}
