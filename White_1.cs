using System;
using System.Linq;
namespace Lab_7
{
    public class White_1
    {
        public class Participant
        {
            private string _surname;
            private string _club;
            private double _firstJump;
            private double _secondJump;
            
            private static double _standard = 5;
            private static int _activeCount = 0;
            private static int _disqualifiedCount = 0;
            
            public static int Jumpers => _activeCount;
            public static int Disqualified => _disqualifiedCount;
            
            static Participant()
            {
                _standard = 5;
                _activeCount = 0;
                _disqualifiedCount = 0;
            }
            
            public string Surname => _surname;
            public string Club => _club;
            public double FirstJump => _firstJump;
            public double SecondJump => _secondJump;
            public double JumpSum => _firstJump + _secondJump;

            public Participant(string surname, string club)
            {
                _surname = surname;
                _club = club;
                _firstJump = 0;
                _secondJump = 0;
                _activeCount++;
            }

            public void Jump(double result)
            {
                if (_firstJump == 0)
                {
                    _firstJump = result;
                }
                else if (_secondJump == 0)
                {
                    _secondJump = result;
                }
            }

            public static void Sort(Participant[] participants)
            {
                if (participants == null || participants.Length == 0) return;

                for (int i = 0; i < participants.Length - 1; i++)
                {
                    for (int j = 0; j < participants.Length - i - 1; j++)
                    {
                        if (participants[j].JumpSum < participants[j + 1].JumpSum)
                        {
                            Participant temp = participants[j];
                            participants[j] = participants[j + 1];
                            participants[j + 1] = temp;
                        }
                    }
                }
            }

            public static void Disqualify(ref Participant[] participants)
            {
                if (participants == null) return;
                
                var qualified = participants.Where(p => p._firstJump >= _standard && p._secondJump >= _standard).ToArray();
                _disqualifiedCount += participants.Length - qualified.Length;
                _activeCount = qualified.Length;
                participants = qualified;
            }

            public void Print()
            {
                Console.WriteLine($"Фамилия: {_surname}, Клуб: {_club}, " +
                                $"Первый прыжок: {_firstJump}, Второй прыжок: {_secondJump}, " +
                                $"Сумма: {JumpSum}");
            }
        }
    }
}