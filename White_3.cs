using System;
using System.Linq;
namespace Lab_7
{
    public class White_3
    {
        public class Student
        {
            protected string _name;
            protected string _surname;
            protected int[] _marks;
            protected int _skipped;

            public string Surname => _surname;
            public string Name => _name;
            public int Skipped => _skipped;
            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;
                    return (double)_marks.Sum() / _marks.Length;
                }
            }

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[0];
                _skipped = 0;
            }

            protected Student(Student student)
            {
                _name = student._name;
                _surname = student._surname;
                _marks = student._marks.ToArray();
                _skipped = student._skipped;
            }

            public void Lesson(int mark)
            {
                if (mark == 0)
                {
                    _skipped++;
                }
                else
                {
                    int[] newMarks = new int[_marks.Length + 1];
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        newMarks[i] = _marks[i];
                    }
                    newMarks[newMarks.Length - 1] = mark;
                    _marks = newMarks;
                }
            }

            public static void SortBySkipped(Student[] students)
            {
                if (students == null || students.Length == 0) return;
                for (int i = 0; i < students.Length - 1; i++)
                {
                    for (int j = 0; j < students.Length - 1 - i; j++)
                    {
                        if (students[j].Skipped < students[j + 1].Skipped)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
            }

            public virtual void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, " +
                                $"Средняя оценка: {AvgMark:F2}, Пропуски: {_skipped}");
            }
        }

        public class Undergraduate : Student
        {
            public Undergraduate(string name, string surname) : base(name, surname)
            {
            }

            public Undergraduate(Student student) : base(student)
            {
            }

            public void WorkOff(int mark)
            {
                if (_skipped > 0)
                {
                    _skipped--;
                    Lesson(mark);
                }
                else if (_marks.Contains(2))
                {
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        if (_marks[i] == 2)
                        {
                            _marks[i] = mark;
                            break;
                        }
                    }
                }
            }

            public override void Print()
            {
                Console.WriteLine($"Имя: {_name}, Фамилия: {_surname}, " +
                                $"Средняя оценка: {AvgMark:F2}, Пропуски: {_skipped}, " +
                                $"Статус: Undergraduate");
            }
        }
    }
}