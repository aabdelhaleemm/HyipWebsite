namespace Application.User.Queries.GetUserDetails.Dto
{
    public class UserDto
    {
        public UserWalletDto Wallet { get; set; }
        public int? ReferenceId { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}