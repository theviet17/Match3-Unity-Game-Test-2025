using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Item, new()
{
    public Stack<T> pool = new Stack<T>();
    private SpriteCollection m_spriteCollection;
    private GameObject m_emptyPrefab;
    //Use Object Pooling to reuse Items and Item.Views instead of constantly creating new ones.
    public ObjectPool( int initialSize, SpriteCollection spriteCollection, GameObject emptyPrefab )
    {
        this.m_spriteCollection = spriteCollection;
        this.m_emptyPrefab = emptyPrefab;   
        
        for (int i = 0; i < initialSize; i++)
        {
            T item = new T();
            item.Init(m_spriteCollection, m_emptyPrefab);
            
            pool.Push(item);
        }
    }
    public T Get()
    {
        if (pool.Count > 0)
        { 
            Debug.Log("Pool non empty");
            return pool.Pop();
        }
        else
        {
            Debug.Log("Pool empty");
            T item = new T();
            item.Init(m_spriteCollection,m_emptyPrefab);
            return item;
        }
    }
    public void Release(T obj)
    {
        Debug.Log("Pool released");
        pool.Push(obj);
    }
}
