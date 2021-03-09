using System;

namespace Interface
{
    public interface IViewModel
    {
        event Action OnAction;
        void Action();
    }
    
    public interface IViewModel<T>
    {
        event Action<T> OnAction;
        void Action(T obj);
    }
    
    public interface IViewModel<T, U>
    {
        event Action<T, U> OnAction;
        void Action(T obj1, U obj2);
    }
}