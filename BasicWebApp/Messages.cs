using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicWebApp
{
    public class Messages
    {
        public const string EmptyBodyMessage = "Error: Request body is empty.";
        public const string InvalidBodyMessage = "Error: Invalid request body.";
        public const string InvalidIdMessage = "Error: Id does not exist.";
        public const string MissingIdMessage = "Error: No id was given.";
    }
}