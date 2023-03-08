using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public struct BaseStruct<T> where T : IComponent
{
    public uint _key;
    public T _component;

    BaseStruct(uint key, T component)
    {
        _key = key;
        _component = component;
    }
}


public struct ComponentContainer<T> where T : IComponent
{

    public List<BaseStruct<T>> components;
      


    public bool ContainsKey(uint key)
    {
        foreach (BaseStruct<T> component in components)
        {
            if (component._key == key)
            {
                return true;
            }
        }
        return false;
    }

    public T this[uint key]
    {
        get
        {
            foreach (BaseStruct<T> component in components)
            {
                if (component._key == key)
                {
                    return component._component;
                }
            }
            return components[0]._component;
        }

    }

    
}

    

