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


        public MainViewModel()
        {
            
            _db = new DB();
            _items = _db.TaskItems.ToList();
        }


        public ICommand AddItemCommand => new Command(() =>
        {
            TaskItem newItem = new TaskItem() { Title = CurrentTitle }; 
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






        //string _name = "sdf";
        //public string Name
        //{
        //    get { return _name; }
        //    set
        //    {
        //        _name = value;
        //        OnPropertyChanged(nameof(Name));
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
