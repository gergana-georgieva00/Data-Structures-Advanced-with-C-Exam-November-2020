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
            dogsById.Add(dog.Id, dog);
        }

        public bool Contains(Dog dog)
            => dogsById.ContainsKey(dog.Id);

        public Dog GetDog(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            var owner = ownersById[ownerId];
            if (owner is null || owner.Dogs.Find(d => d.Name == name) is null)
            {
                throw new ArgumentException();
            }

            return owner.Dogs.Find(d => d.Name == name);
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }
            var owner = ownersById[ownerId];
            if (owner is null || owner.Dogs.Find(d => d.Name == name) is null)
            {
                throw new ArgumentException();
            }

            var dog = owner.Dogs.Find(d => d.Name == name);
            owner.Dogs.Remove(dog);
            dogsById.Remove(dog.Id);

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return ownersById[ownerId].Dogs;
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            if (dogsById.Keys.Count == 0)
            {
                throw new ArgumentException();
            }

            return dogsById.Values.Where(d => d.Breed == breed);
        }

        public void Vaccinate(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }
            if (!ownersById[ownerId].Dogs.Any(d => d.Name == name))
            {
                throw new ArgumentException();
            }

            ownersById[ownerId].Dogs.Find(d => d.Name == name).Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }
            if (!ownersById[ownerId].Dogs.Any(d => d.Name == oldName))
            {
                throw new ArgumentException();
            }

            ownersById[ownerId].Dogs.Find(d => d.Name == oldName).Name = newName;
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            if (dogsById.Count == 0)
            {
                throw new ArgumentException();
            }

            return dogsById.Values.Where(d => d.Age == age);
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