namespace Todo.Shared.Constants
{
    public class FunctionConstants
    {
        public class TodoItem
        {
            public const string Get = "GetItemsOfTodoList";
            public const string Add = "AddTodoItem";
            public const string Update = "UpdateTodoItem";
            public const string Delete = "DeleteTodoItem";
        }

        public class User
        {
            public const string Get = "GetUsers";
        }

        public class TodoList
        {
            public const string Get = "GetTodoLists";
            public const string Add = "AddTodoLists";
            public const string Update = "UpdateTodoList";
            public const string Delete = "DeleteTodoList";

            public class Members
            {
                public const string Add = "AddMemberToTodoList";
                public const string Remove = "RemoveMemberFromTodoList";
            }
        }



    }
}
