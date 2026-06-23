using Xunit;
using System;
using PlanetariumApp; // Підключаємо наш основний додаток

namespace PlanetariumApp.Tests
{
    public class WeightCalculatorTests
    {
        // Тест 1: Перевірка правильного розрахунку ваги на Місяці (гравітація 0.16)
        [Fact]
        public void CalculateWeight_EarthWeight80OnMoon_ReturnsCorrectWeight()
        {
            // Arrange (Налаштування умов)
            var calculator = new WeightCalculator();
            double earthWeight = 80;
            double moonGravity = 0.16;
            double expected = 12.8; // 80 * 0.16 = 12.8

            // Act (Виконання дії)
            double actual = calculator.CalculateWeight(earthWeight, moonGravity);

            // Assert (Перевірка результату)
            Assert.Equal(expected, actual);
        }

        // Тест 2: Перевірка розрахунку ваги на гігантському Юпітері (гравітація 2.34)
        [Fact]
        public void CalculateWeight_EarthWeight70OnJupiter_ReturnsCorrectWeight()
        {
            // Arrange
            var calculator = new WeightCalculator();
            double earthWeight = 70;
            double jupiterGravity = 2.34;
            double expected = 163.8; // 70 * 2.34 = 163.8

            // Act
            double actual = calculator.CalculateWeight(earthWeight, jupiterGravity);

            // Assert
            Assert.Equal(expected, actual);
        }

        // Тест 3: Перевірка реакції на некоректне введення (вага дорівнює 0)
        [Fact]
        public void CalculateWeight_ZeroWeight_ThrowsArgumentException()
        {
            // Arrange
            var calculator = new WeightCalculator();

            // Act & Assert (Перевіряємо, що метод викине помилку ArgumentException)
            Assert.Throws<ArgumentException>(() => calculator.CalculateWeight(0, 0.38));
        }
    }
}