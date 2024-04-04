using Newtonsoft.Json;
using PassGen.View;
using PassGen.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassGen.ViewModel
{
    class MainViewModel : BindingHelper
    {
        #region Команды

        public BindableCommand Generate { get; set; }
        public BindableCommand OpenHistory { get; set; }


        #endregion

        #region Свойства
        private Type_T selectedType;

        public Type_T SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged();
                if (selectedType == Type_T.Random)
                    IsEnabled = true;
                else
                    IsEnabled = false;
            }
        }

        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        private string countSelected;

        public string CountSelected
        {
            get { return countSelected; }
            set
            {
                countSelected = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Type_T> TypeTValues => Enum.GetValues(typeof(Type_T)).Cast<Type_T>();
        #endregion

        #region Переменные
        public string letters = "qwertyuiopasdfghjklzxcvbnm";

        public string symbols = "!@#$%*()";

        public List<History> histories = new List<History>();
        #endregion

        #region Функции
        public string GenerateNumbers(uint count)
        {
            var rand = new Random();
            string result = null;
            for (int i = 0; i < count; i++) { result += Convert.ToString(rand.Next(9)); }
            return result;
        }

        public string GenerateLetters(uint count)
        {
            var rand = new Random();
            string result = null;
            for (int i = 0; i < count; i++)
            {
                if (rand.Next(2) % 2 == 0)
                    result += letters[rand.Next(letters.Length)];
                else
                {
                    string upper = letters.ToUpper();
                    result += upper[rand.Next(letters.Length)];
                }
            }
            return result;
        }

        public string GenerateRandom(uint count)
        {
            var rand = new Random();
            string result = null;
            int switcher = IsChecked ? 3 : 2;
            for (int i = 0; i < count; i++)
            {
                int randomValue = rand.Next(1, switcher + 1);
                switch (randomValue)
                {
                    case (int)Type_T.Numbers:
                        result += GenerateNumbers(1);
                        break;
                    case (int)Type_T.Letters:
                        result += GenerateLetters(1);
                        break;
                    case (int)Type_T.Random:
                        result += symbols[rand.Next(symbols.Length)];
                        break;
                }
            }
            return result;
        }

        public void GeneratePass()
        {
            if (string.IsNullOrEmpty(CountSelected) || SelectedType == 0)
            {
                MessageBox.Show("Вы не ввели количество или не выбрали данные!", "Ошибка!");
                return;
            }

            if (uint.Parse(CountSelected) > 64)
            {
                MessageBox.Show("Длина символов должна быть не больше 64!", "Ошибка!");
                return;
            }

            string result = string.Empty;
            switch (SelectedType)
            {
                case Type_T.Numbers:
                    result = GenerateNumbers(uint.Parse(CountSelected));
                    histories.Add(new History(result, Convert.ToInt16(CountSelected)));
                    break;
                case Type_T.Letters:
                    result = GenerateLetters(uint.Parse(CountSelected));
                    histories.Add(new History(result, Convert.ToInt16(CountSelected)));
                    break;
                case Type_T.Random:
                    result = GenerateRandom(uint.Parse(CountSelected));
                    histories.Add(new History(result, Convert.ToInt16(CountSelected)));
                    break;
            }
            Clipboard.SetText(result);
            MessageBox.Show("Ваш сгенерированный пароль сохранён в буфер обмена!", "Успешно!");

            string json = JsonConvert.SerializeObject(histories);
            File.WriteAllTextAsync("C:\\Users\\cutec\\OneDrive\\Рабочий стол\\Gen.json", json);
        }

        public void OpenPage()
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.Show();
        }
        #endregion

        public MainViewModel()
        {
            Generate = new BindableCommand(_ => GeneratePass());
            OpenHistory = new BindableCommand(_ => OpenPage());
        }
    }
}
