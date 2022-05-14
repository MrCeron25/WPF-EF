using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.ViewModels.Base;

namespace WpfApp1.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Заголовок
        private string _Tiile = "Альта";

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title
        {
            get => _Tiile;
            set => Set(ref _Tiile, value);
        }

        #endregion
    }
}
