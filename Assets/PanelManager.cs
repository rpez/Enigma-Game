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
    private bool resetBool;
    //private bool roundOnGoing = true;

    // Start is called before the first frame update
    void Start()
    {
        cipher = CreateCipher();
        roundTimer = 0f;
        messages = new List<Message>(messagesArray);
        orders = new List<Order>(orderArray);

        string str =
            "["
            + new string('=', GameManager.Instance.currentWarStatus)
            + new string('-', 20 - GameManager.Instance.currentWarStatus)
            + "]";

        str = str.Insert(6, "|");
        str = str.Insert(12, "|");
        str = str.Insert(18, "|");

        statusBar.text = str;
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
                        NewMessage("WEEK " + round + ":\n" + GameManager.Instance.currentSummary + "\n\n" + message.message, message.id);
                    } else
                    {
                        NewMessage("WEEK " + round + ":\n" + message.message, message.id);
                    }
                    
                }
                messages.Remove(message);


                break;
            }
        }


        int ordersLeftThisRound = 0;
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

            if (order.round == round) ordersLeftThisRound++;

        }

        foreach (OrderElement orderElement in orderElementActive)
        {
            if (orderElement != null) ordersLeftThisRound++;
        }

        if (ordersLeftThisRound == 0) NextRound();

        float time = roundtimes[round - 1] - roundTimer;
        if (time > 0f)
        {
            timer.text = Mathf.CeilToInt(roundtimes[round - 1] - roundTimer).ToString();
        }
        else
        {
            timer.text = "0";
            NextRound();
        }


        if (resetBool)
        {
            Debug.Log("Summary reseted");
            GameManager.Instance.ResetSummary();
        }
        resetBool = false;

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

        if (round > 5)
        {
            if (GameManager.Instance.currentWarStatus >= 10) round = 8; // Bad ending
            else round = 7; // Good ending
        } else if (round == 5)
        {
            if (GameManager.Instance.currentWarStatus >= 15) round = 6; // Bad ending
            else round = 5; //Good ending
        }

        roundTimer = 0;
        for (int i = 0; i < pageButtons.Length; i++)
        {
            pageButtons[i].SetActive(false);
        }

        int destroyedCount = 0;
        for (int i = 0; i < orderElementActive.Count; i++)
        {
            if (orderElementActive[i] != null)
            {
                Destroy(orderElementActive[i].gameObject);
                destroyedCount++;
            }
        }
        if (destroyedCount > 0)
        {
            // Negative points for unanswered questions
            GameManager.Instance.UpdateGameStatus(-destroyedCount, "There was chaos among the units, because they did not some orders.");
        }

        string str =
            "["
            + new string('=', GameManager.Instance.currentWarStatus)
            + new string('-', 20 - GameManager.Instance.currentWarStatus)
            + "]";

        str = str.Insert(6, "|");
        str = str.Insert(12, "|");
        str = str.Insert(18, "|");

        statusBar.text = str;

        SwitchMessage(0);

        resetBool = true;

    }

}
