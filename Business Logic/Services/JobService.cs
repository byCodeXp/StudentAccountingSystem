using System;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Entities;
using Hangfire;
using SendGrid.Helpers.Mail;

namespace Business_Logic.Services
{
    public class JobService
    {
        private readonly EmailService emailService;
        
        public JobService(EmailService emailService)
        {
            this.emailService = emailService;
        }

        public void ScheduleCourseReminder(UserDTO user, CourseDTO course, DateTime dateStart)
        {
            string subject = "Course reminder";
            string content = $"This is reminder for course: {course.Name}";
            EmailAddress email = new(user.Email);
            
            if (dateStart > DateTime.Today + AppEnv.Dates.In30Days)
            {
                BackgroundJob.Schedule(() => emailService.SendMailAsync(subject, email, content), dateStart - AppEnv.Dates.In30Days);
            }
            if (dateStart > DateTime.Today + AppEnv.Dates.In7Days)
            {
                BackgroundJob.Schedule(() => emailService.SendMailAsync(subject, email, content), dateStart - AppEnv.Dates.In7Days);
            }
            if (dateStart > DateTime.Today + AppEnv.Dates.In1Day)
            {
                BackgroundJob.Schedule(() => emailService.SendMailAsync(subject, email, content), dateStart - AppEnv.Dates.In1Day);
            }
        }
    }
}