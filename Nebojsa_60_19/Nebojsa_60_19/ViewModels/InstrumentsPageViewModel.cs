using Nebojsa_60_19.Database;
using Nebojsa_60_19.Models;
using Nebojsa_60_19.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nebojsa_60_19.ViewModels
{
    [QueryProperty(nameof(Refresh), "refresh")]
    public class InstrumentsPageViewModel : BaseViewModel
    {
        private string responseText;
        private string responseColor;
        private bool isVisible = false;
        private ObservableCollection<TypesOfInstruments> types;
        private TypesOfInstruments selectedType;
        private string search;
        private ObservableCollection<InstrumentsRead> instruments;
        private bool isRefreshing = false;
        private bool noInstruments = false;
        private int refresh;


        public string ResponseText { get => responseText; set => SetProperty(ref responseText, value); }
        public string ResponseColor { get => responseColor; set => SetProperty(ref responseColor, value); }
        public bool IsVisible { get => isVisible; set => SetProperty(ref isVisible, value); }
        public ObservableCollection<TypesOfInstruments> Types { get => types; set => SetProperty(ref types, value); }
        public TypesOfInstruments SelectedType { get => selectedType; 
            set 
            {
                SetProperty(ref selectedType, value);
                var db = new Db();
                if (Search == null)
                {
                    var instruments = db.Connection.Query<InstrumentsRead>("SELECT i.Id, i.Name, i.Description, i.Country, i.Year, ty.Name as Type FROM Instruments i INNER JOIN TypesOfInstruments ty ON i.TypeId = ty.Id WHERE ty.Id =" + SelectedType.Id); 
                    Instruments = new ObservableCollection<InstrumentsRead>(instruments);
                    NoInstruments = instruments.Count == 0;
                }
                else
                {
                    var instruments = db.Connection.Query<InstrumentsRead>("SELECT i.Id, i.Name, i.Description, i.Country, i.Year, ty.Name as Type FROM Instruments i INNER JOIN TypesOfInstruments ty ON i.TypeId = ty.Id WHERE i.Name LIKE '%" + Search + "%' AND ty.Id =" + SelectedType.Id);
                    Instruments = new ObservableCollection<InstrumentsRead>(instruments);
                    NoInstruments = instruments.Count == 0;
                }
            } 
        }
        public string Search { get => search;
            set 
            {
                SetProperty(ref search, value);
                var db = new Db();
                if (SelectedType == null)
                {
                    var instruments = db.Connection.Query<InstrumentsRead>("SELECT i.Id, i.Name, i.Description, i.Country, i.Year, ty.Name as Type FROM Instruments i INNER JOIN TypesOfInstruments ty ON i.TypeId = ty.Id WHERE i.Name LIKE '%"+Search+"%'");
                    Instruments = new ObservableCollection<InstrumentsRead>(instruments);
                    NoInstruments = instruments.Count == 0;
                }
                else
                {
                    var instruments = db.Connection.Query<InstrumentsRead>("SELECT i.Id, i.Name, i.Description, i.Country, i.Year, ty.Name as Type FROM Instruments i INNER JOIN TypesOfInstruments ty ON i.TypeId = ty.Id WHERE i.Name LIKE '%" + Search + "%' AND ty.Id ="+ SelectedType.Id);
                    Instruments = new ObservableCollection<InstrumentsRead>(instruments);
                    NoInstruments = instruments.Count == 0;
                }
            } 
        }
        public ObservableCollection<InstrumentsRead> Instruments { get => instruments; set => SetProperty(ref instruments, value); }
        public bool IsRefreshing { get => isRefreshing; set => SetProperty(ref isRefreshing, value); }
        public bool NoInstruments { get => noInstruments; set => SetProperty(ref noInstruments, value); }
        public int Refresh { get => refresh; set 
            {
                SetProperty(ref refresh, value);
                var db = new Db();
                LoadInstruments(db);
            }
        }


        public InstrumentsPageViewModel()
        {
            var db = new Db();

            AddInstrument = new Command(() =>
            {
                Shell.Current.GoToAsync(nameof(AddInstrumentPage));
            });
            AddType = new Command(() =>
            {
                Shell.Current.GoToAsync(nameof(AddTypePage));
            });
            HideCommand = new Command(() => 
            {
                IsVisible = false;
            });
            RefreshCommand = new Command(() => 
            {   
                Search = null;
                LoadInstruments(db);
            });
            DeleteCommand = new Command(DeleteInstrument);
            EditCommand = new Command(EditInstrument);

            var types = db.Connection.Table<TypesOfInstruments>().ToList();
            Types = new ObservableCollection<TypesOfInstruments>(types);

            LoadInstruments(db);
        }

        private void EditInstrument(object item)
        {
            var db = new Db();
            var instrument = (InstrumentsRead)item;

            Shell.Current.GoToAsync(nameof(EditInstrumentPage) + "?id=" + instrument.Id);
        }

        private void DeleteInstrument(object item)
        {
            var db = new Db();
            var instrument = (InstrumentsRead)item;

            var result = db.Connection.Delete<Instruments>(instrument.Id);
            if(result == 1)
            {
                Instruments.Remove(instrument);
                ResponseColor = "Green";
                ResponseText = "Succes delete";
                IsVisible = true;
            }
            else
            {
                ResponseColor = "Red";
                ResponseText = "Error delete";
                IsVisible = true;
            }
            NoInstruments = Instruments.Count == 0;
        }

        private void LoadInstruments(Db db)
        {
            var instruments = db.Connection.Query<InstrumentsRead>("SELECT i.Id, i.Name, i.Description, i.Country, i.Year, ty.Name as Type FROM Instruments i INNER JOIN TypesOfInstruments ty ON i.TypeId = ty.Id");
            Instruments = new ObservableCollection<InstrumentsRead>(instruments);

            IsRefreshing = false;

            NoInstruments = instruments.Count == 0;
        }

        public ICommand AddInstrument { get; }
        public ICommand AddType { get; }
        public ICommand HideCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        
    }
}
