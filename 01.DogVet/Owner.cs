using System.Collections.Generic;

namespace _01.DogVet
{
    public class Owner
    {
        public Owner(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            //Dogs = new List<Dog>();
            //DogsNames = new List<string>();
            DogsByNames = new Dictionary<string, Dog>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        //public List<Dog> Dogs { get; set; }
        //public List<string> DogsNames { get; set; }
        public Dictionary<string, Dog> DogsByNames { get; set; }
    }
}