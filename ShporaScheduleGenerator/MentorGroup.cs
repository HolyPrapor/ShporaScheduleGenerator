using System;
using System.Collections.Generic;
using System.Linq;

namespace ShporaScheduleGenerator
{
    public class MentorGroup
    {
        public List<Mentor> Mentors = new List<Mentor>();
        public HashSet<string> ReviewedStudents = new HashSet<string>();

        public void AddMentor(string mentor, MentorReviewGroup mentorReviewGroup = MentorReviewGroup.Nevermind)
        {
            Mentors.Add(new Mentor(mentor, mentorReviewGroup));
        }

        public bool ReviewStudent(string student, Group @group)
        {
            if (ReviewedStudents.Contains(student))
                return false;
            // I suppose that every time the same people will have 2 students.
            var mentor = Mentors
                .Where(x =>
                {
                    if (@group == Group.First)
                        return x.ReviewGroup == MentorReviewGroup.OnlyFirst ||
                               x.ReviewGroup == MentorReviewGroup.Nevermind;
                    return x.ReviewGroup == MentorReviewGroup.OnlySecond ||
                           x.ReviewGroup == MentorReviewGroup.Nevermind;
                })
                .OrderBy(x => x.ReviewedStudents.Last().Count)
                .ThenBy(x => x.ReviewGroup)
                .FirstOrDefault();

            if (mentor == null)
                return false;
            mentor.ReviewStudent(student);
            ReviewedStudents.Add(student);
            return true;
        }

        public void NextIteration()
        {
            foreach (var mentor in Mentors)
                mentor.NextIteration();
        }
    }
}