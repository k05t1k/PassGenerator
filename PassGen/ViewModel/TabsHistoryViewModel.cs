using Newtonsoft.Json;
using PassGen.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGen.ViewModel
{
    class TabsHistoryViewModel : BindingHelper
    {
        #region Команды
        public BindableCommand AddRow { get; set; }

        #endregion

        #region Свойства
        private List<History> historySelected;

        public List<History> HistorySelected
        {
            get { return historySelected; }
            set 
            { 
                historySelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Переменные

        #endregion

        #region Функции
        public void AddRowFromClass()
        {
            string json = File.ReadAllText("C:\\Users\\cutec\\OneDrive\\Рабочий стол\\Gen.json");
            HistorySelected = JsonConvert.DeserializeObject<List<History>>(json);
        }
        #endregion
        public TabsHistoryViewModel() 
        {
            AddRow = new BindableCommand(_ => AddRowFromClass());
        }
    }
}
