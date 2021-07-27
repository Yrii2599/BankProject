using System;
using BankProject.Models;

namespace BankProject.Abstraction
{
    class StateContext
    {
        private State _state = null;
        public User User { get; set; }

        public StateContext(State state)
        {
            this.TransitionTo(state);
        }

        public void TransitionTo(State state)
        {
             this._state = state;
            this._state.SetContext(this);
            Console.Clear();
            this._state.ShowMenu();
        }

        public void GetMenu()
        {
            this._state.ShowMenu();
        }

     
    }
}
