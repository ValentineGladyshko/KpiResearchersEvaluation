using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для FacultyWindow.xaml
    /// </summary>
    public partial class FacultyWindow : Window
    {
        private Faculty LocalFaculty { get; set; }
        private ActionType Action { get; set; }
        private EFUnitOfWork unitOfWork = new EFUnitOfWork();
        public FacultyWindow()
        {
            InitializeComponent();

            LocalFaculty = new Faculty
            {
                Chairs = new List<Chair>()
            };

            Action = ActionType.Create;
            SetContent();
            SetHandlers();
        }

        public FacultyWindow(Faculty faculty, ActionType action)
        {
            InitializeComponent();

            LocalFaculty = faculty;
            
            LocalFaculty.Chairs = LocalFaculty.Chairs ?? new List<Chair>();            

            Action = action;

            SetContent();
            SetHandlers();
        }

        public void SetContent()
        {
            if (Action == ActionType.Edit)
            {
                Title = "Редагування факультету";
                WorkButton.Content = "Редагувати";
            }
            else if (Action == ActionType.Create)
            {
                Title = "Створення факультету";
                WorkButton.Content = "Створити";
            }
            
            FacultyBox.Text = LocalFaculty.FacultyName;            
        }

        public void SetHandlers()
        {
            CancelButton.Click += (object sender, RoutedEventArgs e) => { Close(); };

            WorkButton.Click += (object sender, RoutedEventArgs args) =>
            {                
                LocalFaculty.FacultyName = FacultyBox.Text;                

                if (Action == ActionType.Create)
                {
                    unitOfWork.Faculties.Create(LocalFaculty);
                    unitOfWork.Save();
                    Close();
                }
                else if (Action == ActionType.Edit)
                {
                    unitOfWork.Faculties.Update(LocalFaculty);
                    unitOfWork.Save();
                    Close();
                }
            };            
        }
    }
}
