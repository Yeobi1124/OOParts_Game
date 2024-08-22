using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CombatEventType { Win, Lose };

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get{ return instance; } set {} }
    private static EventManager instance;
    public delegate void OnEvent(CombatEventType type, Component sender, object param = null);

    private Dictionary<CombatEventType, List<OnEvent>> Listeners;

    private void Awake() {
        if(instance == null)
            instance = this;
        else{
            DestroyImmediate(this);
        }

        Listeners = new Dictionary<CombatEventType, List<OnEvent>>();
    }

    public void AddEventListner(CombatEventType Event_Type, OnEvent Listener){
        List<OnEvent> ListenList = null;

        if(Listeners.TryGetValue(Event_Type, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }

        ListenList = new List<OnEvent>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);

        Debug.Log(Listeners[Event_Type][0]);
    }

    public void PostNotification(CombatEventType Event_Type, Component Sender, object Param = null){
        Debug.Log("PostNotification Act");
        List<OnEvent> ListenList = null;

        if(!Listeners.TryGetValue(Event_Type, out ListenList))
            return;
        
        ListenList.ForEach(e => {
            if(!e.Equals(null)){
                e(Event_Type, Sender, Param);
            }
        });
    }
}
