using System;
using System.Threading.Tasks;
using Data_Transfer_Objects.Entities;
using Hangfire;
using SendGrid.Helpers.Mail;

namespace Business_Logic.Services
{
    public class JobService
    {
        private EmailService _emailService;
        
        public JobService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void ScheduleCourseReminder(UserDTO user, CourseDTO course, DateTime dateStart)
        {
            string content = $"This is reminder for course: {course.Name}";
            
            if (dateStart > DateTime.Today + TimeSpan.FromDays(30))
            {
                BackgroundJob.Schedule(() => Invoke("Course reminder", user.Email, content), dateStart - TimeSpan.FromDays(30));
            }
            if (dateStart > DateTime.Today + TimeSpan.FromDays(7))
            {
                BackgroundJob.Schedule(() => Invoke("Course reminder", user.Email, content), dateStart - TimeSpan.FromDays(7));
            }
            if (dateStart > DateTime.Today + TimeSpan.FromDays(1))
            {
                BackgroundJob.Schedule(() => Invoke("Course reminder", user.Email, content), DateTime.Today.AddHours(8));
            }
        }

        private async Task Invoke(string subject, string email, string htmlContent)
        {
            await _emailService.SendMailAsync(subject, new EmailAddress(email), htmlContent);
        }
    }
}