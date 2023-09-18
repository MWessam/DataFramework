using Controller.Inputs;
using DataContainers;
namespace Controller
{
    public abstract class Controller
    {
        protected DataContainer _dataContainer;
        protected IInputHandler _inputHandler;
    }

    /*
     * Handle everything basically
     * Controls data, using input and provide the results to the view.
     * So every controller must have only ONE:
     * DataContainer,
     * InputHandler,
     * View
     * It should assign view methods to data events,
     * It should assign data transaction methods to input events.
     * 
     */
}