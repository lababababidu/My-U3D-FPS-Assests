using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public bool IsDynamic = false;

    public float Speed = 1f;
    public float StartTime = 0;
    public float DownSpeed = 1f;
    public bool Mirror = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private int state = 1;
    public bool special = false;

    private void Start(){
        startPosition = transform.position;
        if(!Mirror){
            endPosition  = transform.position + Vector3.right * 5;
        }else{
            endPosition  = transform.position + Vector3.left * 5;
        }
        state = 1;
    }
    private void Update(){
        if(IsDynamic){
            if(state == 1){
                this.transform.position = Vector3.MoveTowards (this.transform.position, endPosition, Speed * Time.deltaTime);
                
                if (this.transform.position == endPosition) {
                    state = 2;
                }
            }
            else if(state == 2){
                this.transform.position = Vector3.MoveTowards (this.transform.position, startPosition, Speed * Time.deltaTime);
                if (this.transform.position == startPosition) {
                    state = 1;
                }
            }
        }
    }

    private void Down(){
        // transform.position += Vector3.down*1;
        transform.eulerAngles = new Vector3(150, 0, 0);
    }
    private void StandUp(){
        // transform.position -= Vector3.down*1;
        transform.eulerAngles = new Vector3(90, 0, 0);
    }

    void OnCollisionEnter(Collision collision){
        GameObject other = collision.gameObject;
        // Debug.Log("Target Collided with " + other.layer);
        if(other.layer == 7){
            Debug.Log("Add score! ");
            if(IsDynamic){
                GameObject.Find("Score Controller").GetComponent<ScoreCounter>().HitDynamicTarget();
                Down();
                Invoke("StandUp", 5f);
            }
            else{
                GameObject.Find("Score Controller").GetComponent<ScoreCounter>().HitStaticTarget();
                if(special){
                    GameObject.Find("Score Controller").GetComponent<ScoreCounter>().HitDynamicTarget();
                    Debug.Log("SPC");
                    Animator Animator = GetComponent<Animator>();
                    if(Animator == null){
                        Debug.Log("AM null!!");
                    }
                    Animator.SetBool("Hit",true);
                    Debug.Log("AM not null set true!!");
                    Invoke("setHitfalse",1f);
                }
            }
        }
    }

    void setHitfalse(){
        Animator Animator = GetComponent<Animator>();
        Animator.SetBool("Hit",false);
    }

}
