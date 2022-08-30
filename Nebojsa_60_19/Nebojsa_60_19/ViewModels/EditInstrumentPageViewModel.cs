using Nebojsa_60_19.Database;
using Nebojsa_60_19.Validators;
using Nebojsa_60_19.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nebojsa_60_19.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public class EditInstrumentPageViewModel : BaseViewModel
    {
        private int id;
        private string name;
        private string description;
        private string year;
        private string country;
        private bool isFormValid = true;
        private string descriptionError;
        private string yearError;
        private string countryError;

        public int Id { get => id; set 
            {
                SetProperty(ref id, value);
                var db = new Db();

                var instrument = db.Connection.Find<Instruments>(x => x.Id == this.id);
                Name = instrument.Name;
                Description = instrument.Description;
                Year = instrument.Year;
                Country = instrument.Country;
            } 
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set 
            {
                SetProperty(ref description, value);
                DescriptionError = Validator("Description");
            }
        }
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
        public string DescriptionError { get => descriptionError; set => SetProperty(ref descriptionError, value); }
        public string YearError { get => yearError; set => SetProperty(ref yearError, value); }
        public string CountryError { get => countryError; set => SetProperty(ref countryError, value); }
        public bool IsFormValid { get => isFormValid; set => SetProperty(ref isFormValid, value); }

        private string Validator(string property)
        {
            var validator = new EditInstrumentValidator();
            var result = validator.Validate(this);
            IsFormValid = result.IsValid;
            return result.Errors.FirstOrDefault(x => x.PropertyName == property)?.ErrorMessage;
        }

        public EditInstrumentPageViewModel()
        {
            EditCommand = new Command(EditInstrument);
        }

        private void EditInstrument()
        {
            var db = new Db();

            var instrument = db.Connection.Find<Instruments>(x => x.Id == this.id);
            instrument.Description = Description;
            instrument.Year = Year;
            instrument.Country = Country;

            db.Connection.Update(instrument);

            Shell.Current.GoToAsync("//" + nameof(InstrumentsPage) + "?refresh=" + 1);
        }

        public ICommand EditCommand { get; }
    }
}
