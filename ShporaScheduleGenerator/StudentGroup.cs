using System.Collections.Generic;

namespace ShporaScheduleGenerator
{
    public class StudentGroup
    {
        public readonly List<string> Students = new List<string>();
        public Group Group { get; }

        public StudentGroup(Group @group)
        {
            Group = @group;
        }

        public void AddStudent(string student)
        {
            Students.Add(student);
        }
    }
}