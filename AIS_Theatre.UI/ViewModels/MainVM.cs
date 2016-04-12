using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using AIS_Theatre.DAL;
using AIS_Theatre.Data;
using AIS_Theatre.UI.Debug;
using AIS_Theatre.UI.MVVM;
using AIS_Theatre.UI.Views;

namespace AIS_Theatre.UI.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        private IList<Genre> _allGenres;
        private Genre _currentSelection;
        private Genre _currentGenre;
        private Visibility _isEditEnabled;

        public MainVM()
        {
            UnitOfWorkFactory.__Initialize(() => new UnitOfWork());
            if (!GuiDebug.InDesignerMode)
            {
                _isEditEnabled = Visibility.Collapsed;

                RefreshList();
            }
        }

        public IList<Genre> AllGenres
        {
            get { return _allGenres; }
            set
            {
                if (Equals(value, _allGenres)) return;
                _allGenres = value;
                OnPropertyChanged();
            }
        }

        public Genre CurrentSelection
        {
            get { return _currentSelection; }
            set
            {
                if (Equals(value, _currentSelection)) return;
                _currentSelection = value;
                OnPropertyChanged();

                //Logic
                if (value != null)
                {
                    using (var unitOfWork = UnitOfWorkFactory.CreateInstance())
                    {
                        CurrentGenre = unitOfWork.GenreRepository.GetById(value.Id);
                    }
                }
                else
                {
                    CurrentGenre = null;
                }
            }
        }

        public Genre CurrentGenre
        {
            get { return _currentGenre; }
            set
            {
                if (Equals(value, _currentGenre)) return;
                _currentGenre = value;
                OnPropertyChanged();

                //Logic

                IsEditEnabled = value != null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsEditEnabled
        {
            get { return _isEditEnabled; }
            private set
            {
                if (value == _isEditEnabled) return;
                _isEditEnabled = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand RefreshListCommand
        {
            get { return RelayCommand.CreateVoid(RefreshList); }
        }

        public ICommand EditGenreCommand
        {
            get { return RelayCommand.CreateVoid(EditGenre); }
        }

        public ICommand AddGenreCommand
        {
            get { return RelayCommand.CreateVoid(AddGenre); }
        }

        private void RefreshList()
        {
            new Thread(() =>
            {
                using (var unitOfWork = UnitOfWorkFactory.CreateInstance())
                {
                    var selection = CurrentSelection;
                    var genres = unitOfWork.GenreRepository.GetAll();

                    CurrentSelection = null;
                    AllGenres = genres;
                    if (selection != null)
                    {
                        CurrentSelection = genres.FirstOrDefault(p => selection.Id == p.Id);
                    }
                }
            }).Start();
        }

        private void EditGenre()
        {
            if (CurrentGenre != null)
            {
                var result = new EditGenre() { DataContext = new EditGenreVM(CurrentGenre) }.ShowDialog();
                if (result == true)
                    RefreshList();
            }
        }

        private void AddGenre()
        {/*
            var result = new CreateGenre().ShowDialog();
            if (result == true)
                RefreshList();*/
        }


        #endregion

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
    }
}
