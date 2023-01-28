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
            if (owner.DogsByNames.ContainsKey(dog.Name))
            {
                throw new ArgumentException();
            }

            owner.DogsByNames.Add(dog.Name, dog);
            dog.Owner = owner;
            dogsById.Add(dog.Id, dog);

            if (!ownersById.ContainsKey(owner.Id))
            {
                ownersById.Add(owner.Id, owner);
            }
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
            if (owner != null && owner.DogsByNames.TryGetValue(name, out var dog))
            {
                return dog;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }
            var owner = ownersById[ownerId];
            if (owner is null || !owner.DogsByNames.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            var dog = owner.DogsByNames[name];
            owner.DogsByNames.Remove(name);
            dogsById.Remove(dog.Id);

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return ownersById[ownerId].DogsByNames.Values;
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
            if (!ownersById[ownerId].DogsByNames.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            ownersById[ownerId].DogsByNames[name].Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }
            if (!ownersById[ownerId].DogsByNames.ContainsKey(oldName))
            {
                throw new ArgumentException();
            }

            var dog = ownersById[ownerId].DogsByNames[oldName];
            dog.Name = newName;
            ownersById[ownerId].DogsByNames.Remove(oldName);
            //ownersById[ownerId].DogsByNames[oldName].Name = newName;
            ownersById[ownerId].DogsByNames.Add(newName, dog);
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            if ((dogsById.Values.Where(d => d.Age == age).ToList().Count) == 0)
            {
                throw new ArgumentException();
            }

            return dogsById.Values.Where(d => d.Age == age);
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
            => dogsById.Values.Where(d => d.Age >= lo && d.Age <= hi);

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
            => dogsById.Values
            .OrderBy(d => d.Age)
            .ThenBy(d => d.Name)
            .ThenBy(d => d.Owner.Name);
    }
}