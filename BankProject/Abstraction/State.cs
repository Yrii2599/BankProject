namespace BankProject.Abstraction
{
    abstract class State
    {
        protected StateContext _context;

        public void SetContext(StateContext context)
        {
            this._context = context;
        }

        public abstract void ShowMenu();
    }
}
