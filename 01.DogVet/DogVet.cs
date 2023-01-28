namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById = new Dictionary<string, Dog>();
        private Dictionary<string, Owner> ownersById = new Dictionary<string, Owner>();

        public int Size { get => dogsById.Keys.Count; }
        public void AddDog(Dog dog, Owner owner)
        {
            if (dogsById.ContainsKey(dog.Id))
            {
                throw new ArgumentException();
            }
            if (owner.Dogs.Any(d => d.Name == dog.Name))
            {
                throw new ArgumentException();
            }

            owner.Dogs.Add(dog);
        }

        public bool Contains(Dog dog)
            => dogsById.ContainsKey(dog.Id);

        public Dog GetDog(string name, string ownerId)
        {
            throw new NotImplementedException();
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            throw new NotImplementedException();
        }

        public void Vaccinate(string name, string ownerId)
        {
            throw new NotImplementedException();
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            throw new NotImplementedException();
        }
    }
}