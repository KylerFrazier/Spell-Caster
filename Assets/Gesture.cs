using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Gesture : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    [SerializeField] private GameObject leftGestureChecks;
    [SerializeField] private GameObject rightGestureChecks;

    [SerializeField] private float[] holdTimes;
    [SerializeField] private float[] moveTimes;
    private float holdTimer;
    private float moveTimer;

    [SerializeField] private GameObject spellPrefab;
    private GameObject spell;

    private List<TriggerCheckpoint> leftCheckpoints  = new List<TriggerCheckpoint>();
    private List<TriggerCheckpoint> rightCheckpoints = new List<TriggerCheckpoint>();
    
    [System.NonSerialized] public int state;
    
    private int n;

    void Awake() {
        foreach (Transform checkpoint in leftGestureChecks. transform) {
            leftCheckpoints .Add(checkpoint.GetComponent<TriggerCheckpoint>());
        }
        foreach (Transform checkpoint in rightGestureChecks.transform) {
            rightCheckpoints.Add(checkpoint.GetComponent<TriggerCheckpoint>());
        }
        Assert.IsTrue(
            leftCheckpoints.Count == rightCheckpoints.Count &&
            leftCheckpoints.Count == holdTimes.Length,
            " The number of lefthand checkpoints should equal the number of righthand checkpoints."
        );

        state = -1;
        holdTimer = 0f;
        moveTimer = 0f;
        n = leftCheckpoints.Count;
    }

    void Update() {
        if (state == -1 && isActive(state+1)) {
            spell = Instantiate(spellPrefab, center(), Quaternion.identity);
            incrementState();
        } 
        else if (0 <= state && state < n-1) {
            if (isActive(state+1))
                incrementState();
            else if (isActive(state))
                holdState();
            else
                moveState();
        } 
        else if (state == n-1) {
            if (isActive(state))
                holdState();
            else
                resetState(destroy: false);
        } 
        else {
            resetState();
        }
    }

    private bool isActive(int i) {
        if (i >= n)
            return false;
        return leftCheckpoints[i].isActive() && rightCheckpoints[i].isActive();
    }

    private Vector3 center() {
        return (leftHand.transform.position + rightHand.transform.position)/2;
    }

    private void holdState() {
        spell.transform.position = center();

        moveTimer = 0;
        
        if (holdTimer + Time.deltaTime <= holdTimes[state])
            holdTimer += Time.deltaTime;
        else
            holdTimer = holdTimes[state];
    }

    private void moveState() {
        spell.transform.position = center();

        holdTimer = 0;

        if (moveTimer + Time.deltaTime <= moveTimes[state])
            moveTimer += Time.deltaTime;
        else
            resetState();
    }

    private void incrementState() {
        state ++;
        holdTimer = 0;
        moveTimer = 0;
    }

    private void resetState(bool destroy = true) {
        state = -1;
        holdTimer = 0;
        moveTimer = 0;
        if (destroy)
            Destroy(spell);
        else
            spell = null;
    }
}
