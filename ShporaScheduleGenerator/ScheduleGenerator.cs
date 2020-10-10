using System;
using System.Collections.Generic;
using System.Linq;

namespace ShporaScheduleGenerator
{
    public static class ScheduleGenerator
    {
        public static List<Mentor> GenerateSchedule(StudentGroup firstStudentGroup, StudentGroup secondStudentGroup,
            List<MentorGroup> mentorGroups, int iterationAmount = 6)
        {
            if (firstStudentGroup.Group != Group.First || secondStudentGroup.Group != Group.Second ||
                mentorGroups.Count == 0)
                throw new ArgumentException("Wrong arguments.");

            for (var i = 0; i < iterationAmount; i++)
            {
                if (firstStudentGroup.Students.Select(student => mentorGroups
                    .Where(x => !x.ReviewedStudents.Contains(student))
                    .OrderBy(x => x.ReviewedStudents.Count)
                    .ThenBy(x => x.Mentors.Count(y => y.ReviewGroup != MentorReviewGroup.OnlySecond))
                    .Any(x => x.ReviewStudent(student, Group.First))).Any(foundMentor => !foundMentor))
                    throw new ArgumentException("It is not possible to generate schedule.");

                if (secondStudentGroup.Students.Select(student => mentorGroups
                    .Where(x => !x.ReviewedStudents.Contains(student))
                    .OrderBy(x => x.ReviewedStudents.Count)
                    .ThenBy(x => x.Mentors.Count(y => y.ReviewGroup != MentorReviewGroup.OnlyFirst))
                    .Any(x => x.ReviewStudent(student, Group.Second))).Any(foundMentor => !foundMentor))
                    throw new ArgumentException("It is not possible to generate schedule.");

                foreach (var mentorGroup in mentorGroups)
                    mentorGroup.NextIteration();
            }

            return mentorGroups.SelectMany(x => x.Mentors).ToList();
        }
    }
}