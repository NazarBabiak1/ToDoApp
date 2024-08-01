using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Enums;

namespace ToDoApp.Services.Exceptions
{
    public class InvalidStatusTransitionException : Exception
    {
        public InvalidStatusTransitionException(ActivityStatus currentStatus, ActivityStatus newStatus)
            : base($"Invalid status transition from {currentStatus} to {newStatus}.")
        {
        }
    }
}
