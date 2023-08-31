using CLientTestTask.View;
using CLientTestTask.ViewModel.Bases;
using CLientTestTask.ViewModel.ComadsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CLientTestTask.ViewModel
{
    public  class NavigatedViewModel :  NotifyProperty
    {
        private Page _page;
        private HomePage _homePage;

        private HomeViewModel _homeVM;
        private StreetViewModelcs _streetVM;
        private LocalityViewModel _localytiVM;
        private ApartamentViewModel _apartamentVM;

        private CommandBase _commandHomePage;
        private CommandBase _commandStreetPage;
        private CommandBase _commandLocalytiPage;
        private CommandBase _commandApartamentPage;
       

        public NavigatedViewModel(HomePage homePage, HomeViewModel homeVM, StreetViewModelcs streetPage,
            LocalityViewModel localytiPage, ApartamentViewModel apartamentPage) 
        {
           
            _homeVM = homeVM;
            _streetVM = streetPage;
            _apartamentVM = apartamentPage;
            _localytiVM = localytiPage;
            _homePage = homePage;

            Navigated = new HomePage(_homeVM);
        }

        public Page Navigated
        {
            get { return _page; }
            set
            {
                _page = value;
                OnPropertyChange(nameof(Navigated));
            }
        }

        public CommandBase ButtonHomePage
        {
            get
            {
                return _commandHomePage ?? (_commandHomePage = new CommandBase( _ =>
                {
                    _homePage.DataContext = _homeVM;
                    Navigated = _homePage;
                }));
            }
        }

        public CommandBase ButtonStreetPage
        {
            get
            {
                return _commandStreetPage ?? (_commandStreetPage = new CommandBase( _ =>
                {
                    _homePage.DataContext = _streetVM;
                    Navigated = _homePage;
                }));
            }
        }

        public CommandBase ButtonLocalytuPage
        {
            get
            {
                return _commandLocalytiPage ?? (_commandLocalytiPage = new CommandBase( _ =>
                {

                    _homePage.DataContext = _localytiVM ;
                    Navigated = _homePage;
                }));
            }
        }

        public CommandBase ButtonApartamentPage
        {
            get
            {
                return _commandApartamentPage ?? (_commandApartamentPage = new CommandBase( _ =>
                {
                    _homePage.DataContext = _apartamentVM;
                    Navigated = _homePage;
                }));
            }
        }
 

    }
}
