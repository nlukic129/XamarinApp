using Nebojsa_60_19.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nebojsa_60_19.ViewModels
{
    public class AddTypePageViewModel : BaseViewModel
    {
        private ObservableCollection<TypesOfInstruments> types;
        private TypesOfInstruments type;
        private bool _isFormValid;
        private string _name;
        private string _nameError;
        private bool _hasNameError;

        public bool HasNameError
        {
            get => _hasNameError;
            set => SetProperty(ref _hasNameError, value);
        }

        public ObservableCollection<TypesOfInstruments> Types
        {
            get => types;
            set => SetProperty(ref types, value);
        }

        public TypesOfInstruments SelectedTypesOfInstruments
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public bool IsFormValid
        {
            get => _isFormValid;
            set => SetProperty(ref _isFormValid, value);
        }

        public string NameError
        {
            get => _nameError;
            set
            {
                SetProperty(ref _nameError, value);
                IsFormValid = value == null;
                HasNameError = value != null;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);

                if (string.IsNullOrEmpty(value))
                {
                    NameError = "Name is required";
                    return;
                }

                if (string.IsNullOrEmpty(value.Trim()))
                {
                    NameError = "Whitespaces only are not allowed";
                    return;
                }

                if (value.Trim().Length < 3)
                {
                    NameError = "Name must be at least 3 characters long";
                    return;
                }

                var name = value.Trim();
                var category = Db.Connection.Find<TypesOfInstruments>(x => x.Name == name);

                if (category != null)
                {
                    NameError = "Category name is already in use";
                    return;
                }

                NameError = null;
            }
        }
        private Db Db { get; }
        public ICommand AddTypeCommand { get; }

        public AddTypePageViewModel()
        {
            Db = new Db();

            var dbTypes = Db.Connection.Table<TypesOfInstruments>().ToList();

            Types = new ObservableCollection<TypesOfInstruments>(dbTypes);
            AddTypeCommand = new Command(AddType);
        }

        private void AddType()
        {
            var typeToAdd = new TypesOfInstruments
            {
                Name = this.Name,
            };

            Db.Connection.Insert(typeToAdd);

            Types.Add(typeToAdd);
            this.Name = "";
        }
    }
}
