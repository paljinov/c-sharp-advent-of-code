namespace App.Tasks.Year2020.Day4
{
    public class Passport
    {
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }

        public Passport()
        {
            BirthYear = string.Empty;
            IssueYear = string.Empty;
            ExpirationYear = string.Empty;
            Height = string.Empty;
            HairColor = string.Empty;
            EyeColor = string.Empty;
            PassportId = string.Empty;
        }
    }
}
