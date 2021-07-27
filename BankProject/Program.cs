using BankProject.Abstraction;
using BankProject.States;

namespace BankProject
{
    class Program
    {
        static void Main(string[] args)
        {

            StateContext context = new StateContext(new AutorizeMenu());
            context.GetMenu();

        }
    }
}
