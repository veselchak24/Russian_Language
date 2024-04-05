using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DocsMenu : MonoBehaviour
{
    [SerializeField] private Text docsText;
    [SerializeField] private Image background;
    [SerializeField] private ParonymsManager paronyms;

    private List<string> docs;

    private void OnEnable()
    {
        docs = paronyms.GetCurrentWordsDocs();

        for (int i = 0; i < docs.Count; i++)
            if (docs[i].Contains('1') && docs[i].IndexOf('1') != 0) // Плохое решение(Добавил index != 0)
            {
                List<string> docsWord = new List<string>();

                for (int index = 1; ; index++)
                    if (!docs[i].Contains(index.ToString()))
                        break;
                    else
                    {
                        string doc;
                        int currentIndex = docs[i].IndexOf(index.ToString());
                        if (docs[i].Contains((index + 1).ToString()))
                            doc = docs[i].Substring(currentIndex, docs[i].IndexOf((index + 1).ToString()) - currentIndex);
                        else
                            doc = docs[i].Substring(currentIndex);
                        docsWord.Add(doc);
                    }

                for (int index = 0; index < docsWord.Count; index++)
                    if (i + 1 + index >= docs.Count)
                        docs.Add(docsWord[index]);
                    else
                        docs.Insert(i + 1 + index, docsWord[index]);


                docs[i] = docs[i].Substring(0, docs[i].IndexOf('-') + 1);
            }

        docsText.text = "";
        foreach (string st in docs)
            docsText.text += st + '\n';
    }
}
