namespace BankProject.Models
{
  public  class Card
    {
        public int Id { get; set; }
        
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public BaseAccount BankAccount { get; set; }
    }
}
