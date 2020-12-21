using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue<E, K, D>
{
    private Queue<E> queue = new Queue<E>();
    private Dictionary<K, Handler> callbacks = new Dictionary<K, Handler>();

    public delegate void Handler(D data);
    public delegate K KeyParser(E eventData);
    public delegate D DataParser(E eventData);

    private KeyParser Event2Key;
    private DataParser Event2Data;

    public EventQueue(KeyParser Event2Key, DataParser Event2Data)
    {
        this.Event2Data = Event2Data;
        this.Event2Key = Event2Key;
    }

    public void Enqueue(E eventData)
    {
        queue.Enqueue(eventData);
    }

    public void RegisterCallback(K key, Handler callback)
    {
        callbacks.Add(key, callback);
    }

    public void ProcessAll()
    {
        while (queue.Count > 0)
        {
            E eventData = queue.Dequeue();
            K key = Event2Key(eventData);

            if (!callbacks.TryGetValue(key, out Handler callback))
                throw new KeyNotFoundException("No key " + key + " exists.");

            callback(Event2Data(eventData));
        }
    }
}
