using UnityEngine;
using UnityEngine.SceneManagement;
public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;
    // public GameObject imageToShow;

    // public void RevealImage()
    // {
    //     imageToShow.SetActive(true);
    // }

    // public void HideImage()
    // {
    //     imageToShow.SetActive(false);
    // }


    void Awake()
    {
        // HideImage();
        text = GetComponent<TMPro.TMP_Text>();
    }

    void Start() => UpdateCount();

    void OnEnable() => Collectible.OnCollected += OnCollectibleCollected;
    void OnDisable() => Collectible.OnCollected -= OnCollectibleCollected;

    void OnCollectibleCollected()
    {
        // text.text = (++count).ToString();
        count++;
        UpdateCount();
        if (count == Collectible.total)
        {
            // RevealImage();


            // ------ PUT WIN SCRIPT HERE ------
        }
    }

    
    void UpdateCount()
    {
        text.text = $"{count} / {Collectible.total}";
    }
}
