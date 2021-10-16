using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business_Logic.Helpers;
using Data_Transfer_Objects;
using Data_Transfer_Objects.EmailTemplates;
using Data_Transfer_Objects.Entities;
using Hangfire;
using SendGrid.Helpers.Mail;

namespace Business_Logic.Services
{
    public class JobService
    {
        private readonly EmailService emailService;
        private readonly RazorTemplateHelper razorTemplateHelper;

        public JobService(EmailService emailService, RazorTemplateHelper razorTemplateHelper)
        {
            this.emailService = emailService;
            this.razorTemplateHelper = razorTemplateHelper;
        }

        public async Task<IEnumerable<string>> ScheduleCourseReminder(UserDTO user, CourseDTO course, DateTime dateStart)
        {
            string subject = "Course reminder";
            EmailAddress email = new(user.Email);

            List<string> jobs = new ();
            
            var template = await razorTemplateHelper.GetTemplateHtmlAsStringAsync("Remind", new SubscribedEmailModel
            {
                Title = course.Name, Preview = course.Preview, Date = dateStart.ToLongDateString()
            });
            
            if (dateStart > DateTime.Today + AppEnv.Dates.In30Days)
            {
                var job = BackgroundJob.Schedule(() => emailService.SendMailAsync(subject, email, template), dateStart - AppEnv.Dates.In30Days);
                jobs.Add(job);
            }
            if (dateStart > DateTime.Today + AppEnv.Dates.In7Days)
            {
                var job = BackgroundJob.Schedule(() => emailService.SendMailAsync(subject, email, template), dateStart - AppEnv.Dates.In7Days);
                jobs.Add(job);
            }
            if (dateStart > DateTime.Today + AppEnv.Dates.In1Day)
            {
                var job = BackgroundJob.Schedule(() => emailService.SendMailAsync(subject, email, template), dateStart - AppEnv.Dates.In1Day);
                jobs.Add(job);
            }

            return jobs;
        }
    }
}