namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> membersByid = new Dictionary<int, Member>();
        private Dictionary<int, Trainer> trainersById = new Dictionary<int, Trainer>();

        public void AddMember(Member member)
        {
            if (membersByid.ContainsKey(member.Id))
            {
                throw new ArgumentException();
            }

            membersByid.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (trainersById.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            trainersById.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!membersByid.ContainsKey(member.Id))
            {
                membersByid.Add(member.Id, member);
            }
            else
            {
                if (!trainersById.ContainsKey(trainer.Id) || !(membersByid[member.Id].Trainer is null))
                {
                    throw new ArgumentException();
                }
                else
                {
                    membersByid[member.Id].Trainer = trainer;
                }
            }
        }

        public bool Contains(Member member)
            => membersByid.ContainsKey(member.Id);

        public bool Contains(Trainer trainer)
            => trainersById.ContainsKey(trainer.Id);

        public Trainer FireTrainer(int id)
        {
            if (!trainersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var trainer = trainersById[id];
            trainersById.Remove(id);
            membersByid.Values.Where(m => m.Trainer.Equals(trainer)).ToList().ForEach(m => m.Trainer = null);

            return trainer;
        }

        public Member RemoveMember(int id)
        {
            if (!membersByid.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var member = membersByid[id];
            membersByid.Remove(id);
            trainersById.Values.Where(t => t.Members.Contains(member)).ToList().ForEach(t => t.Members.Remove(member));

            return member;
        }

        public int MemberCount { get => membersByid.Count; }
        public int TrainerCount { get => trainersById.Count; }

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
            => membersByid.Values
            .OrderBy(m => m.RegistrationDate)
            .ThenByDescending(m => m.Name);

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
            => trainersById.Values
            .OrderBy(t => t.Popularity);

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
            => trainer.Members
            .OrderBy(m => m.RegistrationDate)
            .ThenBy(m => m.Name);

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
            => membersByid.Values
            .Where(m => m.Trainer.Popularity >= lo && m.Trainer.Popularity <= hi)
            .OrderBy(m => m.Visits);

        public Dictionary<Trainer, HashSet<Member>> 
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            var dict = new Dictionary<Trainer, HashSet<Member>>();

            foreach (var trainer in trainersById.Values)
            {
                dict.Add(trainer, new HashSet<Member>());
                dict[trainer] = trainer.Members;
            }

            return dict;
        }
    }
}