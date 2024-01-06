using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakProp : MonoBehaviour, Props.ChangableObject
{
    public int Hits = 1;

    public Sprite BrokenObject;
    public int Extras;
    public Sprite BrokenObject2;
    public Sprite BrokenObject3;
    public Sprite BrokenObject4;
    public Sprite BrokenObject5;

    public GameObject BrokenGlass;

    public Collider2D[] SwapColliders;

    public GameObject[] ObjectsToDestroy;

    public GameObject[] ObjectsToSummon;

    public UnityEngine.Experimental.Rendering.Universal.ShadowCaster2D Shadow;

    public bool PlaySound;
    public bool UseParticle = true;
    public bool DestroyGameobjects;
    public bool SummonGameobjects;
    public bool DestroyShadow;

    public void Change()
    {
        if (Hits > 1) {
            Hits--;
            return;
        }


        if (Extras == 0)
            GetComponent<SpriteRenderer>().sprite = BrokenObject;
        else
        {
            int Image = Random.Range(1, 1 + Extras);
            if (Image == 1)
                GetComponent<SpriteRenderer>().sprite = BrokenObject;
            else if (Image ==2)
                GetComponent<SpriteRenderer>().sprite = BrokenObject2;
            else if (Image == 3)
                GetComponent<SpriteRenderer>().sprite = BrokenObject3;
            else if (Image == 4)
                GetComponent<SpriteRenderer>().sprite = BrokenObject4;
            else if (Image == 5)
                GetComponent<SpriteRenderer>().sprite = BrokenObject5;
        }
        if (PlaySound)
            GetComponent<AudioSource>().Play();
        Destroy(GetComponent<BoxCollider2D>());
        gameObject.tag = "Untagged";
        foreach (Collider2D Collider in SwapColliders)
        {
            if (Collider.enabled)
                Collider.enabled = false;
            else
                Collider.enabled = true;
        }
        if (DestroyGameobjects)
        {
            foreach (GameObject Light in ObjectsToDestroy)
            {
                Destroy(Light);
            }
        }
        if (SummonGameobjects)
        {
            foreach (GameObject Object in ObjectsToSummon)
            {
                Instantiate(Object, transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
        if (DestroyShadow)
            Destroy(Shadow);
        if (UseParticle)
            Instantiate(BrokenGlass, transform);
        Destroy(this);
    }
}
