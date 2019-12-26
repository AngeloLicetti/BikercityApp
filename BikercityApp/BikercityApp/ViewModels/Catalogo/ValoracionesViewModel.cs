using BikercityApp.Extensions;
using BikercityApp.Models.Catalogo;
using BikercityApp.Services.Catalogo;
using BikercityApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikercityApp.ViewModels.Catalogo
{
    class ValoracionesViewModel : ViewModelBase
    {
        List<ComentarioModel> listmodel;
        private ICatalogoService _catalogoService;

        public ValoracionesViewModel(
                                ICatalogoService catalogoService
                             )
        {
            _catalogoService = catalogoService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;
                listmodel = navigationData as List<ComentarioModel>;
                ResenasList = listmodel.ToObservableCollection();
                double sumatotal = 0;
                foreach (var item in ResenasList)
                {
                    if (item.calificacion == 1)
                        UnaEstrella++;
                    if (item.calificacion == 2)
                        DosEstrellas++;
                    if (item.calificacion == 3)
                        TresEstrellas++;
                    if (item.calificacion == 4)
                        CuatroEstrellas++;
                    if (item.calificacion == 5)
                        CincoEstrellas++;
                    sumatotal += item.calificacion;
                }

                CalificacionTotal = sumatotal / ResenasList.Count;
                IsBusy = false;
            }
            catch (Exception ex)
            {
            }
			
        }

        public ICommand OrdenarTodoCommand => new Command(async () => await OrdenarTodo());

        private async Task OrdenarTodo()
        {
            ResenasList =  listmodel.ToObservableCollection();
        }

        public ICommand OrdernarMenorMayorCommand => new Command(async () => await OrdernarMenorMayor());

        private async Task OrdernarMenorMayor()
        {
            ResenasList.OrderBy(l => l.calificacion);
        }

        public ICommand OrdernarMayorMenorCommand => new Command(async () => await OrdernarMayorMenor());

        private async Task OrdernarMayorMenor()
        {
            ResenasList.OrderByDescending(l => l.calificacion);
        }


        private double _calificacionTotal;
        public double CalificacionTotal
        {
            get { return _calificacionTotal; }
            set
            {
                _calificacionTotal = value;
                RaisePropertyChanged(() => CalificacionTotal);
            }
        }
        private ObservableCollection<ComentarioModel> _resenasList;
        public ObservableCollection<ComentarioModel> ResenasList
        {
            get { return _resenasList; }
            set
            {
                _resenasList = value;
                RaisePropertyChanged(() => ResenasList);
            }
        }
        private int _unaEstrella;
        public int UnaEstrella
        {
            get { return _unaEstrella; }
            set
            {
                _unaEstrella = value;
                RaisePropertyChanged(() => UnaEstrella);
            }
        }
        private int _dosEstrellas;
        public int DosEstrellas
        {
            get { return _dosEstrellas; }
            set
            {
                _dosEstrellas = value;
                RaisePropertyChanged(() => DosEstrellas);
            }
        }
        private int _tresEstrellas;
        public int TresEstrellas
        {
            get { return _tresEstrellas; }
            set
            {
                _tresEstrellas = value;
                RaisePropertyChanged(() => TresEstrellas);
            }
        }
        private int _cuatroEstrellas;
        public int CuatroEstrellas
        {
            get { return _cuatroEstrellas; }
            set
            {
                _cuatroEstrellas = value;
                RaisePropertyChanged(() => CuatroEstrellas);
            }
        }
        private int _cincoEstrellas;
        public int CincoEstrellas
        {
            get { return _cincoEstrellas; }
            set
            {
                _cincoEstrellas = value;
                RaisePropertyChanged(() => CincoEstrellas);
            }
        }
    }
}
