using CLientTestTask.Models;
using CLientTestTask.Repository.Bases;
using CLientTestTask.View;
using CLientTestTask.ViewModel.ComadsBase;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace CLientTestTask.ViewModel
{
    public class HomeViewModel : BaseViewModel<Home>, IDataErrorInfo
    {
        private readonly HomeEditPage homeEditPage;

        private string name;
        private int countApartament;
        private Street street;

        private CommandBase createCommandBase;
        private CommandBase updateCommandBase;

        public HomeViewModel(IRepository<Home> repository, HomeEditPage homeEditPage) : base(repository)
        {

            this.homeEditPage = homeEditPage;
            this.homeEditPage.DataContext = this;
        }



        public override Page EditGetPage
        {

            get { return homeEditPage; }
        }

        public string Error => throw new NotImplementedException();

        public Street Street
        {
            get
            {
                return street;

            }
            set
            {
                street = value;
                OnPropertyChange(nameof(Street));
            }
        }
        public int CountApartament
        {
            get
            {

                return countApartament;
            }
            set
            {
                countApartament = value;
                OnPropertyChange(nameof(CountApartament));

            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChange(nameof(Name));
            }
        }

        public override Home GetValue
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                Name = value.Name;
                Street = value.Street;
                CountApartament = value.CountApartments;

            }
        }

        public override CommandBase Create
        {
            get
            {
                return createCommandBase ?? (createCommandBase = new CommandBase(_ =>
                    GetValue = repository.CreateValue(new Home
                    {
                        Name = Name,
                        Street = Street,
                        StreetId = Street.Id,
                        CountApartments = CountApartament,
                        Id = 0
                    })
                ));
            }
        }



        public override CommandBase Update
        {
            get
            {
                return updateCommandBase ?? (updateCommandBase = new CommandBase(_ =>
                {
                    value.Name = Name;
                    value.Street = Street;
                    value.StreetId = Street.Id;
                    value.CountApartments = CountApartament;
                    Message = repository.Update(Id, value);
                }
                ));
            }
        }

        public string this[string columnName]
        {
            get
            {

                string error = base[columnName];


                switch (columnName)
                {

                    case nameof(GetValue):
                        var home = GetValue;
                        if (home.CountApartments <= 0 || home.CountApartments > 100)
                        {
                            error = "CountApartments  не может быть меньше или равным 0 или больше 100";
                            home.CountApartments = 1;
                        }

                        break;
                }

                return error;
            }


        }
    }
}
