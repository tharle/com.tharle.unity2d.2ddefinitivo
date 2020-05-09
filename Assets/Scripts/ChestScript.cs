using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public bool open;
    void Start(){
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        
    }

    public void interacao(){
        this.open = !this.open;
        if(this.open){
            this.spriteRenderer.sprite = sprites[1];
        }else{
            this.spriteRenderer.sprite = sprites[0];
        }
    }
}
