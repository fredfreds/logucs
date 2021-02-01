using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns_dotNET.Structural
{
    public interface IWorker
    {
        double Work(double machines);
        double Estimate(double machines);
    }

    public class WorkingCompany : IWorker
    {
        private IEnumerable<IWorker> workers;

        public WorkingCompany(IEnumerable<IWorker> workers)
        {
            this.workers = workers;
        }

        public double Estimate(double machines)
        {
            return machines / GetWorkTime();
        }

        public double Work(double machines)
        {
            double totalWork = GetWorkTime();

            double totalTime = workers.Select(worker => new
            {
                Worker = worker,
                Speed = 1 / (double)worker.Estimate(1)
            }).Select(record => new
            {
                Worker = record.Worker,
                Machines = machines * record.Speed / totalWork
            }).Select(record => record.Worker.Work(record.Machines)).Max();

            return totalTime;
        }

        private double GetWorkTime()
        {
            return workers.Select(worker => worker.Estimate(1))
                .Select(daysToComplete => 1 / (double)daysToComplete)
                .Sum();
        }
    }

    public class Worker : IWorker
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
            double totalWork = Estimate(totalMachines);

            Debug.Log($"{name} work for {totalMachines}, {daysToCompleteWork}");

            return totalWork;
        }

        public double Estimate(double machines)
        {
            return machines * daysToCompleteWork;
        }
    }

    public class Plant
    {
        private int totalMachines;
        private IWorker worker;

        public Plant(int totalMachines, IWorker worker)
        {
            this.totalMachines = totalMachines;
            this.worker = worker;
        }

        public void StartWork()
        {
            double time = worker.Work(totalMachines);

            Debug.Log($"All work = {totalMachines:0}, {time:0.00}");
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
            IWorker localWorkers = new WorkingCompany(new IWorker[] { 
                new Worker(7, "Fred1"),
                new Worker(5, "Fred2")
            });

            IWorker mainWorkers = new WorkingCompany(new IWorker[] {
                new Worker(4, "Fred3"),
                new Worker(5, "Fred4"),
                new Worker(3, "Fred5"),
                localWorkers
            });

            plant = new Plant(14, mainWorkers);

            plant.StartWork();
        }

        private void OnDisable()
        {
            runBtn.onClick.RemoveListener(Run);
        }
    }
}