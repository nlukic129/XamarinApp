using Nebojsa_60_19.Database;
using Nebojsa_60_19.Validators;
using Nebojsa_60_19.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nebojsa_60_19.ViewModels
{
    public class AddInstrumentPageViewModel : BaseViewModel
    {
        private string name;
        private string description;
        private TypesOfInstruments type;
        private string year;
        private string country;
        private bool formValid;
        private string nameError;
        private string descriptionError;
        private string yearError;
        private string countryError;
        private ObservableCollection<TypesOfInstruments> types;

        public string Name 
        { 
            get => name; 
            set
            {
                SetProperty(ref name, value);
                NameError = Validator("Name");
            }
        }
        public string Description { get => description; set
            {
                SetProperty(ref description, value);
                DescriptionError = Validator("Description");
            }
        }
        public TypesOfInstruments SelectedTypesOfInstruments { get => type; set => SetProperty(ref type, value); }
        public string Year { get => year; set
            {
                SetProperty(ref year, value);
                YearError = Validator("Year");
            }
        }
        public string Country { get => country; set
            {
                SetProperty(ref country, value);
                CountryError = Validator("Country");
            }
        }
        public string NameError { get => nameError; set => SetProperty(ref nameError, value); }
        public string DescriptionError { get => descriptionError; set => SetProperty(ref descriptionError, value); }
        public string YearError { get => yearError; set => SetProperty(ref yearError, value); }
        public string CountryError { get => countryError; set => SetProperty(ref countryError, value); }
        public ObservableCollection<TypesOfInstruments> Types { get => types; set => SetProperty(ref types, value); }
        public bool FormValid { get => formValid; set => SetProperty(ref formValid, value); }

        private string Validator(string property)
        {
            var db = new Db();
            var validator = new AddInstrmentValidator(db);

            var result = validator.Validate(this);
            FormValid = result.IsValid;

            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }

        public AddInstrumentPageViewModel()
        {
            var db = new Db();
            var dbTypes = db.Connection.Table<TypesOfInstruments>().ToList();

            Types = new ObservableCollection<TypesOfInstruments>(dbTypes);
            AddInstrumentCommand = new Command(AddInstrument);
        }

        private void AddInstrument()
        {
            var db = new Db();
            var instrument = new Instruments
            {
                Name = this.Name,
                Year = this.Year,
                Country = this.Country,
                TypeId = this.SelectedTypesOfInstruments.Id,
                Description = this.Description,
            };

            var result = db.Connection.Insert(instrument);

            if (result == 1)
            {
                Shell.Current.GoToAsync("//" + nameof(InstrumentsPage) + "?refresh=" + 1);
            }    

        }

        public ICommand AddInstrumentCommand { get; }
    }
    
}
