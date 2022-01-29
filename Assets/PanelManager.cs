using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelManager : MonoBehaviour
{
    [SerializeField] Message[] messagesArray;
    [SerializeField] Order[] orderArray;
    [SerializeField] GameObject[] pageButtons;

    [SerializeField] OrderElement orderElementPrefab;
    [SerializeField] GameObject orderContainer;
    [SerializeField] GameObject encryptionContainer;

    const string alphabet = "abcdefghijklmnopqrstuvwxyz";
    Dictionary<string, string> cipher;
    List<Message> messages;
    List<Order> orders;

    private float roundTimer;
    private int round = 1;

    // Start is called before the first frame update
    void Start()
    {
        cipher = CreateCipher();
        roundTimer = 0f;
        messages = new List<Message>(messagesArray);
        orders = new List<Order>(orderArray);

        /*for (int i = 0; i < 4; i++)
        {
            OrderElement order = Instantiate(orderElementPrefab, orderContainer.transform);
            order.Initialize(orders[i]);
        }*/
        
    }

    public void SwitchMessage(int page)
    {
        NewMessage(messages[page].message);
    }

    // Update is called once per frame
    void Update()
    {
        roundTimer += Time.deltaTime;
        foreach (Message message in messages)
        {
            if (message.round == round && roundTimer >= message.time)
            {
                pageButtons[message.id].SetActive(true);
                string encryptedMessage = Encrypt(message.message);
                NewMessage(encryptedMessage);
                messages.Remove(message);
                break;
            }
        }
    }

    void NewMessage(string message)
    {
        encryptionContainer.GetComponent<EncryptedMessageManager>().GenerateMessage(message);
    }

    Dictionary<string, string> CreateCipher()
    {
        // Map every letter to another random letter. Can map to same letter.

        Dictionary<string, string> cipher = new Dictionary<string, string>();
        string lettersLeft = alphabet;

        System.Random r = new System.Random();
        string randomOrder = new string(alphabet.ToCharArray().OrderBy(s => (r.Next(2) % 2) == 0).ToArray()); //Said to be not very good random sort
        for (int i = 0; i <randomOrder.Length; i++)
        {
            cipher.Add(alphabet[i].ToString(), randomOrder[i].ToString());
        }
        /*foreach (char letter in alphabet)
        {
            int letterIndex = Random.Range(0, lettersLeft.Length - 1);
            cipher.Add(letter.ToString(), lettersLeft[letterIndex].ToString());
            lettersLeft.Remove(letterIndex);


            Debug.Log(alphabet + ", " + lettersLeft);
        }*/

        return cipher;
    }

    string Encrypt(string message)
    {
        Debug.Log(message);
        string encryptedMessage = "";
        foreach (char character in message)
        {
            string letter = character.ToString().ToLower();
            if (this.cipher.ContainsKey(letter)) encryptedMessage += this.cipher[letter];
            else encryptedMessage += letter;

        }
        Debug.Log(encryptedMessage);
        return encryptedMessage;
    }

}
