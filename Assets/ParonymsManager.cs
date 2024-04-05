using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParonymsManager : MonoBehaviour
{
    private class ArrWords
    {
        private List<Word> arrWords;

        public ArrWords() => arrWords = new List<Word>();
        public void Add(List<string> keys, List<string> dicts) => arrWords.Add(new Word(keys, dicts));
        public void RemoveAt(int index) => arrWords.RemoveAt(index);
        public int Count => arrWords.Count;
        public string RandomWord()
        {
            Word word = arrWords[Random.Range(0, arrWords.Count)];
            return word.keys[Random.Range(0, word.keys.Count)];
        }


        public Word this[string key]
        {
            get
            {
                foreach (Word word in arrWords)
                    if (word.keys.Contains(key))
                        return word;

                return null;
            }
            set
            {
                for (int i = 0; i < arrWords.Count; i++)
                    if (arrWords[i].keys.Contains(key))
                    {
                        arrWords[i] = value;
                        return;
                    }
                throw new System.IndexOutOfRangeException("object ArrWords throw the exception set the data");
            }
        }
        public class Word
        {
            public List<string> keys;
            public List<string> dicts;

            public Word(List<string> keys, List<string> dicts)
            {
                this.keys = keys;
                this.dicts = dicts;
            }


        }
    }

    [Header("Texts")]
    [SerializeField] private Text currentWordText;
    [SerializeField] private Text rightWordsText;

    [SerializeField] private TMPro.TMP_InputField answerField;
    [SerializeField] private Text userAnswer;

    [Header("Buttons")]
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject answerButton;
    [SerializeField] GameObject docsButton;

    private ArrWords words = new ArrWords();

    private string currentWord;

    public void UpdateWord()
    {
        userAnswer.gameObject.SetActive(false);
        rightWordsText.gameObject.SetActive(false);
        docsButton.gameObject.SetActive(false);
        answerField.gameObject.SetActive(true);

        nextButton.SetActive(false);
        answerButton.SetActive(true);

        answerField.text = "Ответ";

        currentWord = words.RandomWord();
        currentWordText.text = currentWord;
    }

    public void ClickButtonAnswer()
    {
        userAnswer.gameObject.SetActive(true);
        rightWordsText.gameObject.SetActive(true);

        string userAnswerText = answerField.text.ToUpper();
        while (userAnswerText.Contains(' ')) userAnswerText = userAnswerText.Remove(userAnswerText.IndexOf(' '), 1);

        userAnswer.text = userAnswerText;

        List<string> rightWords = words[currentWord].keys;

        rightWordsText.text = "";
        foreach (string word in rightWords)
            rightWordsText.text += word + " - ";
        rightWordsText.text = rightWordsText.text.Substring(0, rightWordsText.text.Length - 2);

        if (userAnswerText != currentWord && rightWords.Contains(userAnswerText))
            userAnswer.color = Color.green;
        else
            userAnswer.color = Color.red;

        answerField.gameObject.SetActive(false);
        answerButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        docsButton.gameObject.SetActive(true);
    }

    public List<string> GetCurrentWordsDocs() => words[currentWord].dicts;

    void Start()
    {
        string[] text = (Resources.Load("Paronyms") as TextAsset).text.Split('\n');
        int it = 0;
        while (it < text.Length)
        {
            if (text[it].Length == 0) { it++; continue; }
            while (text[it][0] != '!') { it++; if (it >= text.Length) break; }
            if (it >= text.Length) break;

            List<string> keys = new List<string>();
            List<string> dicts = new List<string>();

            while (text[it].Contains(" "))
                text[it] = text[it].Remove(text[it].IndexOf(' '), 1);
            while (text[it].Contains((char)13))
                text[it] = text[it].Remove(text[it].IndexOf((char)13), 1);

            foreach (string st in text[it].Substring(1).Split('-')) keys.Add(st.ToUpper());
            it++;

            for (int i = 0; i < keys.Count; i++)
            {
                dicts.Add(text[it]);
                it++;
            }

            words.Add(keys, dicts);
        }


        UpdateWord();
    }
}