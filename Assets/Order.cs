using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Order Data", order = 51)]
public class Order : ScriptableObject
{
    [SerializeField]
    private string description;
    [SerializeField]
    private int round;
    [SerializeField]
    private int time;
    [SerializeField]
    private int timeLimit;

    [SerializeField]
    private string option1;
    [SerializeField]
    private int successValue1; // negative or positive value describing the effect the decission has to the war
    [SerializeField]
    private string effects1; // narrative description of what happened

    [SerializeField]
    private string option2;
    [SerializeField]
    private int successValue2;
    [SerializeField]
    private string effects2;

}
