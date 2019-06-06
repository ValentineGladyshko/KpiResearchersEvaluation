using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Repositories;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WebLibrary.Indexes;
using WebLibrary.Entities;
using Chair = DatabaseLibrary.DatabaseModel.Chair;
using System.Collections.Generic;
using WebLibrary.Parsers;
using Researcher = DatabaseLibrary.DatabaseModel.Researcher;
using System;
using WebLibrary;
using System.Threading.Tasks;

namespace DesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EFUnitOfWork unitOfWork = new EFUnitOfWork();

        Faculty LocalFaculty = new Faculty();
        Chair LocalChair = new Chair();
        public MainWindow()
        {
            InitializeComponent();

            SetFacultyHandlers();
            SetChairHandlers();
            SetResearcherHandlers();
            SetDataCheckHandlers();

            this.Closing += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                WebSocket.Driver.Quit();
            };
        }


        private void SetFacultyHandlers()
        {
            FacultyBox.ItemsSource = unitOfWork.Faculties.GetAll().ToList();

            FacultyBox.SelectionChanged += (object sender, SelectionChangedEventArgs args) =>
            {
                var faculty = FacultyBox.SelectedItem as Faculty;
                if (faculty != null)
                {
                    LocalFaculty = faculty;
                    ListBoxChair.ItemsSource = unitOfWork.Chairs
                        .Find(item => item.FacultyId == LocalFaculty.FacultyId)
                        .Select(item => new WebLibrary.Entities.Chair(item, unitOfWork, new HIndex()));
                }
            };
            FacultyBox.SelectedIndex = 0;

            FacultyCreateButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var facultyToCreate = new Faculty();
                FacultyWindow facultyWindow = new FacultyWindow(facultyToCreate, ActionType.Create)
                {
                    Owner = this
                };
                facultyWindow.ShowDialog();

                FacultyUpdate();
            };

            FacultyEditButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var facultyToEdit = FacultyBox.SelectedItem as Faculty;
                FacultyWindow facultyWindow = new FacultyWindow(facultyToEdit, ActionType.Edit)
                {
                    Owner = this
                };
                facultyWindow.ShowDialog();

                FacultyUpdate();
            };

            FacultyDeleteButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var facultyToDelete = FacultyBox.SelectedItem as Faculty;

                string message = "Ви впевнені що хочете видалити дані про факультет "
                                 + facultyToDelete.FacultyName + " та всіх кафедр цього факультету?";

                DialogWindow dialogWindow = new DialogWindow(message);
                bool? dialogResult = dialogWindow.ShowDialog();

                if (dialogResult != true)
                    return;

                unitOfWork.Faculties.Delete(facultyToDelete.FacultyId);
                unitOfWork.Save();

                FacultyUpdate();
            };

            ChairFacultyCreateButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var chair = new Chair();
                chair.Faculty = FacultyBox.SelectedItem as Faculty;
                ChairWindow chairWindow = new ChairWindow(chair, ActionType.Create)
                {
                    Owner = this
                };
                chairWindow.ShowDialog();

                ChairUpdate();
            };
        }

        private void SetChairHandlers()
        {
            ChairBox.ItemsSource = unitOfWork.Chairs.GetAll().ToList();

            ChairBox.SelectionChanged += (object sender, SelectionChangedEventArgs args) =>
            {
                var chair = ChairBox.SelectedItem as Chair;
                if (chair != null)
                {
                    LocalChair = chair;
                    ListBoxChairResearcher.ItemsSource = unitOfWork.Researchers
                        .Find(item => item.ChairId == LocalChair.ChairId)
                        .Select(item => new WebLibrary.Entities.FullResearcher(item, unitOfWork));
                }
            };
            ChairBox.SelectedIndex = 0;

            ChairCreateButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var chair = new Chair();
                ChairWindow chairWindow = new ChairWindow(chair, ActionType.Create)
                {
                    Owner = this
                };
                chairWindow.ShowDialog();

                ChairUpdate();
            };

            ChairEditButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var chair = ChairBox.SelectedItem as Chair;
                ChairWindow chairWindow = new ChairWindow(chair, ActionType.Create)
                {
                    Owner = this
                };
                chairWindow.ShowDialog();

                ChairUpdate();
            };

            ChairDeleteButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var chairToDelete = ChairBox.SelectedItem as Chair;

                string message = "Ви впевнені що хочете видалити дані про кафедру "
                                 + chairToDelete.ChairName + " та всіх викладачів цієї кафедри?";

                DialogWindow dialogWindow = new DialogWindow(message);
                bool? dialogResult = dialogWindow.ShowDialog();

                if (dialogResult != true)
                    return;

                unitOfWork.Chairs.Delete(chairToDelete.ChairId);
                unitOfWork.Save();

                ChairUpdate();
            };

            ResearcherChairCreateButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var researcher = new Researcher();
                researcher.Chair = ChairBox.SelectedItem as Chair;
                ResearcherWindow researcherWindow = new ResearcherWindow(researcher, ActionType.Create)
                {
                    Owner = this
                };
                researcherWindow.ShowDialog();

                ChairUpdate();
            };
        }

        private void SetResearcherHandlers()
        {

            FirstNameBox.ItemsSource = unitOfWork.Researchers.GetAll()
            .Select(item => item.FirstName).Distinct().ToList();

            MiddleNameBox.ItemsSource = unitOfWork.Researchers.GetAll()
                .Select(item => item.MiddleName).Distinct().ToList();

            LastNameBox.ItemsSource = unitOfWork.Researchers.GetAll()
                .Select(item => item.LastName).Distinct().ToList();


            SearchButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var query = unitOfWork.Researchers.Find(item =>
                        item.FirstName.Contains(FirstNameBox.Text)
                        && item.MiddleName.Contains(MiddleNameBox.Text)
                        && item.LastName.Contains(LastNameBox.Text));

                ListBoxResearcher.ItemsSource = query.Select(item => new WebLibrary.Entities.FullResearcher(item, unitOfWork));

            };

            ResearcherCreateButton.Click += (object sender, RoutedEventArgs e) =>
            {
                var researcher = new Researcher();
                ResearcherWindow researcherWindow = new ResearcherWindow(researcher, ActionType.Create)
                {
                    Owner = this
                };
                researcherWindow.ShowDialog();

                ChairUpdate();
            };
        }

        private void SetDataCheckHandlers()
        {
            CheckButton.Click += (object sender, RoutedEventArgs e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        var list = new List<Data>();
                        var dataRaw = File.ReadAllLines(openFileDialog.FileName).Select(line => line.Split(';'));
                        foreach (var row in dataRaw)
                        {
                            var data = new Data
                            {
                                Faculty = row[0],
                                Chair = row[1],
                                LastName = row[2],
                                FirstName = row[3],
                                MiddleName = row[4],
                                Orcid = row[5],
                                GoogleHIndexOld = int.Parse(row[6])
                            };

                            var researchers = unitOfWork.Researchers.Find(item =>
                                item.FirstName.ToLower() == data.FirstName.ToLower()
                                && item.MiddleName.ToLower() == data.MiddleName.ToLower()
                                && item.LastName.ToLower() == data.LastName.ToLower()
                                && item.Chair.ChairName.ToLower() == data.Chair.ToLower()
                                && item.Chair.Faculty.FacultyName.ToLower() == data.Faculty.ToLower());

                            if (researchers.Count() == 0)
                            {
                                ResearcherSaver.SaveResearcher(data.Faculty, data.Chair,
                                    data.FirstName, data.MiddleName, data.LastName, data.Orcid, unitOfWork);
                                data.GoogleHIndex = new FullResearcher(unitOfWork.Researchers.Find(item =>
                                item.FirstName.ToLower() == data.FirstName.ToLower()
                                && item.MiddleName.ToLower() == data.MiddleName.ToLower()
                                && item.LastName.ToLower() == data.LastName.ToLower()
                                && item.Chair.ChairName.ToLower() == data.Chair.ToLower()
                                && item.Chair.Faculty.FacultyName.ToLower() == data.Faculty.ToLower()).First(), unitOfWork).GoogleHIndex;
                            }
                            else
                            {
                                data.GoogleHIndex = new FullResearcher(researchers.First(), unitOfWork).GoogleHIndex;
                            }

                            list.Add(data);
                        }
                        DataList.ItemsSource = list;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message + " Файл містить дані в недопустимому форматі\nДопустимий формат:" +
                            " факультет;кафедра;Прізвище;ім'я;по-батькові;id;індекс Гірша"
                        , "Неправильний файл", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            };
            DownloadButton.Click += (object sender, RoutedEventArgs e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {                        
                        var dataRaw = File.ReadAllLines(openFileDialog.FileName).Select(line => line.Split(';'));
                        foreach (var row in dataRaw)
                        {
                            var data = new Data
                            {
                                Faculty = row[0],
                                Chair = row[1],
                                LastName = row[2],
                                FirstName = row[3],
                                MiddleName = row[4],
                                Orcid = row[5],
                                GoogleHIndexOld = int.Parse(row[6])
                            };

                            ResearcherSaver.SaveResearcher(data.Faculty, data.Chair,
                                    data.FirstName, data.MiddleName, data.LastName, data.Orcid, unitOfWork);
                            //string message = "qwer";

                            //DialogWindow dialogWindow = new DialogWindow(message);
                            //dialogWindow.Show();
                        }
                        
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message + " Файл містить дані в недопустимому форматі\nДопустимий формат:" +
                            " факультет;кафедра;Прізвище;ім'я;по-батькові;id;індекс Гірша"
                        , "Неправильний файл", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            };
    }

        private void ChairDeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var chairToDelete = unitOfWork.Chairs.Get((int)button.Tag);

            string message = "Ви впевнені що хочете видалити дані про кафедру "
                             + chairToDelete.ChairName + " та всіх викладачів цієї кафедри?";

            DialogWindow dialogWindow = new DialogWindow(message);
            bool? dialogResult = dialogWindow.ShowDialog();

            if (dialogResult != true)
                return;

            unitOfWork.Chairs.Delete(chairToDelete.ChairId);
            unitOfWork.Save();

            ChairUpdate();

        }

        private void ChairEditButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var chair = unitOfWork.Chairs.Get((int)button.Tag);
            ChairWindow chairWindow = new ChairWindow(chair, ActionType.Create)
            {
                Owner = this
            };
            chairWindow.ShowDialog();

            ChairUpdate();
        }

        private void ResearcherDeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var researcherToDelete = unitOfWork.Researchers.Get((int)button.Tag);

            string message = "Ви впевнені що хочете видалити дані про викладача "
                             + researcherToDelete.LastName + " "
                             + researcherToDelete.FirstName
                             + " " + researcherToDelete.MiddleName + "?";

            DialogWindow dialogWindow = new DialogWindow(message);
            bool? dialogResult = dialogWindow.ShowDialog();

            if (dialogResult != true)
                return;

            unitOfWork.Researchers.Delete(researcherToDelete.ResearcherId);
            unitOfWork.Save();

            SearchButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void ResearcherEditButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var researcher = unitOfWork.Researchers.Get((int)button.Tag);
            ResearcherWindow researcherWindow = new ResearcherWindow(researcher, ActionType.Create)
            {
                Owner = this
            };
            researcherWindow.ShowDialog();

            ResearcherUpdate();
        }

        private void ResearcherChairDeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var researcherToDelete = unitOfWork.Researchers.Get((int)button.Tag);

            string message = "Ви впевнені що хочете видалити дані про викладача "
                             + researcherToDelete.LastName + " "
                             + researcherToDelete.FirstName
                             + " " + researcherToDelete.MiddleName + "?";

            DialogWindow dialogWindow = new DialogWindow(message);
            bool? dialogResult = dialogWindow.ShowDialog();

            if (dialogResult != true)
                return;

            unitOfWork.Researchers.Delete(researcherToDelete.ResearcherId);
            unitOfWork.Save();

            ChairBox.RaiseEvent(new RoutedEventArgs(ComboBox.SelectedEvent));
        }

        private void ResearcherChairEditButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var researcher = unitOfWork.Researchers.Get((int)button.Tag);
            ResearcherWindow researcherWindow = new ResearcherWindow(researcher, ActionType.Create)
            {
                Owner = this
            };
            researcherWindow.ShowDialog();

            ResearcherUpdate();
        }

        private void FacultyUpdate()
        {
            FacultyBox.ItemsSource = unitOfWork.Faculties.GetAll().ToList();
            FacultyBox.SelectedIndex = 0;
            FacultyBox.RaiseEvent(new RoutedEventArgs(ComboBox.SelectedEvent));

            ChairUpdate();            
        }

        private void ChairUpdate()
        {
            ListBoxChair.ItemsSource = unitOfWork.Chairs
                        .Find(item => item.FacultyId == LocalFaculty.FacultyId)
                        .Select(item => new WebLibrary.Entities.Chair(item, unitOfWork, new HIndex()));

            ChairBox.ItemsSource = unitOfWork.Chairs.GetAll().ToList();
            ChairBox.SelectedIndex = 0;
            ChairBox.RaiseEvent(new RoutedEventArgs(ComboBox.SelectedEvent));

            ResearcherUpdate();
        }

        private void ResearcherUpdate()
        {
            ListBoxChairResearcher.ItemsSource = unitOfWork.Researchers
                        .Find(item => item.ChairId == LocalChair.ChairId)
                        .Select(item => new WebLibrary.Entities.FullResearcher(item, unitOfWork));
        }        
    }
}
