using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using AIS_Theatre.DAL;
using AIS_Theatre.Data;
using AIS_Theatre.UI.MVVM;

namespace AIS_Theatre.UI.ViewModels
{
    public class EditGenreVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly Genre _genre;
        private string _name;
        private Guid _id;

        public EditGenreVM()
        {

        }

        public EditGenreVM(Genre genre) : this()
        {
            _genre = genre.Clone();//We don't want to trash main window

            Name = _genre.Name;
            Id = _genre.Id;
        }

        public String Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public Guid Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return RelayCommand.Create<Window>(
                    w =>
                    {
                        using (var unitOfWork = UnitOfWorkFactory.CreateInstance())
                        {
                            var genre = _genre ?? new Genre(Name) { };

                            genre.Name = Name;
                            genre.Id = Id;

                            unitOfWork.GenreRepository.Upsert(genre);

                            unitOfWork.Commit();

                            w.DialogResult = true;
                            w.Close();
                        }
                    },
                    _ => IsValid()
                );
            }
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                if (Application.Current.Dispatcher.Thread != Thread.CurrentThread)
                    Application.Current.Dispatcher.BeginInvoke(handler, this, new PropertyChangedEventArgs(propertyName));
                else
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        return String.IsNullOrWhiteSpace(Name) ? columnName + " can't be empty" : null;
                }

                return null;
            }
        }

        public string Error { get; private set; }

        public bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Name);
        }

        #endregion

    }
}
