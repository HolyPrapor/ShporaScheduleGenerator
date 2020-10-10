using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShporaScheduleGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var firstStudentGroup = new StudentGroup(Group.First);

            var line = Console.ReadLine();
            while (line != "__")
            {
                firstStudentGroup.AddStudent(line);
                line = Console.ReadLine();
            }

            var secondStudentGroup = new StudentGroup(Group.Second);

            line = Console.ReadLine();
            while (line != "__")
            {
                secondStudentGroup.AddStudent(line);
                line = Console.ReadLine();
            }

            var mentorGroups = new List<MentorGroup>();
            line = Console.ReadLine();
            while (line != "__")
            {
                mentorGroups.Add(new MentorGroup());
                while (line != "___")
                {
                    if (line[0] == '1')
                        mentorGroups.Last().AddMentor(line.Substring(2, line.Length - 2), MentorReviewGroup.OnlyFirst);
                    else if (line[0] == '2')
                        mentorGroups.Last().AddMentor(line.Substring(2, line.Length - 2), MentorReviewGroup.OnlySecond);
                    else
                        mentorGroups.Last().AddMentor(line.Substring(2, line.Length - 2));
                    line = Console.ReadLine();
                }

                line = Console.ReadLine();
            }

            foreach (var mentor in ScheduleGenerator.GenerateSchedule(firstStudentGroup, secondStudentGroup,
                mentorGroups, 1))
            {
                Console.WriteLine(mentor.Name);
                for (var i = 0; i < mentor.ReviewedStudents.Count - 1; i++)
                    Console.WriteLine($"\t {i + 1}. {string.Join(", ", mentor.ReviewedStudents[i])}");
            }

            Console.ReadKey();
        }
    }
}