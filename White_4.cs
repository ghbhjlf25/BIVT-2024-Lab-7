using System;
using System.Linq;
namespace Lab_7
{
    public class White_4
    {
        public class Human
        {
            protected string _name;
            protected string _surname;

            public string Name => _name;
            public string Surname => _surname;

            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}");
            }
        }

        public class Participant : Human
        {
            private double[] _scores;
            private static int _count;

            public static int Count => _count;

            static Participant()
            {
                _count = 0;
            }

            public double[] Scores
            {
                get
                {
                    if (_scores == null) return default(double[]);
                    double[] copy = new double[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);
                    return copy;
                }
            }
            public double TotalScore => _scores?.Sum() ?? 0;

            public Participant(string name, string surname) : base(name, surname)
            {
                _scores = new double[0];
                _count++;
            }

            public void PlayMatch(double result)
            {
                if (_scores == null) return;
                double[] newScores = new double[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    newScores[i] = _scores[i];
                }
                newScores[newScores.Length - 1] = result;
                _scores = newScores;
            }

            public static void Sort(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;
                for (int i = 0; i < participants.Length - 1; i++)
                {
                    for (int j = 0; j < participants.Length - 1 - i; j++)
                    {
                        if (participants[j].TotalScore < participants[j + 1].TotalScore)
                        {
                            Participant temp = participants[j];
                            participants[j] = participants[j + 1];
                            participants[j + 1] = temp;
                        }
                    }
                }
            }

            public override void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, Общий результат: {TotalScore}");
            }
        }
    }
}