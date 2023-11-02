using Microsoft.AspNetCore.Identity;

namespace Campus.CustomerMiddlewares
{
    /// <summary>
    /// 本地化一些错误，如果没有本地化会显示为英语
    /// </summary>
    /// <remarks>这些错误将返回到控制器，通常用作向最终用户显示Identity中的消息。</remarks>
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError { Code = nameof(DefaultError), Description = $"发生了未知故障。" };
        }
        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError { Code = nameof(ConcurrencyFailure), Description = $"乐观并发失败，对象已被修改。" };
        }
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Code = nameof(PasswordMismatch), Description = $"密码错误。" };
        }
        public override IdentityError InvalidToken()
        {
            return new IdentityError { Code = nameof(InvalidToken), Description = $"无效的令牌。" };
        }
        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = $"具有此登陆的用户已经存在。" };
        }
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError { Code = nameof(InvalidUserName), Description = $"用户名{userName}无效，只能包含字母或数字。" };
        }
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError { Code = nameof(InvalidEmail), Description = $"邮箱{email}无效。" };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = nameof(DuplicateUserName), Description = $"用户名{userName}已被使用。" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = nameof(DuplicateEmail), Description = $"邮箱{email}已被使用。" };
        }
        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError { Code = nameof(InvalidRoleName), Description = $"角色名{role}无效。" };
        }
        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"角色名{role}已被使用。" };
        }
        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = $"该用户已经设置了密码。" };
        }
        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = $"此用户未启用锁定。" };
        }
        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"用户已关联角色{role}。" };
        }
        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError { Code = nameof(UserNotInRole), Description = $"用户未关联角色{role}。" };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = nameof(PasswordTooShort), Description = $"密码必须是至少{length}字符。" };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError { 
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = $"密码必须至少有一个非字母数字字符。" };
        }
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = $"密码必须至少有一个数字。" };
        }
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return base.PasswordRequiresUniqueChars(uniqueChars);
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = $"密码必须至少有一个小写字母。" };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = $"密码必须至少有一个大写字母。" };
        }
    }
}
