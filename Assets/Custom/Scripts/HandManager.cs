using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandManager : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;
    [SerializeField] private GameObject[] leftTracked;
    [SerializeField] private GameObject[] rightTracked;
    private bool prev;

    void Awake()
    {
        prev = false;
    }
    
    void Update()
    {
        if (OVRPlugin.GetHandTrackingEnabled()){
            leftHand.SetActive(true);
            rightHand.SetActive(true);
            leftController.SetActive(false);
            rightController.SetActive(false);

            if (!prev){
                Debug.Log("SWITCHED TO HANDS!!!");
                foreach (GameObject leftItem in leftTracked) {
                    leftItem.transform.Translate(0.11f, 0.09f, 0f);
                    leftItem.transform.Rotate(0f, 90f, 90f);
                }
                foreach (GameObject rightItem in rightTracked) {
                    rightItem.transform.Rotate(0f, -90f, 90f);
                    rightItem.transform.Translate(-0.03f, 0f, 0.11f);
                }
                prev = true;
            }
        }
        else {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            leftController.SetActive(true);
            rightController.SetActive(true);

            if (prev){
                Debug.Log("SWITCHED TO CONTROLLERS!!!");
                foreach (GameObject leftItem in leftTracked) {
                    leftItem.transform.Rotate(-90f, 0f, -90f);
                    leftItem.transform.Translate(-0.11f, -0.09f, 0f);
                }
                foreach (GameObject rightItem in rightTracked) {
                    rightItem.transform.Translate(0.03f, 0f, -0.11f);
                    rightItem.transform.Rotate(90f, 0f, -90f);
                }
                prev = false;
            }
        }
    }
}
