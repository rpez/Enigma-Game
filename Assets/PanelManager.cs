using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class PanelManager : MonoBehaviour
{
    public TMP_Text timer;
    public TMP_Text statusBar;
    public float[] roundtimes;

    [SerializeField] Message[] messagesArray;
    [SerializeField] Order[] orderArray;
    [SerializeField] GameObject[] pageButtons;
    [SerializeField] EncryptedMessageManager[] encryptionPanels;
    [SerializeField] GameObject missionStartPanel;

    [SerializeField] OrderElement orderElementPrefab;
    [SerializeField] GameObject orderContainer;
    [SerializeField] GameObject encryptionContainer;

    const string alphabet = "abcdefghijklmnopqrstuvwxyz";
    Dictionary<string, string> cipher;
    List<Message> messages;
    List<Order> orders;

    List<OrderElement> orderElementActive = new List<OrderElement>();

    private float roundTimer;
    private int round = 1;
    //private bool roundOnGoing = true;

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
        //NewMessage(Encrypt(messages[page].message));
        for (int i = 0; i < encryptionPanels.Length; i++)
        {
            if (i == page) Debug.Log("Selected: " + i);
            encryptionPanels[i].gameObject.SetActive(i == page);
        }
    }

    // Update is called once per frame
    void Update()
    {
        roundTimer += Time.deltaTime;
        foreach (Message message in messages)
        {
            //Debug.Log(message.round +", "+roundTimer+)
            if (message.round == round && roundTimer >= message.time)
            {
                pageButtons[message.id].SetActive(true);
                
                if (message.encrypted)
                {
                    string encryptedMessage = Encrypt(message.message);
                    NewMessage(encryptedMessage, message.id);
                } else
                {
                    if (message.appendWithResults)
                    {
                        NewMessage("WEEK " + round + ":\n" + GameManager.Instances.currentSummary + "\n\n" + message.message, message.id);
                    } else
                    {
                        NewMessage("WEEK " + round + ":\n" + message.message, message.id);
                    }
                    
                }
                messages.Remove(message);


                break;
            }
        }

        foreach (Order order in orders)
        {
            //Debug.Log(message.round +", "+roundTimer+)
            if (order.round == round && roundTimer >= order.time)
            {

                OrderElement orderElement = (OrderElement) Instantiate(orderElementPrefab, orderContainer.transform);
                orderElement.Initialize(order);
                orderElementActive.Add(orderElement);

                orders.Remove(order);

                break;
            }
        }

        float time = roundtimes[round] - roundTimer;
        if (time > 0f)
        {
            timer.text = Mathf.CeilToInt(roundtimes[round] - roundTimer).ToString();
        }
        else
        {
            timer.text = "0";
            NextRound();
        }
    }

    void NewMessage(string message, int id)
    {
        encryptionPanels[id].GenerateMessage(message);
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

    public void NextRound()
    {
        // Move round tracker
        // Inactivate all pages and buttons, except index 0 = unencrypted one.
        round += 1;
        roundTimer = 0;
        for (int i = 0; i < pageButtons.Length; i++)
        {
            pageButtons[i].SetActive(false);
        }
        for (int i = 0; i < orderElementActive.Count; i++)
        {
            Destroy(orderElementActive[i].gameObject);
        }

        SwitchMessage(0);
    }

}
