using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns_dotNET.Behavioral
{
    public class Unit
    {
        public event EventHandler<string> doSomething;
        public event EventHandler<Tuple<string, string>> doAfterSomething;

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
                doSomething(this, info);
            }
        }

        private void OnDoAfterSomething(string info, string n)
        {
            if (doAfterSomething != null)
            {
                doAfterSomething(this, Tuple.Create(info, n));
            }
        }
    }

    public class UI
    {
        public void AfterDo(object sender, string info)
        {
            Debug.Log($"UI Update {sender.ToString()} with {info}");
        }
    }

    public class Log 
    {
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
            unit.doSomething += ui.AfterDo;
            unit.doSomething += log.AfterDo;
            unit.doAfterSomething += log.AfterDoMore;
            unit.Do("Unit 1", infoIF.text);
        }

        private void OnDisable()
        {
            runBtn.onClick.RemoveListener(Run);
        }
    }
}