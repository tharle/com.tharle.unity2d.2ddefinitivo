using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private _GameController gameController;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public bool open;
    void Start(){
        this.gameController = FindObjectOfType(typeof(_GameController)) as _GameController;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        
    }

    public void interacao(){
        this.open = !this.open;
        if(this.open){
            this.spriteRenderer.sprite = sprites[1];
            this.gameController.teste++;
        }else{
            this.spriteRenderer.sprite = sprites[0];
        }
    }
}
