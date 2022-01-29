using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
    Dictionary<char, char> cipher;

    // Start is called before the first frame update
    void Start()
    {
        cipher = CreateCipher();
        string encryptedMessage1 = Encrypt("HELLO WORLD");
        string encryptedMessage2 = Encrypt("HELLO AGAIN");
        Debug.Log(encryptedMessage1);
        Debug.Log(encryptedMessage2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Dictionary<char, char> CreateCipher()
    {
        // Map every letter to another random letter. Can map to same letter.

        Dictionary<char, char> cipher = new Dictionary<char, char>();
        string lettersLeft = alphabet;
        foreach (char letter in alphabet)
        {
            int letterIndex = Random.Range(0, lettersLeft.Length - 1);
            cipher.Add(letter, lettersLeft[letterIndex]);
            lettersLeft.Remove(letterIndex);
        }

        return cipher;
    }

    string Encrypt(string message)
    {
        string encryptedMessage = "";
        foreach (char letter in message)
        {
            if (this.cipher.ContainsKey(letter)) encryptedMessage += ("" + this.cipher[letter]);
            else encryptedMessage += letter;

        }
        return encryptedMessage;
    }

}
