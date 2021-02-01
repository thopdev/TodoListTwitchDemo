using Microsoft.Azure.Cosmos.Table;

namespace Todo.AzureFunctions.Entities
{
    public class UserEntity : TableEntity
    {
        [IgnoreProperty]
        public string IdentityProvider
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
        public string UserDetails { get; set; }
    }
}
