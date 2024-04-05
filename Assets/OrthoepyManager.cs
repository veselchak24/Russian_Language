using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OrthoepyManager : MonoBehaviour
{
    [Header("Letters")]
    [SerializeField] private Letter LetterPrefab;
    [SerializeField] private Transform LetterParent;

    [SerializeField] private UnityEngine.UI.Text PartOfSpeech;
    [SerializeField] private UnityEngine.UI.Text AllText;

    [Header("Buttons")]
    [SerializeField] GameObject nextButton;

    private Rect letterRect;

    private List<string> ListWords = new List<string>();

    private List<string> LoseWords = new List<string>();

    //Words
    private string currentWord, currentAllLine;

    public void UpdateWord()
    {
        foreach (Transform child in LetterParent.transform.GetComponentInChildren<Transform>())
            Destroy(child.gameObject);

        AllText.gameObject.SetActive(false);
        PartOfSpeech.gameObject.SetActive(false);
        nextButton.SetActive(false);

        int index;

        do
        {
            index = Random.Range(0, ListWords.Count);
            currentAllLine = ListWords[index];
        }
        while (currentAllLine[0] == '!');

        PartOfSpeech.text = "Часть речи:" + ListWords.GetRange(0, index).FindLast(match => match[0] == '!').Substring(1);

        int currentIndex = currentAllLine.IndexOf(';');

        int temp = currentAllLine.IndexOf(',');
        currentIndex = (temp > 0) ? ((temp < currentIndex || currentIndex < 0) ? temp : currentIndex) : currentIndex;

        temp = currentAllLine.IndexOf(' ');
        currentIndex = (temp > 0) ? ((temp < currentIndex || currentIndex < 0) ? temp : currentIndex) : currentIndex;


        currentWord = currentAllLine.Substring(0, currentIndex > 0 ? currentIndex : currentAllLine.Length);

        for (var i = 0; i < currentWord.Length; i++)
        {
            Letter letter = Instantiate(LetterPrefab);

            //Position
            {
                letter.transform.parent = LetterParent;

                letter.gameObject.SetActive(true);

                letter.transform.position = LetterPrefab.transform.position - new Vector3(currentWord.Length / 2 - i, 0, 0) * letterRect.width * 1.5f;
            }

            //Text
            {
                letter.GetComponent<UnityEngine.UI.Text>().text = currentWord[i].ToString().ToLower();

                if (char.IsUpper(currentWord[i]))
                    letter.isAccentLetter = true;
            }
        }
    }

    public void Answer(bool isWin)
    {
        AllText.text = currentAllLine;
        AllText.gameObject.SetActive(true);

        PartOfSpeech.gameObject.SetActive(true);

        nextButton.SetActive(true);
    }


    void Start()
    {
        TextAsset file = Resources.Load("Orthoepy") as TextAsset;

        string[] text = file.text.Split('\n');

        try
        {
            foreach (string word in text)
                ListWords.Add(word);
        }
        catch (System.Exception e)
        {
            var s = File.CreateText(Application.persistentDataPath + "/Debbug.txt");
            s.WriteLine("Exception for Load Orthoepy file:");
            s.Write(e.Message);
            s.Close();
        }

        if (ListWords.Count == 0)
            throw new System.Exception("List Words is Empty!");

        letterRect = LetterPrefab.GetComponent<RectTransform>().rect;

        UpdateWord();
    }
}
