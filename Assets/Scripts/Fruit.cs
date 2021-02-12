using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private const float GRAVITY = 2.0f;

    public bool IsActive {get;set;}

    private float VerticalVelocity;
    private float speed;

    private bool isSliced = false;

    public void LaunchFruit(float VerticalVelocity,float speedX, float startX){
        IsActive = true;
        speed = speedX;
        this.VerticalVelocity = VerticalVelocity;
        transform.position = new Vector3(startX,0,0);
        isSliced = false;
    }    

    private void Update (){
        if (!IsActive)
            return;
        
        VerticalVelocity -= GRAVITY * Time.deltaTime;
        transform.position += new Vector3(speed,VerticalVelocity,0) * Time.deltaTime;

        if (transform.position.y < -1){
         {
            IsActive = false;
            if(!isSliced)
               GameManage.Instance.LoseLP();
         }   
        }
    }

    public void Slice(){
        if (isSliced)
            return;
        if (VerticalVelocity <0.5f)
            VerticalVelocity = 0.5f;

        speed = speed * 0.5f;
        isSliced = true;
    }



}
