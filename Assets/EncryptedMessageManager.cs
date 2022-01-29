using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EncryptedMessageManager : MonoBehaviour
{
    public GameObject letterFieldPrefab;
    public float spaceBetweenLetters;
    public float lettersPerRow;

    private List<GameObject> letterFieldObjects = new List<GameObject>();
    private List<TMP_InputField> letterFieldTexts;
    private List<LetterField> letterFields;

    private Dictionary<string, List<int>> letterIndexConnections;
    private string[] currentLetters;

    public void GenerateMessage(string text)
    {
        foreach (GameObject letterObj in letterFieldObjects)
        {
            Destroy(letterObj);
        }
        letterFieldObjects = new List<GameObject>();
        letterFieldTexts = new List<TMP_InputField>();
        letterFields = new List<LetterField>();
        letterIndexConnections = new Dictionary<string, List<int>>();
        currentLetters = new string[text.Length];

        int whiteSpaceOffset = 0;
        int xOffset = 0;
        int yOffset = 0;
        int currentRowLetters = 0;
        for (int i = 0; i < text.Length; i++)
        {
            string letter = text[i].ToString();
            int index = i - whiteSpaceOffset;
            if (letter == "\n")
            {
                currentRowLetters = 0;
                xOffset = 0;
                yOffset++;
                whiteSpaceOffset++;
            }
            else if (letter != " ")
            {
                GameObject obj = GameObject.Instantiate(letterFieldPrefab, transform);
                obj.name = "TextField" + index;
                letterFieldObjects.Add(obj);
                obj.transform.position = obj.transform.position + Vector3.right * xOffset * spaceBetweenLetters + Vector3.down * yOffset * spaceBetweenLetters;

                TMP_InputField field = obj.GetComponent<TMP_InputField>();
                letterFieldTexts.Add(field);

                LetterField letterScript = obj.GetComponent<LetterField>();
                letterScript.Initalize(letter, UpdateCipherLetters, index, UpdateHighlights);
                letterFields.Add(letterScript);

                field.placeholder.GetComponent<TMP_Text>().text = letter;
                currentLetters[index] = "";

                if (letterIndexConnections.ContainsKey(letter))
                {
                    letterIndexConnections[letter].Add(index);
                }
                else
                {
                    letterIndexConnections.Add(letter, new List<int>());
                    letterIndexConnections[letter].Add(index);
                }
                currentRowLetters++;
                xOffset++;
            }
            else
            {
                int nextWordLength = 0;
                int k = i + 1;
                while (k < text.Length && text[k].ToString() != " " && text[k].ToString() != "\n")
                {
                    nextWordLength++;
                    k++;
                }
                if (nextWordLength + currentRowLetters > lettersPerRow)
                {
                    currentRowLetters = 0;
                    xOffset = 0;
                    yOffset++;
                }
                else xOffset++;
                whiteSpaceOffset++;
            }
        }
    }

    public void UpdateCipherLetters(string letter, string updatedLetter)
    {
        for (int i = 0; i < currentLetters.Length; i++)
        {
            if (!letterIndexConnections[letter].Contains(i))
            {
                if (currentLetters[i] == updatedLetter) {
                    currentLetters[i] = "";
                    letterFieldTexts[i].text = "";
                }
            }
        }
        for (int i = 0; i < letterIndexConnections[letter].Count; i++)
        {
            int index = letterIndexConnections[letter][i];
            letterFieldTexts[index].text = updatedLetter;
            currentLetters[index] = updatedLetter;
        }
    }

    public void UpdateHighlights(int index, bool active)
    {
        for (int i = 0; i < letterIndexConnections[letterFields[index].encryptedLetter].Count; i++)
        {
            int k = letterIndexConnections[letterFields[index].encryptedLetter][i];
            if (k != index)
            {
                if (active)
                {
                    letterFields[k].StartHighlight(Color.grey);
                }
                else
                {
                    letterFields[k].StopHighlight();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //GenerateMessage(testString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
