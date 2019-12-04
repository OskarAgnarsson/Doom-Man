using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RedButton : MonoBehaviour
{
    public List<SpriteRenderer> renderList;
    public Tilemap floor;
    public Tilemap walls;
    public Animator generatorAnim;

    public bool button = false;

    //Þegar player snertir takkann kviknar á ljósunum
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            if (!button) {
                button = true;
                for (int i = 0; i < renderList.Count; i++) {
                    if (renderList[i]) {
                        renderList[i].color = Color.white;
                    }
                }
                floor.color = Color.white;
                walls.color = Color.white;
                generatorAnim.SetTrigger("On");
            }
        }
    }
}
