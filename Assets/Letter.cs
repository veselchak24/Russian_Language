using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IPointerDownHandler
{
    public bool isAccentLetter = false;
    private Text text;
    private OrthoepyManager manager;

    private void Start()
    {
        text = GetComponent<Text>();
        manager = FindObjectOfType<OrthoepyManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isAccentLetter)
        {
            text.color = Color.green;
            manager.Answer(true);
        }
        else
        {
            text.color = Color.red;
            manager.Answer(false);
        }

        //Disenabled script in a letters
        foreach (Letter child in transform.parent.GetComponentsInChildren<Letter>())
          if(child != this)
            child.enabled = false;
        enabled = false;
    }
}
