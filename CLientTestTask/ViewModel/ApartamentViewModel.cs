using CLientTestTask.Models;
using CLientTestTask.View;
using CLientTestTask.ViewModel.ComadsBase;
using System.Windows.Controls;

namespace CLientTestTask.ViewModel
{
    public class ApartamentViewModel : BaseViewModel<Apartment>
    {
        private readonly ApartamentEditPage _apartamentEditPage;

        private string name;
        private string fioOnwer;
        private Home home;


        private CommandBase createCommandBase;
        private CommandBase updateCommandBase;
        public ApartamentViewModel(Repository.Bases.IRepository<Apartment> repository, ApartamentEditPage apartamentEditPage) : base(repository)
        {
            _apartamentEditPage = apartamentEditPage;
            _apartamentEditPage.DataContext = this;
        }

        public override Page EditGetPage
        {
            get { return _apartamentEditPage; }
        }

        public override Apartment GetValue
        {
            get { return value; }
            set
            {
                this.value = value;

                Name = value.Name;
                FioOnwer = value.FIOOnwer;
                Homes = value.Home;

                OnPropertyChange(nameof(GetValue));
            }
        }

        public override CommandBase Create
        {
            get
            {
                return createCommandBase ?? (createCommandBase = new CommandBase(_ =>
                    {
                        GetValue = repository.CreateValue(new Apartment
                        {
                            Name = Name,
                            FIOOnwer = FioOnwer,
                            Home = Homes,
                            HomeId = Homes.Id
                        });
                    }
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
                        value.FIOOnwer = FioOnwer;
                        value.Home = Homes;
                        value.HomeId = Homes.Id;
                        Message = repository.Update(Id, value);
                    }
                ));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChange(nameof(Name));
            }
        }
        public string FioOnwer
        {
            get { return fioOnwer; }
            set
            {
                fioOnwer = value;
                OnPropertyChange(nameof(FioOnwer));
            }
        }
        public Home Homes
        {
            get { return home; }
            set
            {
                home = value;
                OnPropertyChange(nameof(Homes));
            }
        }
    }
}
