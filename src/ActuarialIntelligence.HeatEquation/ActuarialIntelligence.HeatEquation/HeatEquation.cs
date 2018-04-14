using ActuarialIntelligence.HeatEquation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActuarialIntelligence.HeatEquation
{
    public class HeatEquation : IStepEquationContainer
    {// i = X ; j = T
        private double DT, DX;
        private double K;
        private IList<double> U_0, X;
        public IList<IList<double>> solution { get; private set; }
        public HeatEquation(double DT, double DX, double K)
        {
            this.DT = DT;
            this.DX = DX;
            this.K = K;
            U_0 = new List<double>();
            X = new List<double>();
            solution = new List<IList<double>>();
        }

        public void Solve(int MaxStep, int diamondSteps)
        {
            AssignBoundaryValues(MaxStep);
            double u1;
            string values = "";
            for (int j = 0; j < diamondSteps; j++)
            {
                var previousSetOfSolutions = solution.Last();
                int count = previousSetOfSolutions.Count;
                if (count != 1 && count >= 3)
                {
                    var nextSetOfSolutions = new List<double>();
                    for (int i = 1; i < count - 1; i++)
                    {
                        u1 = previousSetOfSolutions[i] + (K * (DT / Math.Pow(DX, 2))) *
                            (previousSetOfSolutions[i + 1] - 2 * previousSetOfSolutions[i] + previousSetOfSolutions[i - 1]);
                        nextSetOfSolutions.Add(u1);
                        values += u1 + " ";
                    }
                    solution.Add(nextSetOfSolutions);
                    values += "\n";
                    Console.WriteLine(values);
                    values = "";
                }
            }
        }

        private void AssignBoundaryValues(int MaxStep)
        {

            for (int i = 0; i < MaxStep; i++)
            {
                X.Add(i * DX);
                U_0.Add(Math.Sin(Math.PI * X[i]) / Math.PI * X[i]);
            }
            solution.Add(U_0);
        }
    }
}
