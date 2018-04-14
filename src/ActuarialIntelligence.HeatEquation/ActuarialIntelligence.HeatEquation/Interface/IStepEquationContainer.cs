using System.Collections.Generic;

namespace ActuarialIntelligence.HeatEquation.Interface
{
    public interface IStepEquationContainer
    {
        IList<IList<double>> solution { get; }
        void Solve(int MaxStep, int diamondSteps);
    }
}
