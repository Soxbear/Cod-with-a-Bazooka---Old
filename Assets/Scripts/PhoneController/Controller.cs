using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace PhoneController {
public class Controller : NetworkBehaviour
{
    public Vector2 Movement; 
    public Vector2 Shooting; 
    
    public int BazookaBadges;
    public int Health;
    
    public RectTransform MoveStickPos;
    public RectTransform ShootStickPos;

    public RectTransform Canvas;
    public RectTransform MoveStick;
    public RectTransform ShootStick;

    private float Scaler;

    void Start() {
        Scaler = 1f / Canvas.localScale.x;
    }

    int MoveNum; bool MovTouch = false;
    int ShootNum; bool ShoTouch = false;

    void Update()
    {
        bool MovEval = false;
        //if (!IsOwner) return;

        foreach (Touch Touch in Input.touches) {
            
            if (!MovTouch && Touch.phase == TouchPhase.Began) {
                Vector2 MoveStickVectorT = (Touch.position * (1/Scaler)) - new Vector2(MoveStickPos.position.x, MoveStickPos.position.y);
                Debug.Log(MoveStickVectorT.magnitude);
                if (MoveStickVectorT.magnitude < 400) {
                    MovTouch = true;
                    MoveNum = Touch.fingerId;
                }
            }

            if (MoveNum == Touch.fingerId) {
                if (Touch.phase == TouchPhase.Ended || Touch.phase == TouchPhase.Canceled) {
                    MoveStick.localPosition = new Vector3(0, 0, 0);
                    MovTouch = false;
                }
                else {
                    Vector2 MoveStickVector = (Touch.position * (1/Scaler)) - new Vector2(MoveStickPos.position.x, MoveStickPos.position.y);
                    Debug.Log(MoveStickVector.magnitude);
                    MoveStick.localPosition = Vector2.ClampMagnitude(MoveStickVector, 400);
                    MovEval = true;
                }
            }
            
            /*
            Vector2 MoveStickVector = (Touch.position * (1/Scaler)) - new Vector2(MoveStickPos.position.x, MoveStickPos.position.y);
            Debug.Log(MoveStickVector.magnitude);
            if (MoveStickVector.magnitude < 500) {
                MoveStick.localPosition = Vector2.ClampMagnitude(MoveStickVector, 400);
                MovEval = true;
            }
            */  
        }

        if (MovTouch) {
            //Input.GetTouch
        }

        if (!MovEval) {
            MoveStick.localPosition = new Vector2(0, 0);
        }
    }

    
}

}