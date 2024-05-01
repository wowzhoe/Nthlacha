using System;
using System.Collections.Generic;

namespace CodeFirst
{
    /// <summary>
    /// Utility class for data binding.
    /// Sample of usage:
    /// Box<float> someFloatValue = new Box<float>(0f);
    /// someFloatValue.Value = 42f;
    /// someFloatValue.Bind(s=>Debug.Log(s));
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class Box<T>
    {
        private T storedValue;
        private List<Action<T>> listeners = new List<Action<T>>();

        public Box(T value)
        {
            storedValue = value;
        }

        public T Value
        {
            get { return storedValue; }
            set
            {
                try { if (storedValue.Equals(value)) return; } catch { }
                storedValue = value;
                Trigger();
            }
        }

        public void SetValueAndTrigger(T value)
        {
            storedValue = value;
            Trigger();
        }

        public void Bind(Action<T> listener)
        {
            LazyBind(listener);
            listener(Value);
        }

        public void LazyBind(Action<T> listener)
        {
            listeners.Add(listener);
        }

        public void Unbind(Action<T> listener)
        {
            listeners.Remove(listener);
        }

        public void Trigger()
        {
            listeners.Map(listener =>
            {
                try
                {
                    listener?.Invoke(Value);
                }
                catch
                {
                    Unbind(listener);
                }
            });
        }
    }
}
