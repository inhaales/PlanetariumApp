using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PlanetariumApp.Models;

namespace PlanetariumApp
{
    public partial class MainWindow : Window
    {
        private System.Collections.Generic.List<QuizQuestion> _questions = new();
        private int _currentQuestionIndex = 0;
        private int _score = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HideAllPanels()
        {
            StatusText.Visibility = Visibility.Collapsed;
            DataDisplayList.Visibility = Visibility.Collapsed;
            QuizPanel.Visibility = Visibility.Collapsed;
            WeightCalcPanel.Visibility = Visibility.Collapsed;
        }

        // 1. Натискання на Словник
        private void Menu_Glossary_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Словник термінів";
            PageTitle.Visibility = Visibility.Visible;
            HideAllPanels();
            DataDisplayList.Visibility = Visibility.Visible;

            using (var db = new PlanetariumDbContext())
            {
                var data = db.Glossary
                             .Select(g => new {
                                 Title = g.Term,
                                 Details = g.Definition,
                                 Image = (string?)null,
                                 AdditionalImages = (System.Collections.Generic.List<string>?)null
                             })
                             .ToList();

                DataDisplayList.ItemsSource = data;
            }
        }

        // 2. Натискання на Космічні об'єкти
        private void Menu_CelestialBodies_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Космічні об'єкти";
            PageTitle.Visibility = Visibility.Visible;
            HideAllPanels();
            DataDisplayList.Visibility = Visibility.Visible;

