using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns_dotNET.Behavioral
{
    public interface IUnit
    {
        void Add(IObserver observer);
        void Remove(IObserver observer);
        void Notify(string info);
        string Name { get; }
    }

    public interface IObserver
    {
        void Update(IUnit unit, string info);
    }

    public class Unit : IUnit
    {
        public string Name { get; private set; }

        private List<IObserver> observers = new List<IObserver>();

        public void Add(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Remove(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string info)
        {
            foreach (var observer in observers)
            {
                observer.Update(this, info);
            }
        }

        public void Do(string n, string info)
        {
            Name = n;
            Debug.Log($"Do {info}");
            Notify(info);
        }
    }

    public class UI : IObserver
    {
        public void Update(IUnit unit, string info)
        {
            Debug.Log($"UI Update {unit.Name} with {info}");
        }
    }

    public class Log : IObserver
    {
        public void Update(IUnit unit, string info)
        {
            Debug.Log($"Log Update {unit.Name} with {info}");
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
            unit.Add(ui);
            unit.Add(log);
            unit.Do("Unit 1", infoIF.text);
            unit.Remove(ui);
            unit.Remove(log);
        }

        private void OnDisable()
        {
            runBtn.onClick.RemoveListener(Run);
        }
    }
}