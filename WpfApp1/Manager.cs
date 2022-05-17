using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1
{
    public class Manager
    {
        private static Manager _INSTANCE;

        public static Manager Instance
        {
            get
            {
                if (_INSTANCE == null) _INSTANCE = new Manager();
                return _INSTANCE;
            }
        }

        public course_work_WPF_EFEntities2 Context { get; set; }
        public Frame MainFrame { get; set; }
        public Frame MenuFrame { get; set; }
    }
}
