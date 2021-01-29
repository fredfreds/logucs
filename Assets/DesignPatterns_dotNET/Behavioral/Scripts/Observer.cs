using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns_dotNET.Behavioral
{
    public interface IObserver<T>
    {
        void Update(object unit, T info);
    }

    public class Notification<T> : IObserver<T>
    {
        private Action<object, T> action;

        public Notification(Action<object, T> action)
        {
            this.action = action;
        }

        public void Update(object sender, T info)
        {
            action(sender, info);
        }
    }

    public class CastNotifier<T>
    {
        private List<IObserver<T>> observersList;

        private CastNotifier(IObserver<T> observer)
        {
            observersList = new List<IObserver<T>>();
            observersList.Add(observer);
        }

        private CastNotifier(CastNotifier<T> notifier, IObserver<T> observer)
        {
            observersList = new List<IObserver<T>>(notifier.observersList);
            observersList.Add(observer);
        }

        public void Notify(object sender, T data)
        {
            foreach (IObserver<T> observer in observersList)
            {
                observer.Update(sender, data);
            }
        }

        public static CastNotifier<T> operator + (CastNotifier<T> notifier, IObserver<T> observer)
        {
            if(notifier == null)
            {
                return new CastNotifier<T>(observer);
            }

            return new CastNotifier<T>(notifier, observer);
        }
    }

    public class Unit
    {
        public CastNotifier<string> doSomething;
        public CastNotifier<Tuple<string, string>> doAfterSomething;

        public string Name { get; private set; }

        public void Do(string n, string info)
        {
            Name = n;
            Debug.Log($"Do {info}");
            OnDoSomething(info);
        }

        public void DoAfter(string n, string info)
        {
            Name += info;
            Debug.Log($"Do After {info}");
            OnDoAfterSomething(info, Name);
        }

        private void OnDoSomething(string info)
        {
            if(doSomething != null)
            {
                doSomething.Notify(this, info);
            }
        }

        private void OnDoAfterSomething(string info, string n)
        {
            if (doAfterSomething != null)
            {
                doAfterSomething.Notify(this, Tuple.Create(info, n));
            }
        }
    }

    public class UI
    {
        public readonly IObserver<string> AfterDoSmth;

        public UI()
        {
            AfterDoSmth = new Notification<string>(
                (sender, data) => AfterDo(sender, data));
        }

        public void AfterDo(object sender, string info)
        {
            Debug.Log($"UI Update {sender.ToString()} with {info}");
        }
    }

    public class Log 
    {
        public readonly IObserver<string> AfterDoSmth;
        public readonly IObserver<Tuple<string, string>> AfterDoSmthMore;

        public Log()
        {
            AfterDoSmth = new Notification<string>((sender, data) => AfterDo(sender, data));
            AfterDoSmthMore = new Notification<Tuple<string, string>>((sender, data) =>
            AfterDoMore(sender, data));
        }

        public void AfterDo(object sender, string info)
        {
            Debug.Log($"Log Update {sender.ToString()} with {info}");
        }

        public void AfterDoMore(object sender, Tuple<string, string> info)
        {
            Debug.Log($"Log Update {sender.ToString()} with {info.Item1}, {info.Item2}");
        }
    }

    public class Observer : MonoBehaviour
    {
        [SerializeField] private Button runBtn;
        [SerializeField] private InputField infoIF;

        Log log = new Log();
        UI ui = new UI();
        Unit unit = new Unit();

        private void OnEnable()
        {
            runBtn.onClick.AddListener(Run);
        }

        private void Run()
        {
            unit.doSomething += ui.AfterDoSmth;
            unit.doSomething += log.AfterDoSmth;
            unit.doAfterSomething += log.AfterDoSmthMore;
            unit.Do("Unit 1", infoIF.text);
        }

        private void OnDisable()
        {
            runBtn.onClick.RemoveListener(Run);
        }
    }
}