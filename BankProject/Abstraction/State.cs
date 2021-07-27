namespace BankProject.Abstraction
{
    abstract class State
    {
        protected StateContext _context;
        /// <summary>
        /// Create new context
        /// </summary>
        /// <param name="context"> new instance of context </param>
        public void SetContext(StateContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Presents custom menus
        /// </summary>
        public abstract void ShowMenu();
    }
}
