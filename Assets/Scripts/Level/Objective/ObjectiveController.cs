using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objectives;

namespace Triggers {

    public class ObjectiveController : MonoBehaviour, TriggerEffect {
        EventHandler EventHandler;
        private int ObjectiveQueue;
        [HideInInspector]
        public bool IsObjectiveEffect{get; set;}
        public string ObjectiveTitle;
        public float Activate() {
            EventHandler = FindObjectOfType<EventHandler>();
            foreach (Objective Objective in GetComponents<Objective>()) {
                EventHandler.ObjectiveEvent += Objective.GiveEvent;
                ObjectiveQueue++;
            }
            return -1;
        }
        public void Completed() {
            ObjectiveQueue--;
            if (ObjectiveQueue == 0) {
                foreach (TriggerEffect Effect in GetComponents<TriggerEffect>()) {
                    if (Effect.IsObjectiveEffect)
                        Effect.Activate();
                }
                GetComponent<Trigger>().ReturnCall();
            }
        }

        void Start() {
            IsObjectiveEffect = false;
        }
    }

}

namespace Objectives {
    public interface Objective {
        public void GiveEvent(ObjectiveEvent EventType, GameObject AssociatedObject = null);
    }

    public enum ObjectiveEvent {
        Kill,
        Reach
    }
}