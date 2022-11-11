using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Services
{
    public class ReminderService : BackgroundService
    {
        private readonly IServiceProvider provider;

        public ReminderService(IServiceProvider provider)
        {
            this.provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = provider.CreateScope();
                var data = scope.ServiceProvider.GetRequiredService<ToDoContext>();
                var emails = scope.ServiceProvider.GetRequiredService<EmailService>();

                var users = await data.Set<Assignee>()
                                        .IgnoreQueryFilters()
                                        .Include(u => u.Assignments)
                                        .ToListAsync(stoppingToken);

                foreach (var user in users)
                {
                    foreach (var assignment in user.Assignments)
                    {
                        if (!assignment.IsExpired && DateTime.Now > assignment.Reminder)
                        {
                            emails.SendEmail(user.Email, assignment.Number);
                            assignment.IsExpired = true;
                        }
                    }
                }

                await data.SaveChangesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}
