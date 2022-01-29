using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EncryptedMessageManager : MonoBehaviour
{
    public GameObject letterFieldPrefab;

    private string testString = "This is a message";

    private List<GameObject> letterFieldObjects;
    private List<TMP_InputField> letterFieldTexts;
    private List<LetterField> letterFields;

    private Dictionary<string, List<int>> letterIndexConnections;
    private Dictionary<string, List<int>> updatedLetters =  new Dictionary<string, List<int>>();
    private string[] currentLetters;

    public void GenerateMessage(string text)
    {
        letterFieldObjects = new List<GameObject>();
        letterFieldTexts = new List<TMP_InputField>();
        letterFields = new List<LetterField>();
        letterIndexConnections = new Dictionary<string, List<int>>();
        currentLetters = new string[text.Length];

        int whiteSpaceOffset = 0;
        for (int i = 0; i < text.Length; i++)
        {
            string letter = text[i].ToString();
            if (letter != " ")
            {
                GameObject obj = GameObject.Instantiate(letterFieldPrefab, transform);
                obj.name = "TextField" + i;
                letterFieldObjects.Add(obj);
                obj.transform.position = obj.transform.position + Vector3.right * i * 20;

                TMP_InputField field = obj.GetComponent<TMP_InputField>();
                letterFieldTexts.Add(field);

                LetterField letterScript = obj.GetComponent<LetterField>();
                letterScript.Initalize(letter, UpdateCipherLetters);
                letterFields.Add(letterScript);

                field.placeholder.GetComponent<TMP_Text>().text = letter;
                currentLetters[i - whiteSpaceOffset] = "";

                if (letterIndexConnections.ContainsKey(letter))
                {
                    letterIndexConnections[letter].Add(i - whiteSpaceOffset);
                }
                else
                {
                    letterIndexConnections.Add(letter, new List<int>());
                    letterIndexConnections[letter].Add(i - whiteSpaceOffset);
                }
            }
            else
            {
                whiteSpaceOffset++;
            }
        }
    }

    public void UpdateCipherLetters(string letter, string updatedLetter)
    {
        //if (updatedLetters.ContainsKey(updatedLetter))
        //{
        //    for (int i = 0; i < updatedLetters[updatedLetter].Count; i++)
        //    {
        //        int index = updatedLetters[updatedLetter][i];
        //        letterFieldTexts[index].text = "";
        //    }
        //    updatedLetters.Remove(updatedLetter);
        //}
        //updatedLetters.Add(updatedLetter, new List<int>());
        for (int i = 0; i < letterIndexConnections[letter].Count; i++)
        {
            int index = letterIndexConnections[letter][i];
            //updatedLetters.Remove(currentLetters[index]);
            letterFieldTexts[index].text = updatedLetter;
            //updatedLetters[updatedLetter].Add(index);
            //currentLetters[index] = updatedLetter;
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
