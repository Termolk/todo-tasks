using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TODO_TASKS.Models;
using TODO_TASKS.Services;

namespace TODO_TASKS.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DB _db;

        private List<TaskItem> _items;
        public List<TaskItem> Items
        {
            get { return _items; }
            set 
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private TaskItem _selectedItem;
        public TaskItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _currentTitle;
        public string CurrentTitle
        {
            get { return _currentTitle; }
            set
            {
                _currentTitle = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private string _currentDescription;
        public string CurrentDescription
        {
            get { return _currentDescription; }
            set
            {
                _currentDescription = value;
                OnPropertyChanged(nameof(CurrentDescription));
            }
        }

        private DateTime _currentDueDate;
        public DateTime CurrentDueDate
        {
            get { return _currentDueDate; }
            set
            {
                _currentDueDate = value;
                OnPropertyChanged(nameof(CurrentDueDate));
            }
        }

        private string _currentPriority;
        public string CurrentPriority
        {
            get { return _currentPriority; }
            set
            {
                _currentPriority = value;
                OnPropertyChanged(nameof(CurrentPriority));
            }
        }

        private string _searchTitle;
        public string SearchTitle
        {
            get { return _searchTitle; }
            set
            {
                _searchTitle = value;
                OnPropertyChanged(nameof(SearchTitle));
                Search();
            }
        }

        private List<string> _priorities;
        public List<string> Priorities
        {
            get { return _priorities; }
            set
            {
                _priorities = value;
                OnPropertyChanged(nameof(Priorities));
            }
        }

        public MainViewModel()
        {
            
            _db = new DB();
            _items = _db.TaskItems.ToList();
            _priorities = new List<string>()
            {
                "Low",
                "Medium",
                "Hign"
            };
        }


        public ICommand AddItemCommand => new Command(() =>
        {
            TaskItem newItem = new TaskItem()
            {
                Title = CurrentTitle,
                Description = CurrentDescription,
                DueDate = CurrentDueDate,
                Priority = CurrentPriority
            };
            _db.TaskItems.Add(newItem);
            _db.SaveChanges();
            Items = _db.TaskItems.ToList();
        });


        public ICommand UpdateItemCommand => new Command(() =>
        {
            _db.SaveChanges();
            Items = _db.TaskItems.ToList();
        });

        public ICommand DeleteItemCommand => new Command(() =>
        {
            if (SelectedItem != null)
            {
                _db.TaskItems.Remove(SelectedItem);
                _db.SaveChanges();
                Items = _db.TaskItems.ToList();
                SelectedItem = null;
            }
        });

        public void Search()
        {
            if (string.IsNullOrEmpty(SearchTitle))
            {
                Items = _db.TaskItems.ToList();
            }
            else
            {
                Items = _db.TaskItems.Where(x => x.Title.Contains(SearchTitle)).ToList();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
