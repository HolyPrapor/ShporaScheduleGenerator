using System.Collections.Generic;
using System.Linq;

namespace ShporaScheduleGenerator
{
    public class Mentor
    {
        public string Name { get; }
        public MentorReviewGroup ReviewGroup { get; }

        public List<List<string>> ReviewedStudents = new List<List<string>>();

        public Mentor(string name, MentorReviewGroup reviewGroup)
        {
            Name = name;
            ReviewGroup = reviewGroup;
            ReviewedStudents.Add(new List<string>());
        }

        public void ReviewStudent(string student)
        {
            ReviewedStudents.Last().Add(student);
        }

        public void NextIteration()
        {
            ReviewedStudents.Add(new List<string>());
        }
    }
}