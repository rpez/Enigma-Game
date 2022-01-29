using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] Message[] messagesArray;
    [SerializeField] Order[] orderArray;

    [SerializeField] OrderButton orderButtonPrefab;
    [SerializeField] GameObject orderContainer;

    const string alphabet = "abcdefghijklmnopqrstuvxyz";
    Dictionary<string, string> cipher;
    List<Message> messages;

    private float roundTimer;
    private int round = 1;

    // Start is called before the first frame update
    void Start()
    {
        cipher = CreateCipher();
        roundTimer = 0f;
        messages = new List<Message>(messagesArray);

        for (int i = 0; i < 10; i++)
        {
            OrderButton button = Instantiate(orderButtonPrefab, orderContainer.transform);
            button.Initialize("Send Units");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        roundTimer += Time.deltaTime;
        foreach (Message message in messages)
        {
            if (message.round == round && roundTimer >= message.time)
            {
                string encryptedMessage = Encrypt(message.message);
                NewMessage(encryptedMessage);
                messages.Remove(message);
                break;
            }
        }
    }

    void NewMessage(string message)
    {
        Debug.Log(message);
    }

    Dictionary<string, string> CreateCipher()
    {
        // Map every letter to another random letter. Can map to same letter.

        Dictionary<string, string> cipher = new Dictionary<string, string>();
        string lettersLeft = alphabet;
        foreach (char letter in alphabet)
        {
            int letterIndex = Random.Range(0, lettersLeft.Length - 1);
            cipher.Add(letter.ToString(), lettersLeft[letterIndex].ToString());
            lettersLeft.Remove(letterIndex);
        }

        return cipher;
    }

    string Encrypt(string message)
    {
        string encryptedMessage = "";
        foreach (char character in message)
        {
            string letter = character.ToString().ToLower();
            if (this.cipher.ContainsKey(letter)) encryptedMessage += this.cipher[letter];
            else encryptedMessage += letter;

        }
        return encryptedMessage;
    }

}
