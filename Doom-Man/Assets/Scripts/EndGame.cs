using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public bool finished = false;
    public bool dead = false;
    public Image image;

    private float targetAlpha = 1f;
    private bool colorSet = false;

    void Update() {
        if (finished && !dead) {
            Color curColor = image.color;
            float alphaDiff = Mathf.Abs(curColor.a-targetAlpha);

            if (alphaDiff > 0.0001) {
                curColor.a = Mathf.Lerp(curColor.a,targetAlpha,5*Time.deltaTime);
                image.color = curColor;
            }

            if (alphaDiff <= 0.0001) {
                SceneManager.LoadScene(0);
            }
        }
        else if (dead) {
            if (!colorSet) {
                image.color = new Color (0,0,0,0);
                colorSet = true;
            }
            Color curColor = image.color;
            float alphaDiff = Mathf.Abs(curColor.a-targetAlpha);

            if (alphaDiff > 0.0001) {
                curColor.a = Mathf.Lerp(curColor.a,targetAlpha,5*Time.deltaTime);
                image.color = curColor;
            }

            if (alphaDiff <= 0.0001) {
                SceneManager.LoadScene(0);
            }
        }
    }
}
