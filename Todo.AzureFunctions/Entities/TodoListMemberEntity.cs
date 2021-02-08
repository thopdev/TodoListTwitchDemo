using Microsoft.Azure.Cosmos.Table;
using Todo.Shared.Enums;

namespace Todo.AzureFunctions.Entities
{
    public class TodoListMemberEntity : TableEntity
    {
        [IgnoreProperty]
        public string ListId
        {
            get => PartitionKey;
            set => PartitionKey = value;
        }

        [IgnoreProperty]
        public string UserId
        {
            get => RowKey;
            set => RowKey = value;
        }

        [IgnoreProperty]
        public ShareRole Role { 
            get => (ShareRole) RoleValue;
            set => RoleValue = (int) value;
        }

        public int RoleValue { get; set; }
    }
}
