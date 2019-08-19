namespace Renetdux.Infrastructure.Commands.Users
{
    public enum ErrorCodes
    {
        User_Get_User_Doesnt_Exists,
        User_Registration_Invalid_Data,
        User_Registration_Existing_Email,
        User_Login_Empty_Email,
        User_Login_Empty_Password,
        User_Login_Invalid_Email,
        User_Login_Invalid_Password,
        User_Login_Empty_RefreshToken,
        User_Login_Invalid_RefreshToken
    }
}
