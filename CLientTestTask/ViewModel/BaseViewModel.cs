using CLientTestTask.Repository.Bases;
using CLientTestTask.ViewModel.Bases;
using CLientTestTask.ViewModel.ComadsBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CLientTestTask.ViewModel
{
    public abstract class BaseViewModel<T> : NotifyProperty, IDataErrorInfo
        where T : class
    {
        protected readonly IRepository<T> repository;

        private Visibility _visibilityOn = Visibility.Visible;

        private CommandBase getItem;
        private CommandBase getItems;
        private CommandBase deleteCommand;
        private CommandBase changeVisibilityCommnad;

        private int _id = 1;
        private string _message;

        protected ObservableCollection<T> items;
        protected T value;

        public BaseViewModel(IRepository<T> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange(nameof(Id));
            }

        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChange(nameof(Message));
            }
        }

        public virtual Page EditGetPage
        {
            get { throw new NotImplementedException(); }

        }

        public ObservableCollection<T> GetValues
        {
            get { return items; }
            set
            {
                if (value != null)
                {
                    items = value;
                }
                OnPropertyChange(nameof(GetValues));
            }
        }
        public CommandBase GetItems
        {
            get
            {
                return getItems ?? (getItems = new CommandBase(_ =>
                {
                    GetValues = new ObservableCollection<T>(repository.GetValues());
                    VisibilityOn = Visibility.Visible;
                }
               ));
            }

        }


        public abstract T GetValue
        {
            get;
            set;

        }

        public CommandBase GetItem
        {
            get
            {
                return getItem ?? (getItem = new CommandBase(_ =>
                    {
                        GetValue = repository.GetValue(Id);
                    }
               ));
            }

        }

        public Visibility VisibilityOn
        {
            get { return _visibilityOn; }
            set
            {
                _visibilityOn = value;
                OnPropertyChange(nameof(VisibilityOn));
                OnPropertyChange(nameof(VisibilityOff));
            }
        }

        public Visibility VisibilityOff
        {
            get { return _visibilityOn ^ Visibility.Collapsed; }

        }

        public CommandBase ChangeVisibility
        {
            get
            {
                return changeVisibilityCommnad ?? (changeVisibilityCommnad = new CommandBase(
                 _ =>
                 {
                     VisibilityOn = VisibilityOn ^ Visibility.Collapsed;
                 }
                ));
            }
        }

        public string Error => throw new NotImplementedException();

        public abstract CommandBase Create
        {
            get;
        }

        public abstract CommandBase Update
        {
            get;
        }
        public CommandBase DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new CommandBase(_ =>
                    Message = repository.Delete(Id)
                ));
            }
        }

        public string this[string columnName]
        {
            get
            {

                string error = string.Empty;

                switch (columnName)
                {

                    case nameof(Id):
                        if (Id <= 0)
                        {
                            error = "Id не может быть меньше или равным 0";
                            Id = 1;
                        }

                        break;
                }

                return error;
            }


        }
    }
}