            using (var db = new PlanetariumDbContext())
            {
                var data = db.CelestialBodies
                             .Select(c => new {
                                 Title = c.Name,
                                 Details = "Тип: " + c.Type + "\nМаса: " + c.Mass + "\nВідстань від Землі: " + c.DistanceFromEarth + "\n\n" + c.Description,
                                 Image = c.ImagePath != null ? "/Images/" + c.ImagePath : null,
                                 AdditionalImages = (System.Collections.Generic.List<string>?)null
                             })
                             .ToList();
                DataDisplayList.ItemsSource = data;
            }
        }

        // 3. Натискання на Сузір'я
        private void Menu_Constellations_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Сузір'я неба";
            PageTitle.Visibility = Visibility.Visible;
            HideAllPanels();
            DataDisplayList.Visibility = Visibility.Visible;

            using (var db = new PlanetariumDbContext())
            {
                var data = db.Constellations
                             .Select(c => new {
                                 Title = c.Name + " (" + c.LatinName + ")",
                                 Details = "Найкращий час для спостереження: " + c.BestWatchingTime + "\n\n" + c.Symbolism,
                                 Image = c.ImagePath != null ? "/Images/" + c.ImagePath : null,
                                 AdditionalImages = (System.Collections.Generic.List<string>?)null
                             })
                             .ToList();
                DataDisplayList.ItemsSource = data;
            }
        }

        // 4. Натискання на Вікторину
        private void Menu_Quiz_Click(object sender, RoutedEventArgs e)
        {
            BtnAnswerA.Visibility = Visibility.Visible;
            BtnAnswerB.Visibility = Visibility.Visible;
            BtnAnswerC.Visibility = Visibility.Visible;
            BtnAnswerD.Visibility = Visibility.Visible;

            PageTitle.Text = "Астро-Вікторина";
            PageTitle.Visibility = Visibility.Visible;

            HideAllPanels();
            QuizPanel.Visibility = Visibility.Visible;

            _score = 0;
            _currentQuestionIndex = 0;
            QuizScoreTextBlock.Text = $"Рахунок: {_score}";

            using (var db = new PlanetariumDbContext())
            {
                _questions = db.QuizQuestions.ToList();
            }

            DisplayQuestion();
        }

        private void DisplayQuestion()
        {
            if (_questions != null && _currentQuestionIndex < _questions.Count)
            {
                var q = _questions[_currentQuestionIndex];
                QuestionTextBlock.Text = q.QuestionText;
                BtnAnswerA.Content = q.AnswerA;
                BtnAnswerB.Content = q.AnswerB;
                BtnAnswerC.Content = q.AnswerC;
                BtnAnswerD.Content = q.AnswerD;
            }
            else
            {
                QuestionTextBlock.Text = $"Вікторину завершено! Ваш фінальний результат: {_score} з {_questions?.Count}";
                BtnAnswerA.Visibility = Visibility.Collapsed;
                BtnAnswerB.Visibility = Visibility.Collapsed;
                BtnAnswerC.Visibility = Visibility.Collapsed;
                BtnAnswerD.Visibility = Visibility.Collapsed;
            }
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_questions == null || _currentQuestionIndex >= _questions.Count) return;

            Button clickedButton = (Button)sender;
            string selectedAnswer = clickedButton.Tag.ToString()!;

            var currentQuestion = _questions[_currentQuestionIndex];

            if (selectedAnswer == currentQuestion.CorrectAnswer)
            {
                _score++;
                MessageBox.Show("Правильно! Чудова робота.", "Успіх");
            }
            else
            {
                MessageBox.Show($"Неправильно. Правильна відповідь: {currentQuestion.CorrectAnswer}", "Ой!");
            }

            _currentQuestionIndex++;
            QuizScoreTextBlock.Text = $"Рахунок: {_score}";
            DisplayQuestion();
        }

        // 5. Натискання на Космічну вагу
        private void Menu_WeightCalculator_Click(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            WeightCalcPanel.Visibility = Visibility.Visible;

            PageTitle.Text = "Калькулятор космічної ваги";
            PageTitle.Visibility = Visibility.Visible;

            EarthWeightInput.Text = "";
            WeightResultTextBlock.Text = "";
            PlanetComboBox.SelectedIndex = 0;
        }

        private void CalculateWeight_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(EarthWeightInput.Text, out double earthWeight) || earthWeight <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну вагу (додатне число).", "Помилка введення");
                return;
            }

            if (PlanetComboBox.SelectedItem is ComboBoxItem selectedPlanet)
            {
                string planetName = selectedPlanet.Content.ToString()!;
                double gravityFactor = double.Parse(selectedPlanet.Tag.ToString()!, System.Globalization.CultureInfo.InvariantCulture);

                var calculator = new WeightCalculator();
                try
                {
                    double planetWeight = calculator.CalculateWeight(earthWeight, gravityFactor);
                    WeightResultTextBlock.Text = $"На об'єкті {planetName} ваша вага становитиме:\n{planetWeight} кг!";
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Помилка розрахунку");
                }
            }
        }

        // 6. Натискання на кнопку "Космічні місії"
        private void Menu_Missions_Click(object sender, RoutedEventArgs e)
        {
            PageTitle.Text = "Важливі космічні відкриття та місії";
            PageTitle.Visibility = Visibility.Visible;
            HideAllPanels();
            DataDisplayList.Visibility = Visibility.Visible;

            using (var db = new PlanetariumDbContext())
            {
                var missions = db.CosmicMissions.ToList();

                var data = missions.Select(m => {
                    string[] imgFiles = m.ImagePath?.Split('|') ?? new string[0];
                    string? mainImage = imgFiles.Length > 0 ? "/Images/" + imgFiles[0] : null;

                    var galleryImages = imgFiles.Skip(1)
                                                .Select(f => "/Images/" + f)
                                                .ToList();

                    return new
                    {
                        Title = m.DiscoveryName,
                        Details = "Місія / Апарат: " + m.MissionOrShuttle + "\nРік відкриття: " + m.DiscoveryYear + "\n\n" + m.Description,
                        Image = mainImage,
                        AdditionalImages = (System.Collections.Generic.List<string>?)galleryImages
                    };
                }).ToList();

                DataDisplayList.ItemsSource = data;
            }
        }

        // Налаштування плавної та повільної прокрутки коліщатком
        private void DataDisplayList_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (sender is ListView listView)
            {
                var border = System.Windows.Media.VisualTreeHelper.GetChild(listView, 0) as Decorator;
                var scrollViewer = border?.Child as ScrollViewer;

                if (scrollViewer != null)
                {
                    double scrollOffset = scrollViewer.VerticalOffset - (e.Delta * 0.01);
                    scrollViewer.ScrollToVerticalOffset(scrollOffset);
                    e.Handled = true;
                }
            }
        }
    }
}