using System;

internal class ComponentArray<T> where T : IComponent
{
    public T[] Values;

    public ComponentArray()
    {
        Values = new T[1000];
    }

    public void Add(uint entityID, T component)
    {
        Values[entityID] = component;
    }

    public void Remove(uint entityID)
    {
        Values[entityID] = default;
    }

    public T Get(uint entityID)
    {
        return Values[entityID];
    }

    public void Clear()
    {
        Array.Clear(Values, 0, Values.Length);
    }

    public int Len()
    {
        return Values.Length;
    }

    public bool Contains(T component)
    {
        return Array.Exists(Values, c => c != null && c.Equals(component));
    }

    public bool ContainsKey(uint entityID)
    {
        if (entityID >= Values.Length)
        {
            return false;
        }
        return Values[entityID] != null;
    }

    public T this[uint entityID]
    {
        get { return Values[entityID]; }
        set { Values[entityID] = value; }
    }
}
