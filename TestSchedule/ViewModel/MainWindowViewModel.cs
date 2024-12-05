using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSchedule.Command;
using TestSchedule.Controller;
using TestSchedule.Core;
using TestSchedule.Model;

namespace TestSchedule.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly MainWindowController _controller;

        private string _selectedGroup;

        public string SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        private ObservableCollection<string> _groupList;

        public ObservableCollection<string> GroupList
        {
            get => _groupList;
            set
            {
                _groupList = value;
                OnPropertyChanged(nameof(GroupList));
            }
        }

        private ObservableCollection<string> _facultyList;

        public ObservableCollection<string> FacultyList
        {
            get => _facultyList;
            set
            {
                _facultyList = value;
                OnPropertyChanged(nameof(FacultyList));
            }
        }

        private string _selectedFaculty;

        public string SelectedFaculty
        {
            get => _selectedFaculty;
            set
            {
                _selectedFaculty = value;
                OnPropertyChanged(nameof(SelectedFaculty));

                LoadGroups();
            }
        }

        private ObservableCollection<Schedule> _listOfSchedule;

        public ObservableCollection<Schedule> ListOfSchedule
        {
            get => _listOfSchedule;
            set
            {
                _listOfSchedule = value;
                OnPropertyChanged(nameof(ListOfSchedule));
            }
        }

        public ICommand Get { get; private set; }

        //
        private readonly FacultyGroups _facultyGroups;

        public MainWindowViewModel()
        {
            _controller = new MainWindowController();

            GroupList = new ObservableCollection<string>();
            FacultyList = new ObservableCollection<string>();
            ListOfSchedule = new ObservableCollection<Schedule>();
            FacultySingleton.FacultyList.ForEach(FacultyList.Add);

            Get = new DelegateCommand(GetData);

            //MYCODE
            _facultyGroups = new FacultyGroups();
            _maxFaculty = 13;
            _random = new Random();
            _currentFaculty = 0;

            Start();
        }

        private async void LoadGroups()
        {
            try
            {
                GroupList.Clear();

                var result = await _controller.GetGroups(SelectedFaculty);

                result?.ForEach(GroupList.Add);
            }
            catch
            {
                MessageBox.Error("При попытке получить данные произошла ошибка");
            }
        }

        private async void GetData(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedFaculty) || string.IsNullOrEmpty(SelectedGroup))
                {
                    MessageBox.Info("Все поля должны быть заполнены");

                    return;
                }

                var result = await _controller.GetSchedule(SelectedGroup, SelectedFaculty);

                result?.ForEach(ListOfSchedule.Add);
            }
            catch
            {
                MessageBox.Error("При попытке получить данные произошла ошибка");
            }
        }

        private int _currentFaculty;
        private int _maxFaculty;
        private Random _random;

        public async Task Start()
        {
            while (true)
            {
                var faculty = _facultyGroups.GetFaculty(_currentFaculty.ToString());
                var groups = await _controller.GetGroups(faculty);
                var randomGroup = groups[_random.Next(groups.Count)];

                if (typeof(Entries).GetProperties().All(p => p.GetValue(Entries) == null) && DayTextBlock.Text == DayOfWeek)
                {
                    //  вывод в датаТеймплайт
                }

                _currentFaculty++;
                if (_currentFaculty > _maxFaculty)
                {
                    _currentFaculty = 0;
                }

                await Task.Delay(10000); // пауза на 10 секунд ПОМЕНЯТЬ НА ТАЙМЕР
            }
        }
    }
}