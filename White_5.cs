using System;
using System.Linq;
namespace Lab_7
{
    public class White_5
    {
        public struct Match
        {
            private int _goals;
            private int _misses;

            public int Goals => _goals;
            public int Misses => _misses;
            public int Difference => Goals - Misses;
            public int Score => Goals > Misses ? 3 : Goals == Misses ? 1 : 0;

            public Match(int goals, int misses)
            {
                if (goals < 0 || misses < 0)
                {
                    _goals = 0;
                    _misses = 0;
                }
                else
                {
                    _goals = goals;
                    _misses = misses;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Забито: {_goals}, Пропущено: {_misses}, Очки: {Score}");
            }
        }

        public abstract class Team
        {
            protected string _name;
            protected Match[] _matches;

            public string Name => _name;
            public Match[] Matches => _matches;
            public int TotalScore
            {
                get
                {
                    if (_matches == null || _matches.Length == 0) return 0;
                    int total = 0;
                    foreach (var match in _matches)
                    {
                        total += match.Score;
                    }
                    return total;
                }
            }
            public int TotalDifference
            {
                get
                {
                    if (_matches == null || _matches.Length == 0) return 0;
                    int total = 0;
                    foreach (var match in _matches)
                    {
                        total += match.Difference;
                    }
                    return total;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _matches = new Match[0];
            }

            public virtual void PlayMatch(int goals, int misses)
            {
                if (_matches == null) return;
                Match[] newMatches = new Match[_matches.Length + 1];
                for (int i = 0; i < _matches.Length; i++)
                {
                    newMatches[i] = _matches[i];
                }
                newMatches[newMatches.Length - 1] = new Match(goals, misses);
                _matches = newMatches;
            }

            public static void SortTeams(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if ((teams[j].TotalScore < teams[j + 1].TotalScore) ||
                            (teams[j].TotalScore == teams[j + 1].TotalScore && teams[j].TotalDifference < teams[j + 1].TotalDifference))
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"Команда: {_name}, Очки: {TotalScore}, Разность голов: {TotalDifference}");
            }
        }

        public class ManTeam : Team
        {
            private ManTeam _derby;

            public ManTeam Derby => _derby;

            public ManTeam(string name, ManTeam derby = null) : base(name)
            {
                _derby = derby;
            }

            public void PlayMatch(int goals, int misses, ManTeam team = null)
            {
                base.PlayMatch(goals, misses);
                if (team != null && team == _derby)
                {
                    base.PlayMatch(goals + 1, misses);
                }
            }
        }

        public class WomanTeam : Team
        {
            private int[] _penalties;

            public int[] Penalties => _penalties;
            public int TotalPenalties => _penalties?.Sum() ?? 0;

            public WomanTeam(string name) : base(name)
            {
                _penalties = new int[0];
            }

            public override void PlayMatch(int goals, int misses)
            {
                base.PlayMatch(goals, misses);
                
                if (misses > goals)
                {
                    int penalty = misses - goals;
                    int[] newPenalties = new int[_penalties.Length + 1];
                    for (int i = 0; i < _penalties.Length; i++)
                    {
                        newPenalties[i] = _penalties[i];
                    }
                    newPenalties[newPenalties.Length - 1] = penalty;
                    _penalties = newPenalties;
                }
            }

            public new void Print()
            {
                base.Print();
                Console.WriteLine($"Штрафы: {TotalPenalties}");
            }
        }
    }
}