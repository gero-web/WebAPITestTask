using CLientTestTask.Models;
using CLientTestTask.Repository.Bases;
using CLientTestTask.View;
using CLientTestTask.ViewModel.ComadsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CLientTestTask.ViewModel
{
    public class StreetViewModelcs : BaseViewModel<Street>
    {
        private readonly StreetEditPage _streetEditPage;

        private string name;
        private Locality locality;

        private CommandBase createCommandBase;
        private CommandBase updateCommandBase;

        public StreetViewModelcs(IRepository<Street> repository, StreetEditPage streetEditPage) : base(repository)
        {
            _streetEditPage = streetEditPage;
            _streetEditPage.DataContext = this;
        }

        public override Page EditGetPage
        {
            get
            {
                return _streetEditPage;
            }
        }

        public override Street GetValue
        {
            get { return value; }
            set
            {

                this.value = value;
                Name = value.Name;
                Locality = value.Locality;
                OnPropertyChange(nameof(GetValue));
            }
        }

        public override CommandBase Create
        {
            get
            {
                return createCommandBase ?? (createCommandBase = new CommandBase(_ =>
                    GetValue = repository.CreateValue(new Street()
                    {
                        Name = Name,
                        Locality = Locality,
                        LocalityId = Locality.Id

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
                    value.Locality = Locality;
                    value.Name = Name;
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
        public Locality Locality
        {
            get { return locality; }
            set
            {
                locality = value;
                OnPropertyChange(nameof(Locality));
            }
        }
    }
}
