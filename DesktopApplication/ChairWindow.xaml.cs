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
    /// Логика взаимодействия для ChairWindow.xaml
    /// </summary>
    public partial class ChairWindow : Window
    {
        private Chair LocalChair { get; set; }
        private ActionType Action { get; set; }
        private EFUnitOfWork unitOfWork = new EFUnitOfWork();
        public ChairWindow()
        {
            InitializeComponent();

            LocalChair = new Chair
            {
                Researchers = new List<Researcher>()
            };

            Action = ActionType.Create;
            SetContent();
            SetHandlers();
        }

        public ChairWindow(Chair chair, ActionType action)
        {
            InitializeComponent();

            LocalChair = chair;

            LocalChair.Researchers = LocalChair.Researchers ?? new List<Researcher>();

            Action = action;

            SetContent();
            SetHandlers();
        }

        public void SetContent()
        {
            FacultyBox.ItemsSource = unitOfWork.Faculties.GetAll().ToList();            

            if (Action == ActionType.Edit)
            {
                Title = "Редагування кафедри";
                WorkButton.Content = "Редагувати";
                FacultyBox.SelectedIndex = FacultyBox.Items.IndexOf(LocalChair.Faculty);
            }
            else if (Action == ActionType.Create)
            {
                Title = "Створення кафедри";
                WorkButton.Content = "Створити";
                FacultyBox.SelectedIndex = 0;
            }

            ChairBox.Text = LocalChair.ChairName;            
        }

        public void SetHandlers()
        {
            CancelButton.Click += (object sender, RoutedEventArgs e) => { Close(); };

            WorkButton.Click += (object sender, RoutedEventArgs args) =>
            {
                LocalChair.ChairName = ChairBox.Text;
                var localFaculty = FacultyBox.SelectedItem as Faculty;
                LocalChair.Faculty = localFaculty;
                LocalChair.FacultyId = localFaculty.FacultyId;

                if (Action == ActionType.Create)
                {
                    unitOfWork.Chairs.Create(LocalChair);
                    unitOfWork.Save();
                    Close();
                }
                else if (Action == ActionType.Edit)
                {
                    unitOfWork.Chairs.Update(LocalChair);
                    unitOfWork.Save();
                    Close();
                }
            };
        }
    }
}
