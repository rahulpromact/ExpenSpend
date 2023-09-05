namespace ExpenSpend.Domain.Models.Friends;

public class Friend : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
}