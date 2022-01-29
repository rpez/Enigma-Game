using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Message", menuName = "Message Data", order = 51)]
public class Message : ScriptableObject
{
    public int round;
    public int time;
    [TextArea(3, 15)] public string message;
    public int id;
}
