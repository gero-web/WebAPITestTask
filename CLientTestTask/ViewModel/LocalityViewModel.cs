using CLientTestTask.Models;
using CLientTestTask.Repository.Bases;
using CLientTestTask.View;
using CLientTestTask.ViewModel.ComadsBase;
using System.ComponentModel;
using System.Windows.Controls;

namespace CLientTestTask.ViewModel
{
    public class LocalityViewModel : BaseViewModel<Locality>
    {
        private readonly LocalytiEditPage _localityEditPAge;

        private string name;



        private CommandBase createCommandBase;
        private CommandBase updateCommandBase;
        public LocalityViewModel(IRepository<Locality> repository, LocalytiEditPage localytiEditPage) : base(repository)
        {
            _localityEditPAge = localytiEditPage;
            _localityEditPAge.DataContext = this;
        }



        public override Page EditGetPage
        {
            get { return _localityEditPAge; }
        }

        public override Locality GetValue
        {
            get { return value; }
            set
            {
                this.value = value;
                Name = value.Name;
                OnPropertyChange(nameof(GetValue));
            }
        }

        public override CommandBase Create
        {
            get
            {
                return createCommandBase ?? (createCommandBase = new CommandBase(_ =>
                    {
                        GetValue = repository.CreateValue(new Locality { Name = Name });
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
                    this.value.Name = value.Name;
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
                this.name = value;
                OnPropertyChange(nameof(Name));
            }
        }
    }
}
