using UnityEngine;
using UnityEngine.UI;

public class imageScroller : MonoBehaviour
{
    public Image ship;
    public Sprite[] ships;
    int currentIndex;

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
        ship.sprite = ships[spriteIndex];
    }
    public void selectMinus()
    {
        if (currentIndex <= 0)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex--;
            changeImage(currentIndex);
        }
        
    }
    public void selectPlus()
    {
        if (currentIndex >= ships.Length - 1)
        {
            currentIndex = ships.Length - 1;
        }
        else
        {
            currentIndex++;
            changeImage(currentIndex);
        }

    }
}
