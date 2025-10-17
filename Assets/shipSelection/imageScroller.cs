using UnityEngine;
using UnityEngine.UI;

public class imageScroller : MonoBehaviour
{
    public Image imageComponent;
    public Sprite[] image;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void changeImage(int spriteIndex)
    {
        imageComponent.sprite = image[spriteIndex];
    }
}
