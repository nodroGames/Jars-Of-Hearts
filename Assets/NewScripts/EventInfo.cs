﻿using System;
using UnityEngine;

namespace Assets.NewScripts
{
    public abstract class EventInfo<T> where T : EventInfo<T>
    { 
        /*
         * The base Event,
         * might have some generic text
         * for doing Debug.Log?
         */
        public string Description;

    private bool hasFired;
    public delegate void EventListener(T info);
    private static event EventListener listeners;

    public static void RegisterListener(EventListener listener)
    {
        listeners += listener;
    }

    public static void UnregisterListener(EventListener listener)
    {
        listeners -= listener;
    }

    public void FireEvent()
    {
        if (hasFired)
        {
            throw new Exception("This event has already fired, to prevent infinite loops you can't refire an event");
        }
        hasFired = true;
        if (listeners != null)
        {
            listeners(this as T);
        }
    }
}

    public class DebugEvent : EventInfo<DebugEvent>
    {
        public int VerbosityLevel;
    }

    public class CustomerDeathEvent : EventInfo<CustomerDeathEvent>
    {
        public GameObject DeadCustomerGO;

        /*

        Info about cause of death, our killer, etc...

        Could be a struct, readonly, etc...

        */

    }
}
