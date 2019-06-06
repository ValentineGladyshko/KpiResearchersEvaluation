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
    /// Логика взаимодействия для ResearcherEditWindow.xaml
    /// </summary>
    public partial class ResearcherWindow : Window
    {        
        private Researcher LocalResearcher { get; set; }
        private ActionType Action { get; set; }
        private EFUnitOfWork unitOfWork = new EFUnitOfWork();
        public ResearcherWindow()
        {
            InitializeComponent();

            LocalResearcher = new Researcher
            {
                ResearcherAccounts = new List<ResearcherAccount>(),
                ResearcherOrcids = new List<ResearcherOrcid>()
            };

            Action = ActionType.Create;
            SetContent();
            SetHandlers();
        }

        public ResearcherWindow(Researcher Researcher, ActionType action)
        {
            InitializeComponent();

            LocalResearcher = Researcher;

            LocalResearcher.ResearcherAccounts = LocalResearcher.ResearcherAccounts ?? new List<ResearcherAccount>();
            LocalResearcher.ResearcherOrcids = LocalResearcher.ResearcherOrcids ?? new List<ResearcherOrcid>();

            Action = action;

            SetContent();
            SetHandlers();
        }

        public void SetContent()
        {
            ChairBox.ItemsSource = unitOfWork.Chairs.GetAll().ToList();

            if (Action == ActionType.Edit)
            {
                Title = "Редагування кафедри";
                WorkButton.Content = "Редагувати";
                ChairBox.SelectedIndex = ChairBox.Items.IndexOf(LocalResearcher.Chair);
            }
            else if (Action == ActionType.Create)
            {
                Title = "Створення кафедри";
                WorkButton.Content = "Створити";
                ChairBox.SelectedIndex = 0;
            }

            LastNameBox.Text = LocalResearcher.LastName;
            FirstNameBox.Text = LocalResearcher.FirstName;
            MiddleNameBox.Text = LocalResearcher.MiddleName;            
        }

        public void SetHandlers()
        {
            CancelButton.Click += (object sender, RoutedEventArgs e) => { Close(); };

            WorkButton.Click += (object sender, RoutedEventArgs args) =>
            {
                LocalResearcher.LastName = LastNameBox.Text;
                LocalResearcher.FirstName = FirstNameBox.Text;
                LocalResearcher.MiddleName = MiddleNameBox.Text;

                var localChair = ChairBox.SelectedItem as Chair;
                LocalResearcher.Chair = localChair;
                LocalResearcher.ChairId = localChair.ChairId;

                if (Action == ActionType.Create)
                {
                    unitOfWork.Researchers.Create(LocalResearcher);
                    unitOfWork.Save();
                    Close();
                }
                else if (Action == ActionType.Edit)
                {
                    unitOfWork.Researchers.Update(LocalResearcher);
                    unitOfWork.Save();
                    Close();
                }
            };
        }
    }
}
