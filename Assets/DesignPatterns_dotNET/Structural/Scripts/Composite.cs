using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns_dotNET.Structural
{
    public class Worker
    {
        private string name;
        private int daysToCompleteWork;

        public Worker(int daysToCompleteWork, string name)
        {
            this.daysToCompleteWork = daysToCompleteWork;
            this.name = name;
        }

        public double Work(double totalMachines)
        {
            double totalWork = totalMachines / daysToCompleteWork;
            Debug.Log($"{name} work for {totalMachines}, {daysToCompleteWork}");

            return totalWork;
        }

        public int Estimate(int machines)
        {
            return machines * daysToCompleteWork;
        }
    }

    public class Plant
    {
        private int totalMachines;
        private IEnumerable<Worker> workers;

        public Plant(int totalMachines, IEnumerable<Worker> workers)
        {
            this.totalMachines = totalMachines;
            this.workers = new List<Worker>(workers);
        }

        public void StartWork()
        {
            double totalWork = workers.Select(worker => worker.Estimate(1))
                .Select(daysToComplete => 1 / (double)daysToComplete)
                .Sum();

            double totalTime = totalMachines / totalWork;

            double total = workers.Select(worker => worker.Work(totalTime)).Sum();

            Debug.Log($"All work = {total}");
        }
    }

    public class Composite : MonoBehaviour
    {
        [SerializeField] private Button runBtn;
        [SerializeField] private InputField infoIF;

        Plant plant;

        private void OnEnable()
        {
            runBtn.onClick.AddListener(Run);
        }

        private void Run()
        {
            plant = new Plant(14, new Worker[]
            {
                new Worker(4, "Fred1"),
                new Worker(5, "Fred2"),
                new Worker(3, "Fred3")
            });

            plant.StartWork();
        }

        private void OnDisable()
        {
            runBtn.onClick.RemoveListener(Run);
        }
    }
}