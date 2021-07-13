using System.Collections;
using UnityEngine;

public class TriggerCheckpoint : MonoBehaviour
{

    private enum TriggerSides
	{
		Auto,
        Left,
		Right
	}
    [SerializeField] private TriggerSides TriggerSide = TriggerSides.Auto;
    
    private string TriggerTag;

    [System.NonSerialized] public bool active;

    void Awake() {
        if (TriggerSide == TriggerSides.Auto) {
            if (transform.parent.name.Contains("Right")) {
                TriggerSide = TriggerSides.Right;
                TriggerTag = "RightHand";
            }
            else {
                TriggerSide = TriggerSides.Left;
                TriggerTag = "LeftHand";
            }
        }
        active = false;
    }

    void OnTriggerStay(Collider hand) {
        if (hand.tag == TriggerTag)
            active = true;
    }

    void OnTriggerExit(Collider hand) {
        if (hand.tag == TriggerTag)
            active = false;
    }
}
