namespace KleinMessage.Models
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string IDApi { get; set; }

        public string Initials => InitialsSubstring();

        private string InitialsSubstring()
        {
            string output = "";

            if(Name != "")
            {
                output = Name.Substring(0, 1);
            }

            return output;           
        }
       
    }
}
