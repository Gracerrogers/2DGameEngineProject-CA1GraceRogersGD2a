using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text Score;
    [SerializeField] private Image[] images;

    public void UpdateScore(int score) //update score method
    {
        Score.text = score.ToString();
    }

    public void updateLives(int lives)
    {
        switch (lives)
        {
            case 0:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                images[3].enabled = false;
                images[4].enabled = false;
                break;

            case 1:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                images[3].enabled = false;
                images[4].enabled = true;
                break;

            case 2:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                images[3].enabled = true;
                images[4].enabled = true;
                break;

            case 3:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = true;
                images[3].enabled = true;
                images[4].enabled = true;
                break;

            case 4:
                images[0].enabled = false;
                images[1].enabled = true;
                images[2].enabled = true;
                images[3].enabled = true;
                images[4].enabled = true;
                break;

            case 5: //because I added in 5 hearts, I had to add in another case to allow all of the hearts to dissappear
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = true;
                images[3].enabled = true;
                images[4].enabled = true;
                break;

        }
    }
}