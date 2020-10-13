using System;
using System.Collections.Generic;
using System.IO;
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
                        mentorGroups.Last().AddMentor(line);
                    line = Console.ReadLine();
                }

                line = Console.ReadLine();
            }

            var studentList = new Dictionary<string, string[]>();

            using (var mentorFile = File.CreateText("C://mentors.txt"))
            {
                foreach (var mentor in ScheduleGenerator.GenerateSchedule(firstStudentGroup, secondStudentGroup,
                    mentorGroups))
                {
                    var mentorLine = mentor.Name + "\t";
                
                    for (var i = 0; i < 6; i++)
                        for (var j = 0; j < 2; j++)
                        {
                            if (j < mentor.ReviewedStudents[i].Count)
                                mentorLine += $"{mentor.ReviewedStudents[i][j]}\t";
                            else
                                mentorLine += "\t";
                            if(j < mentor.ReviewedStudents[i].Count && !studentList.ContainsKey(mentor.ReviewedStudents[i][j]))
                                studentList[mentor.ReviewedStudents[i][j]] = new string[6];
                            if(j < mentor.ReviewedStudents[i].Count)
                                studentList[mentor.ReviewedStudents[i][j]][i] = mentor.Name;
                        }
                    mentorFile.WriteLine(mentorLine);
                }
            }

            using (var studentFile = File.CreateText("C://students.txt"))
            {
                foreach (var studentPair in studentList)
                {
                    studentFile.Write($"{studentPair.Key}\t");
                    studentFile.WriteLine(string.Join("\t", studentPair.Value));
                }
            }
        }
    }
}