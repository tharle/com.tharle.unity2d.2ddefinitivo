using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoInimigoControle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        switch(collider.tag){
            case "tagArma":
                print("Tomei DANOOOOOOOOOO!");
            break;
            default:
            break;
        }
    }
}
