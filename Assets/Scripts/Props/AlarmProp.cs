using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmProp : Interactables.Toggleable
{
    public bool On {
        get { return Power; }
        set {
            Power = value;

            if ( value == false ) {
                Renderer.sprite = OffSprite;
                Lit = false;
                Light.intensity = 0f;
                TimeLit = 1f;
                Lit = false;
            }
        }
    }
    [SerializeField]
    bool Power;
    public Sprite OnSprite;
    public Sprite OffSprite;

    bool Lit;

    float TimeLit;
    float LightIntensity;

    SpriteRenderer Renderer;
    UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    AudioSource Audio;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        LightIntensity = Light.intensity;
        Audio = GetComponent<AudioSource>();

        if (!On) {
            Renderer.sprite = OffSprite;
            Lit = false;
            Light.intensity = 0f;
            TimeLit = 1f;
            Lit = false;
        }
    }

    void FixedUpdate()
    {
        if (!Power)
            return;

        if (TimeLit > 0.8f)
        {
            if (Lit)
            {
                Renderer.sprite = OffSprite;
                Lit = false;
                Light.intensity = 0f;
            }
            else
            {
                Renderer.sprite = OnSprite;
                Lit = true;
                Light.intensity = LightIntensity;
                Audio.Play();
            }

            TimeLit = 0;
        }

        TimeLit += Time.deltaTime;
    }

    public override void Trigger() {
        On = !On;
    }

    public override float TriggerTime() {
        return 0;
    }
}
