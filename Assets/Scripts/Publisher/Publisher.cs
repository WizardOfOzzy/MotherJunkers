using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void PublisherEventDelegate<T>(T evt) where T : PublisherEvent;

public class Publisher
{
    public static void Subscribe<T>(PublisherEventDelegate<T> eventDelegate) where T : PublisherEvent
    {
        if (_publisherEventMappings.ContainsKey(typeof(T)))
        {
            PublisherEventMapper<T> mapper = (PublisherEventMapper<T>)(_publisherEventMappings[typeof(T)]);
            mapper.Event += eventDelegate;
        }
        else
        {
            Debug.LogWarning("Couldn't find PublisherEvent to subscribe to: " + typeof(T).ToString());
        }
    }

    public static void Unsubscribe<T>(PublisherEventDelegate<T> eventDelegate) where T : PublisherEvent
    {
        if (_publisherEventMappings.ContainsKey(typeof(T)))
        {
            PublisherEventMapper<T> mapper = (PublisherEventMapper<T>)(_publisherEventMappings[typeof(T)]);
            mapper.Event -= eventDelegate;
        }
        else
        {
            Debug.LogWarning("Couldn't find PublisherEvent to unsubscribe to: " + typeof(T).ToString());
        }
    }

    public static void Raise<T>(T evt) where T : PublisherEvent
    {
        if (_publisherEventMappings.ContainsKey(evt.GetType()))
        {
            PublisherEventMapper<T> mapper = (PublisherEventMapper<T>)(_publisherEventMappings[typeof(T)]);
            mapper.RaiseEvent(evt);
        }
        else
        {
            Debug.LogWarning("Couldn't find PublisherEvent to raise: " + evt.GetType().ToString());
        }
    }

    // TODO - set up PublisherChannels
    public static void Init()
    {
        // Init all publisher events we want to support.
        _publisherEventMappings = new Dictionary<System.Type, PublisherEventMapperBase>();

        //AddPublisherEvent<PaddleGroupDestroyedEvent>();
    }

    private static void AddPublisherEvent<T>() where T : PublisherEvent
    {
        _publisherEventMappings.Add(typeof(T), new PublisherEventMapper<T>());
    }

    private static Dictionary<System.Type, PublisherEventMapperBase> _publisherEventMappings;

    private abstract class PublisherEventMapperBase
    {
        public abstract System.Type Type { get; }
    }

    private class PublisherEventMapper<T> :
        PublisherEventMapperBase
        where T : PublisherEvent
    {
        public override System.Type Type { get { return typeof(T); } }

        public event PublisherEventDelegate<T> Event;

        public void RaiseEvent(T evt)
        {
            if (Event != null)
            {
                Event(evt);
            }
        }
    }


}