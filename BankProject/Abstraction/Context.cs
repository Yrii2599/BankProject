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
        /// <summary>
        /// Transition to state from the parameters
        /// </summary>
        /// <param name="state"> new state</param>
        public void TransitionTo(State state)
        {
             this._state = state;
            this._state.SetContext(this);
            Console.Clear();
            this._state.ShowMenu();
        }
        /// <summary>
        /// Show current menu
        /// </summary>
        public void GetMenu()
        {
            this._state.ShowMenu();
        }

     
    }
}
