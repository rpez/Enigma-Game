using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Order Data", order = 51)]
public class Order : ScriptableObject
{

    public int round;
    public int time;
    public int timeLimit;

    public string option1;
    public int successValue1; // negative or positive value describing the effect the decission has to the war
    public string effects1; // narrative description of what happened

    public string option2;
    public int successValue2;
    public string effects2;

    [TextArea(3, 15)] public string description;
}
