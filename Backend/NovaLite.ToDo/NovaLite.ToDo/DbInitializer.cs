using NovaLite.ToDo.Model;

namespace NovaLite.ToDo
{
    public static class DbInitializer
    {
        public static void Initialize(ToDoContext context)
        {
            if (context.Set<Assignee>().Any())
            {
                return;
            }

            var assignee = new Assignee() { Id = Guid.NewGuid() };
            var assigments = new List<Assignment>
            {
                new Assignment{ Subject="Zadatak 1", Description="Zavrsiti task 1", Steps = { new Step{ Subject="Korak 1", Completed=true },
                                                                                              new Step{ Subject="Korak 2", Completed=false } }
                },
                new Assignment{ Subject="Assignment steps overview", Description="When a user goes to an assignment details page, the user should be able to see all steps for the given assignment."+
                                                                                 "Each step should display it's subject and an indicator whether it is completed or not (checkbox - read only).",
                                Steps = { new Step{ Subject="Korak 1", Completed=true },
                                          new Step{ Subject="Model", Completed=false },
                                          new Step{ Subject="Konfiguracija", Completed=false }}
                },
                new Assignment{ Subject="Assignment details", Description="Open assigment details page", Steps = { new Step{ Subject="Korak 1", Completed=false },
                                                                                                                   new Step{ Subject="Korak 2", Completed=false } }
                },
                new Assignment{ Subject="Zadatak 4", Description="Zavrsiti task 4", Steps = { new Step{ Subject="Korak 1", Completed=true },
                                                                                              new Step{ Subject="Korak 2", Completed=false } }
                },
            };

            foreach (var assigment in assigments)
            {
                assignee.AddAssignment(assigment);
            }

            context.Add(assignee);
            context.SaveChanges();
        }
    }
}
