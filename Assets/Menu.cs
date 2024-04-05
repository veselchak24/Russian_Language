using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas Orthoepy;
    [SerializeField] private Canvas Paronyms;

    public void OrthoepyButton()
    {
        Orthoepy.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ParonymsButton()
    {
        Paronyms.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}