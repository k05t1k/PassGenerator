using PassGen.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassGen.ViewModel
{
    class EmptyHistoryViewModel : BindingHelper
    {
        #region Команды
        public BindableCommand Message { get; set; }
        #endregion

        #region Фунции
        public void ShowMessage()
        {
            MessageBox.Show("Нажми на крестик справа сверху, дурень)", "ПАСХАЛКООООО");
        }
        #endregion

        public EmptyHistoryViewModel() 
        {
            Message = new BindableCommand(_ => ShowMessage());
        }
    }
}
