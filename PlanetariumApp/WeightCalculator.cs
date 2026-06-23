using System;

namespace PlanetariumApp
{
    public class WeightCalculator
    {
        public double CalculateWeight(double earthWeight, double gravityFactor)
        {
            if (earthWeight <= 0)
                throw new ArgumentException("Вага на Землі повинна бути більшою за нуль.");

            if (gravityFactor <= 0)
                throw new ArgumentException("Коефіцієнт гравітації повинен бути додатним.");

            double result = earthWeight * gravityFactor;
            return Math.Round(result, 2);
        }
    }
}