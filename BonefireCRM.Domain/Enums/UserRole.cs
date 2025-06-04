namespace BonefireCRM.Domain.Enums
{
    public enum UserRole
    {
        Admin = 1,    //Manage users, customize pipelines, access all data, configure system settings.
        Manager = 2,    //View team performance, generate reports, assign tasks.
        SalesRep = 3    //Manage assigned leads, log interactions, update tasks, set reminders.
    }
}
