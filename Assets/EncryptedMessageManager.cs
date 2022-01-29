using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncryptedMessageManager : MonoBehaviour
{
    public GameObject letterFieldPrefab;

    private string testString = "This is a message";

    private List<GameObject> letterFieldObjects;

    public void GenerateMessage(string text)
    {
        letterFieldObjects = new List<GameObject>();
        for (int i = 0; i < text.Length; i++)
        {
            letterFieldObjects.Add(GameObject.Instantiate(letterFieldPrefab, transform));
            letterFieldObjects[i].transform.position = letterFieldObjects[i].transform.position + Vector3.right * i;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMessage(testString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
