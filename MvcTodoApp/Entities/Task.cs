using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  FluentNHibernate.Mapping;

namespace MvcTodoApp.Entities
{
    public class Task
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
    }

    public sealed class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Table("Tasks");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Description);             
        }
    }
}