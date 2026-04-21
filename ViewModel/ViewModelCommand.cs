using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Boletera.ViewModel
{
    public class ViewModelCommand : ICommand

    {
        //representa el metodo que se ejecuta cuando se llama a execute
        private readonly Action<object> _executeAction;

        //representa una funcion que devuelve bool y nos dice si-
        //-el comando se puede ejecutar
        private readonly Predicate<object> _canExecuteAction;

        //constructores 
        // este constructor permite ejecutar una accion sin revision previa
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        //este constructo, realiza una accion ejecutando una revision previa
        public ViewModelCommand(Action<object> executeAction,
            Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        //este metodo me permite ejecutar el evento
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //indicar si se puede ejecutar el comando o no 
        //si _canExecute es nulo, entonces devuelve true
        //(el comando sempre se puede ejecutar)
        //si no es null entonces se kkana al oreducadi oara que decida
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true :
                _canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }
}
