using System;
using System.Collections.Generic;

public class PoolBase<T>
{
    private Func<T> _preloadFunc;
    private Func<T> _addFunc;
    private Action<T> _getAction;
    private Action<T> _returnAction;

    private Queue<T> _pool = new();
    private List<T> _activeElements = new();

    public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, List<T> elements, Func<T> addFunc = null)
    {
        _preloadFunc = preloadFunc;
        _addFunc = addFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        foreach (T element in elements)
        {
            Return(element);
        }
    }

    public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount, Func<T> addFunc = null)
    {
        _preloadFunc = preloadFunc;
        _addFunc = addFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        for (int i = 0; i < preloadCount; i++)
        {
            Return(preloadFunc());
        }
    }

    public T GetElement()
    {
        T element = _pool.Count > 0 ? _pool.Dequeue() : (_addFunc == null ? _preloadFunc() : _addFunc());
        _getAction(element);
        _activeElements.Add(element);
        return element;
    }

    public void Return(T element)
    {
        _returnAction(element);
        _pool.Enqueue(element);
        _activeElements.Remove(element);
    }

    public void ReturnAll()
    {
        foreach (T element in _activeElements.ToArray())
        {
            Return(element);
        }
    }

    public void AttachLinkAll(Action<T, PoolBase<T>> func)
    {
        foreach (T element in _pool)
        {
            func(element, this);
        }
    }
}