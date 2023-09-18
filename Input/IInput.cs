using System;
using System.Threading.Tasks;

namespace Controller
{
    namespace Inputs
    {
        public enum InputState
        {
            Triggered, Held, Released, Inactive
        }
        public interface IInput
        {
            event EventHandler<EventArgs> OnInputTriggered;
            event EventHandler<EventArgs> OnInputHold;
            event EventHandler<EventArgs> OnInputReleased;
            void TriggerInput();
            Task HoldInput();
            void ReleaseInput();
            InputState State { get; }
            
        }
        public interface IInputHandler
        {
            void SendInputCommand(string key);
            void SetInput(string key, IInput input);
            void SetInputEventOnTriggered(string key, EventHandler<EventArgs> inputEvent);
            void SetInputEventOnHold(string key, EventHandler<EventArgs> inputEvent);
            void SetInputEventOnKeyReleased(string key, EventHandler<EventArgs> inputEvent);
        }

        internal interface IInputChecker
        {

        }

        internal interface IInputProccessor
        {

        }
    }



    /*
     * We are checking for input "Every Frame"
     * So the input handler should have a method to check for input, but let's extract it to it's seperate interface.
     * So we should have an Input Checker, it will check whether any input has been executed per frame.
     * It will then pass it into an InputProcessor, to process that input and fire the corresponding event.
     * And the input handler will be the mediator between them, it's going to be the facade.
     */
}