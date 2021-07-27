namespace BankProject.Models
{
    class PersonalInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecretWord { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public PersonalInfo()
        {
            Name = "";
            Surname = "";
            SecretWord = "";
        }
    }
}
